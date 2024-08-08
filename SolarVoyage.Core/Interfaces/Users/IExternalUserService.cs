using SolarVoyage.Core.Models;
using SolarVoyage.Core.Models.User;

namespace SolarVoyage.Core.Interfaces.Users;

public interface IExternalUserService
{
    Task<Result> UpdatePasswordAsync(Guid userGuid, string currentPassword, string newPassword);
    Task<User> GetUserAsync(Guid userGuid);
}