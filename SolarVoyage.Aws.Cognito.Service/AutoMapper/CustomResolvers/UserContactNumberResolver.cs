using Amazon.Extensions.CognitoAuthentication;
using AutoMapper;

namespace SolarVoyage.Aws.Cognito.Service.AutoMapper.CustomResolvers;

internal class UserContactNumberResolver : IValueResolver<CognitoUser, Core.Models.User.User, string>
{
    public string Resolve(CognitoUser source, Core.Models.User.User destination, string destMember, ResolutionContext context)
    {
        if (source.Attributes.TryGetValue("phone_number", out var firstName))
        {
            return firstName;
        }
        return string.Empty;
    }
}