using System.Security.Claims;
using Amazon.Extensions.CognitoAuthentication;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StickerFactory.API.AWS.Cognito.Service.Models;
using StickerFactory.API.Core.Interfaces.Users;
using Core = StickerFactory.API.Core.Models;

namespace StickerFactory.API.AWS.Cognito.Service.Services;

public class AwsCognitoUserService : IExternalUserService
{
    private readonly UserManager<CognitoUser> _userManager;
    private readonly IMapper _mapper;

    public AwsCognitoUserService(UserManager<CognitoUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }
    
    public async Task<Core::Result> UpdatePasswordAsync(Guid userGuid, string currentPassword, string newPassword)
    {
        ArgumentNullException.ThrowIfNull(userGuid);
        ArgumentNullException.ThrowIfNull(currentPassword);
        ArgumentNullException.ThrowIfNull(newPassword);

        try
        {
            var user = await _userManager.FindByIdAsync(userGuid.ToString()).ConfigureAwait(false);
            var identityResult = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword)
                .ConfigureAwait(false);
            
            if (!identityResult.Succeeded)
            {
                return Core::Result.Fail(identityResult.Errors.Select(x => x.Description));
            }

            return Core::Result.Success();
        }
        catch (Exception ex)
        {
            //TODO: Log Error
            throw new ExternalServiceException("Exception occured updating Cognito user password", ex);
        }
    }
    
    public async Task<Core::User> GetUserAsync(Guid userGuid)
    {
        try
        {
            var claimPrinciple = new ClaimsPrincipal(new List<ClaimsIdentity>()
            {
                new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, userGuid.ToString())
                })
            });
            var cognitoUser = await _userManager.GetUserAsync(claimPrinciple).ConfigureAwait(false);
        
            //Map Response
            return _mapper.Map<Core::User>(cognitoUser);
        }
        catch (Exception ex)
        {
            //TODO: Log Error    
            throw new ExternalServiceException("Exception occured retrieving Cognito user", ex);
        }
    }
}