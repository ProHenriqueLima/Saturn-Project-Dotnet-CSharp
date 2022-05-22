using AutoMapper;
using WdaApi.Api.ViewModels;
using WdaApi.Business.Models;
using Microsoft.Extensions.Localization;


namespace WdaApi.Api.Configuration
{
    public class AutomapperConfig : AutoMapper.Profile
    {
        public AutomapperConfig()
        {
            CreateMap<ProfileUser, ProfileRequestVM>()   
                .ReverseMap();

            CreateMap<ProfileUser, ProfileResponseVM>()
                .ReverseMap();



            CreateMap<User, UserRequestVM>().
                 ForMember(el => el.Email, opt => opt.MapFrom(el => el.UserIdentity.Email)).
                 ForMember(el => el.ProfileId, opt => opt.MapFrom(el => el.UserIdentity.ProfileId)).
                 ForMember(el => el.Status, opt => opt.MapFrom(el => el.UserIdentity.IsDeleted))
                .ReverseMap();

            CreateMap<User, UserUpdateVM>().
                 ForMember(el => el.ProfileId, opt => opt.MapFrom(el => el.UserIdentity.ProfileId)).
                 ForMember(el => el.Status, opt => opt.MapFrom(el => el.UserIdentity.IsDeleted))
                .ReverseMap();

            CreateMap<User, UserResponseVM>().
                 ForPath(el => el.Profile.Name, opt => opt.MapFrom(el => el.UserIdentity.Profile.Name)).
                 ForPath(el => el.Profile.Id, opt => opt.MapFrom(el => el.UserIdentity.ProfileId)).
                 ForMember(el => el.Status, opt => opt.MapFrom(el => el.UserIdentity.IsDeleted))
                .ReverseMap();



        }



        public class TranslateResolver : IMemberValueResolver<object, object, string, string>
        {
            private readonly IStringLocalizer<SharedResource> _localizer;
            public TranslateResolver(IStringLocalizer<SharedResource> localizer)
            {
                _localizer = localizer;
            }

            public string Resolve(object source, object destination, string sourceMember, string destMember,
                ResolutionContext context)
            {
                return _localizer[sourceMember];
            }
        }
    }
}
