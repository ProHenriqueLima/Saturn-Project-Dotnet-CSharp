using AutoMapper;
using SaturnApi.Api.Configuration;
using SaturnApi.Api.Extensions;
using SaturnApi.Api.Services.Profiles;
using SaturnApi.Api.ViewModels;
using SaturnApi.Api.ViewModels.UserViewModel;
using SaturnApi.Business.Interfaces;
using SaturnApi.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SaturnApi.Api.Controllers
{
    [Route("{culture:culture}/api/account")]
    public class AuthController : MainController<AuthController>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IProfileService _profileService;
        private readonly IProfileRepository _profileRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppSettings _appSettings;
        //private readonly IUserRepository _userRepository;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        private readonly IStringLocalizer<AuthController> _localizer;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public AuthController(IMapper mapper, IProfileService profileService, IUserRepository userRepository, IErrorNotifier errorNotifier,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager, IOptions<AppSettings> appSettings,
            IUser user, /*IUserRepository userRepository,*/
            IEmailSender emailSender, IConfiguration configuration,
            IStringLocalizer<AuthController> localizer, IProfileRepository profileRepository,
            IHttpContextAccessor httpContextAccessor) : base(errorNotifier, user, localizer)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
            //_userRepository = userRepository;
            _emailSender = emailSender;
            _configuration = configuration;
            _localizer = localizer;
            _userRepository = userRepository;
            _profileService = profileService;
            _mapper = mapper;
            _profileRepository = profileRepository;
            _httpContextAccessor = httpContextAccessor;

        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return CustomResponse();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUserViewModel loginUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var userIdentity = await _userManager.FindByEmailAsync(loginUser.Email);

            if ((userIdentity == null) || (userIdentity.IsDeleted == true) || (userIdentity.ProfileId == null)  || (userIdentity.IsDeleted == true))
            {
                NotifyError("Usuario ou Senha incorretos");
                return CustomResponse(loginUser);
            }

            var result = await _signInManager.PasswordSignInAsync(userIdentity, loginUser.Password, false, true);

            if (result.Succeeded)
            {
                return CustomResponse(await GenerateJwt(userIdentity.Email));
            }
            if (result.IsLockedOut)
            {
                NotifyError("Usuário temporariamente bloqueado por tentativas inválidas");
                return CustomResponse(loginUser);
            }

            NotifyError("Usuário ou Senha incorretos");
            return CustomResponse(loginUser);
        }

        private async Task<LoginResponseViewModel> GenerateJwt(string userName)
        {

            var user = await _userManager.FindByEmailAsync(userName);
            var claims = await GetClaims(user);

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            UserTokenViewModel userData = await GetUserData(user, claims);

            return new LoginResponseViewModel
            {
                AccessToken = CreateToken(identityClaims),
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpiryHours).TotalSeconds,
                UserToken = userData
            };
        }

        private async Task<ProfileResponseVM> getProfile(ApplicationUser userIdentity)
        {
            return _mapper.Map<ProfileResponseVM>(await _profileRepository.GetById((Guid)userIdentity.ProfileId));
        }

        private async Task<IList<Claim>> GetClaims(ApplicationUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            return claims;
        }

        private async Task<UserTokenViewModel> GetUserData(ApplicationUser user, IList<Claim> claims)
        {

            var notTypeResults = new List<string>(){
                JwtRegisteredClaimNames.Sub,
                JwtRegisteredClaimNames.Email,
                JwtRegisteredClaimNames.UniqueName,
                JwtRegisteredClaimNames.Jti,
                JwtRegisteredClaimNames.Nbf,
                JwtRegisteredClaimNames.Iat
            };
            var profile = user.ProfileId != null ? await getProfile(user) : null;

            UserTokenViewModel userData = new UserTokenViewModel()
            {
                Id = user.Id,
                Name = user.UserName,
                Email = user.Email,
                Profile = profile,
                Language =  null,

                //Claims = claimsCustomer,
                //Claims = claims.Where(c => !notTypeResults.Contains(c.Type))
                //            .Select(c => new ClaimViewModel { Type = c.Type, Value = c.Value }),

            };

            return userData;
        }

        private string CreateToken(ClaimsIdentity identityClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emitter,
                Audience = _appSettings.ValidIn,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiryHours),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        [HttpPost("ForgotPassword")]

        public async Task<IActionResult> ForgotPassword(UserNameViewModel forgotPassword)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);


            try
            {
                var user = await _userManager.FindByNameAsync(forgotPassword.Email);
                if (user == null)
                    return NotFound();

                string newPassword = UsefulFunctions.GenerateRandomPassword();
                var hash = await _signInManager.UserManager.GeneratePasswordResetTokenAsync(user);
                await _signInManager.UserManager.ResetPasswordAsync(user, hash, newPassword);

                string emailBody = _localizer["Sua nova senha de acesso: {0}", newPassword].Value;

                _emailSender.SendEmail(user.Email, _localizer["Redefinir Senha"].Value, emailBody);
            }
            catch (Exception e)
            {
                NotifyError(e.Message);
            }

            return CustomResponse();
        }

        private async Task<string> GetUrlResetPassword(ApplicationUser user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var forgotPasswordToken = new EmailTokenViewModel()
            {
                userName = user.UserName,
                token = token
            };

            string json = JsonConvert.SerializeObject(forgotPasswordToken);
            string forgotPasswordTokenBase64 = UsefulFunctions.Base64Encode(json);

            return $"{_configuration.GetValue<string>("UrlSaturnApiWeb")}/reset_password/{forgotPasswordTokenBase64}";
        }

        [HttpPost("ResetPassword")]
        [Authorize]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPassword)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (_httpContextAccessor.HttpContext.User.Identity.Name != resetPassword.Email)
            {
                return StatusCode(403);
            }

            var user = await _userManager.FindByEmailAsync(resetPassword.Email);

            if (user == null)
                return NotFound();

            var hash = await _signInManager.UserManager.GeneratePasswordResetTokenAsync(user);

            var resetPasswordResult = await _userManager.ResetPasswordAsync(user, hash, resetPassword.Password);
            if (!resetPasswordResult.Succeeded)
            {
                foreach (var error in resetPasswordResult.Errors)
                {
                    NotifyError(error.Description);
                }
            }
            else
            {
                string emailBody = _localizer["Sua nova senha de acesso: {0}", resetPassword.Password].Value;

                _emailSender.SendEmail(user.Email, _localizer["Redefinir Senha"].Value, emailBody);
            }

            return CustomResponse();
        }

        //[HttpPost("ConfirmEmail")]
        private async Task<IActionResult> ConfirmEmail(EmailTokenViewModel emailTokenViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = await _userManager.FindByNameAsync(emailTokenViewModel.userName);

            var result = await _userManager.ConfirmEmailAsync(user, emailTokenViewModel.token);

            if (result.Succeeded)
            {
                return CustomResponse();
            }

            foreach (var erro in result.Errors)
            {
                NotifyError(erro.Description);
            }

            return CustomResponse();
        }

        //[HttpPost("ResendingConfirmationEmail")]
        private async Task<IActionResult> ResendingConfirmationEmail(UserNameViewModel userName)
        {
            var user = await _userManager.FindByNameAsync(userName.Email);

            var confirmationLink = await GetUrlConfirmationEmail(user);

            var emailBody = _localizer["Para confirmar seu cadastro no sistema <a href={0}>Clique Aqui</a>.", confirmationLink];
            string subject = _localizer["Sua senha de Acesso"];
            _emailSender.SendEmail(user.Email, subject, emailBody.Value);

            return CustomResponse();
        }

        private async Task<string> GetUrlConfirmationEmail(ApplicationUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var confirmEmailToken = new EmailTokenViewModel()
            {
                userName = user.UserName,
                token = token
            };

            string json = JsonConvert.SerializeObject(confirmEmailToken);
            string confirmEmailTokenBase64 = UsefulFunctions.Base64Encode(json);

            return $"{_configuration.GetValue<string>("UrlSaturnApiWeb")}/confirm_email/{confirmEmailTokenBase64}";
        }
    }
}
