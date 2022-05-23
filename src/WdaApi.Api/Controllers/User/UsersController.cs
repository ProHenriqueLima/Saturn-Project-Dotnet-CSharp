using AutoMapper;
using SaturnApi.Api.Services;
using SaturnApi.Api.ViewModels;
using SaturnApi.Business.Interfaces;
using SaturnApi.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SaturnApi.Api.Controllers
{

    [Route("{culture:culture}/api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UsersController : MainController<UsersController>
    {
        private readonly IStringLocalizer<UsersController> _localizer;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IUserIdentityService _userIdentityService;
        private readonly IEmailService _emailService;

        public UsersController(IErrorNotifier errorNotifier,
                 IUser user, IStringLocalizer<UsersController> localizer,
                    IMapper mapper, IUserService userService, IUserIdentityService userIdentityService,
                    IEmailService emailService) : base(errorNotifier, user, localizer)
        {

            _localizer = localizer;
            _mapper = mapper;
            _userService = userService;
            _userIdentityService = userIdentityService;
            _emailService = emailService;
        }
        /// <summary>
        /// Método utilizado para Adicionar um usuário
        /// </summary>
        /// <param name="userVM"></param>
        /// <returns></returns>

        [HttpPost]
        //[Authorize]
        public async Task<ActionResult> Add(UserRequestVM userVM)
        {
            await AddUser(userVM);

            return CustomResponse(userVM, 201);
        }
        /// <summary>
        /// Método utilizado para alterar um usuário
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userVM"></param>
        /// <returns></returns>
        [HttpPut]
        //[Authorize]
        public async Task<ActionResult> PutAsync([Required] Guid id, UserUpdateVM userVM)
        {
            if (!await _userService.CheckUserExist(id))
                return NotFound();
            await _userService.Update(id, _mapper.Map<SaturnApi.Business.Models.User>(userVM));
            return CustomResponse(userVM);
        }
        /// <summary>
        /// Desabilitar um usuário
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userStatus"></param>
        /// <returns></returns>
        [HttpPut("{id:Guid}/disable")]
        //[Authorize]
        public async Task<ActionResult> DeleteAsync([Required] Guid id, UserDeleteVM userStatus)
        {
            if (!await _userService.CheckUserExist(id))
                return NotFound();

            await _userService.Delete(id, userStatus);

            return CustomResponse();
        }
        /// <summary>
        /// Método utilizado para consultar usuários
        /// </summary>
        /// <param name="filterVM"></param>
        /// <returns></returns>
        [HttpGet]
        //[Authorize]
        public async Task<PagedResult<UserResponseVM>> GetAllAsync([FromQuery] FilterUserVM filterVM)
        {
            return await _userService.Search(filterVM);
        }

        private async Task AddUser(UserRequestVM userVM)
        {
            var userIdentity = await _userIdentityService.Add(_mapper.Map<SaturnApi.Business.Models.User>(userVM));

            try
            {               
                if (userIdentity != null)
                {
                    var validUser = await _userService.Add(_mapper.Map<SaturnApi.Business.Models.User>(userVM), userIdentity);
                    if (validUser)
                        await _emailService.SendUserConfirmationEmail(userIdentity, _userIdentityService.GetPassword(), _localizer);
                    else
                        await _userIdentityService.RemoveUser(userIdentity);
                }
            }

            catch
            {
                 await _userIdentityService.RemoveUser(userIdentity);
            }
        }
    }
}

