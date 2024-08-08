using AutoMapper;
using SolarVoyage.API.DtoModels;
using CoreModels = SolarVoyage.Core.Models;

namespace SolarVoyage.API.AutoMapper;

public class ApiProfile: Profile
{
    public ApiProfile()
    {
        CreateMap<UserSignUpRequest, CoreModels.UserAuth.UserSignUpRequest>()
            .ReverseMap();
        CreateMap<ConfirmUserSignUpRequest, CoreModels.UserAuth.ConfirmUserSignUpRequest>()
            .ReverseMap();
        CreateMap<GetResetPasswordEmailRequest, CoreModels.UserAuth.GetResetPasswordEmailRequest>().ReverseMap();
    }
}