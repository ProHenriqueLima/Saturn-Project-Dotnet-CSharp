using AutoMapper;
using WdaApi.Api.Configuration;
using WdaApi.Api.ViewModels;
using WdaApi.Business.Interfaces;
using WdaApi.Business.Models;
using WdaApi.Data.Repository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace WdaApi.Api.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IMapper _mapper;
        public readonly IUserRepository _userRepository;
        public readonly IProfileRepository _profileRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserIdentityService _userIdentityService;


        public UserService(IErrorNotifier errorNotifier, IMapper mapper,
            IUserRepository userRepository, IProfileRepository profileRepository, 
            UserManager<ApplicationUser> userManager, IUserIdentityService userIdentityService) : base(errorNotifier)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _profileRepository = profileRepository;
            _userManager = userManager;
            _userIdentityService = userIdentityService;
        }

        public async Task<bool> Add(User user, ApplicationUser applicationUser)
        {

            user.UserIdentity = applicationUser;
            await _userRepository.AddAsync(user);
            return true;

        }
        public async Task<UserRequestVM> Update(Guid id, User user)
        {
            if (await _profileRepository.checkProfileExist((Guid)user.UserIdentity.ProfileId))
            {
                var userBd = await _userRepository.GetById(id) ;
                userBd.FullName = user.FullName;                  
                await _userRepository.Update(userBd);

                await _userIdentityService.Update(userBd.Email, user);
                return _mapper.Map<UserRequestVM>(userBd);
            }
            else
            {
                NotifyError("Perfil não encontrado!");
                return null;
            }
        }
        public async Task Delete(Guid id, UserDeleteVM user)
        {
            var userBd = await _userRepository.GetByIdWithIncludes(id);
            userBd.UserIdentity.IsExcluded = user.Status;
            //userBd.isExcluded = user.Status;
            await _userIdentityService.Update(userBd.Email, userBd);    
        }
        public async Task<bool> CheckUserExist(Guid id)
        {
            var list = await _userRepository.Search(u => u.Id == id);
            return list.Any();
        }

        public void Dispose()
        {
            _userRepository?.Dispose();
        }

        public async Task<PagedResult<UserResponseVM>> Search(FilterUserVM filterVM)
        {
           return convertPageList(await _userRepository.Search(filterVM.PageIndex, 
                filterVM.PageSize, filterVM.FullName, filterVM.Email,
                filterVM.ProfileName,filterVM.Status,  filterVM.Id));
        }
        private PagedResult<UserResponseVM> convertPageList(IPagedList<User> pagedList)
        {
            PagedResult<UserResponseVM> pagedResult = new PagedResult<UserResponseVM>();

            pagedResult.PageIndex = pagedList.PageNumber;
            pagedResult.PageSize = pagedList.PageSize;
            pagedResult.TotalResults = pagedList.TotalItemCount;
            pagedResult.List = _mapper.Map<IEnumerable<UserResponseVM>>(pagedList);
            return pagedResult;
        }
    }
}
