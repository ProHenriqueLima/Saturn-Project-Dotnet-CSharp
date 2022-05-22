
using AutoMapper;
using WdaApi.Api.ViewModels;
using WdaApi.Business.Interfaces;
using WdaApi.Business.Models;
using WdaApi.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using WdaApi.Business.Dto;

namespace WdaApi.Api.Services.Profiles
{
    public class ProfileService : BaseService, IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IMapper _mapper;

        public ProfileService(IProfileRepository profileRepository,
            IErrorNotifier errorNotifier, IMapper mapper
           ) : base(errorNotifier)
        {
            _profileRepository = profileRepository;
            _mapper = mapper;

        }

        public async Task Add(ProfileUser profile)
        {
            var result = await _profileRepository.AddAsync(profile);
        }

        public async Task Update(ProfileUser profile)
        {            
            await _profileRepository.Update(profile);
        }
        public async Task<bool> checkProfileExist(Guid id)
        {
            return await _profileRepository.checkProfileExist(id);
        }

        public async Task<PagedResult<ProfileResponseVM>> Search(FilterProfileUserDto filterVM)
        {
            return convertPageList(await _profileRepository.Search(filterVM));
        }

        public async Task<bool> CheckValidProfileAsync(ProfileUser profile)
        {
            if (await _profileRepository.checkProfileRegistered(profile))
            {
                NotifyError("Perfil já Cadastrado");
                return false;
            }

            return true;
        }
        public async Task<ProfileResponseVM> GetById(Guid id)
        {
            var profileUser = await _profileRepository.GetById(id);
            return _mapper.Map<ProfileResponseVM>(profileUser);
        }

        public async Task DeleteById(Guid id)
        {
            await _profileRepository.Remove(id);
        }
        private PagedResult<ProfileResponseVM> convertPageList(IPagedList<ProfileUser> pagedList)
        {
            PagedResult<ProfileResponseVM> pagedResult = new PagedResult<ProfileResponseVM>();
            pagedResult.PageIndex = pagedList.PageNumber;
            pagedResult.PageSize = pagedList.PageSize;
            pagedResult.TotalResults = pagedList.TotalItemCount;
            pagedResult.List = _mapper.Map<IEnumerable<ProfileResponseVM>>(pagedList);
            return pagedResult;
        }
        public void Dispose()
        {
            _profileRepository?.Dispose();
        }


    }
}
