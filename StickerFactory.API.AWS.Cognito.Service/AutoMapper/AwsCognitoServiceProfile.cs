using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using AutoMapper;
using StickerFactory.API.AWS.Cognito.Service.AutoMapper.CustomResolvers;
using StickerFactory.API.AWS.Cognito.Service.Models;
using Core = StickerFactory.API.Core.Models;

namespace StickerFactory.API.AWS.Cognito.Service.AutoMapper;

internal class AwsCognitoServiceProfile : Profile
{
    public AwsCognitoServiceProfile()
    {
        CreateMap<CognitoUser, Core::User>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserID))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(new UserFirstNameResolver()))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(new UserLastNameResolver()))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(new UserEmailResolver()))
            .ForMember(dest => dest.ContactNumber, opt => opt.MapFrom(new UserContactNumberResolver()));
        CreateMap<AuthenticationResultType, Core::UserAuth.UserAuthToken>();
    }
}