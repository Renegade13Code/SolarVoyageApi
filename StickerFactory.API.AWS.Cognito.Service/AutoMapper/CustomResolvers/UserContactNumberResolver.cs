using Amazon.Extensions.CognitoAuthentication;
using AutoMapper;

namespace StickerFactory.API.AWS.Cognito.Service.AutoMapper.CustomResolvers;

internal class UserContactNumberResolver : IValueResolver<CognitoUser, Core.Models.User, string>
{
    public string Resolve(CognitoUser source, Core.Models.User destination, string destMember, ResolutionContext context)
    {
        if (source.Attributes.TryGetValue("phone_number", out var firstName))
        {
            return firstName;
        }
        return string.Empty;
    }
}