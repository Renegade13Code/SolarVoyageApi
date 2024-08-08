using Amazon.Extensions.CognitoAuthentication;
using AutoMapper;

namespace StickerFactory.API.AWS.Cognito.Service.AutoMapper.CustomResolvers;

internal class UserEmailResolver : IValueResolver<CognitoUser, Core.Models.User, string>
{
    public string Resolve(CognitoUser source, Core.Models.User destination, string destMember, ResolutionContext context)
    {
        if (source.Attributes.TryGetValue("email", out var firstName))
        {
            return firstName;
        }
        return string.Empty;
    }
}