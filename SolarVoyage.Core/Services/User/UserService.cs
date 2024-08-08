using SolarVoyage.Core.Interfaces.Users;
using SolarVoyage.Core.Models;

namespace SolarVoyage.Core.Services.User;

public class UserService: IUserService
{
    private readonly IExternalUserService _externalUserService;

    public UserService(IExternalUserService externalUserService)
    {
        _externalUserService = externalUserService;
    }
    
    public async Task<Result> UpdateUserPasswordAsync(Guid userGuid, string currentPassword, string newPassword)
    {
        return await _externalUserService.UpdatePasswordAsync(userGuid, currentPassword, newPassword)
            .ConfigureAwait(false);
    }

    public async Task<Models.User.User> GetUserAsync(Guid userGuid)
    {
        return await _externalUserService.GetUserAsync(userGuid).ConfigureAwait(false);
    }
}