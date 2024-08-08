using Amazon.Extensions.CognitoAuthentication;
using AutoMapper;

namespace SolarVoyage.Aws.Cognito.Service.AutoMapper.CustomResolvers;

internal class UserEmailResolver : IValueResolver<CognitoUser, Core.Models.User.User, string>
{
    public string Resolve(CognitoUser source, Core.Models.User.User destination, string destMember, ResolutionContext context)
    {
        if (source.Attributes.TryGetValue("email", out var email))
        {
            return email;
        }
        return string.Empty;
    }
}