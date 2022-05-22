using AutoMapper;
using WdaApi.Api.Extensions;
using WdaApi.Api.Services.Profiles;
using WdaApi.Api.ViewModels;
using WdaApi.Business.Interfaces;
using WdaApi.Business.Models;
using WdaApi.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WdaApi.Business.Dto;

namespace WdaApi.Api.Controllers.User
{
    [Route("{culture:culture}/api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ProfilesController : MainController<ProfilesController>
    {
        private readonly IStringLocalizer<ProfilesController> _localizer;
        private readonly IProfileService _profileService;
        private readonly IMapper _mapper;

        public ProfilesController(IErrorNotifier errorNotifier,
                 IUser user, IStringLocalizer<ProfilesController> localizer,
                 IProfileService profileService, IMapper mapper) : base(errorNotifier, user, localizer)
        {
            _profileService = profileService;
            _localizer = localizer;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<ActionResult> PostAsync(ProfileRequestVM profileVM)
        {
            var profileUser = _mapper.Map<ProfileUser>(profileVM);

            if (await _profileService.CheckValidProfileAsync(profileUser))
            {
                await _profileService.Add(profileUser);
            }
            return CustomResponse();
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> PutAsync([Required] Guid id, ProfileRequestVM profileVM)
        {
            var profileUser = _mapper.Map<ProfileUser>(profileVM);
            profileUser.Id = id;

            if (await _profileService.CheckValidProfileAsync(profileUser))
            {
                await _profileService.Update(profileUser);               
            }
            return CustomResponse();
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync([FromQuery] FilterProfileUserDto filterVM)
        {
            return CustomResponse(await _profileService.Search(filterVM));
        }
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult> GetById([Required] Guid id)
        {
            if (!await _profileService.checkProfileExist(id))
                return NotFound();
            return CustomResponse(await _profileService.GetById(id));
        }
        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<ProfileResponseVM>> Delete([Required] Guid id)
        {
            if (!await _profileService.checkProfileExist(id))
                return NotFound();
             await _profileService.DeleteById(id);

            return CustomResponse();

        }
    }
}
