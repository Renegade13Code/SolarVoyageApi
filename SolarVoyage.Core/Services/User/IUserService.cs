using SolarVoyage.Core.Models;

namespace SolarVoyage.Core.Services.User;

public interface IUserService
{
    Task<Result> UpdateUserPasswordAsync(Guid userGuid, string currentPassword, string newPassword);
    Task<Models.User.User> GetUserAsync(Guid userGuid);
}