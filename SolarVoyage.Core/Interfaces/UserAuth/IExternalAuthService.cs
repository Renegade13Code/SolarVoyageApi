using SolarVoyage.Core.Models;
using SolarVoyage.Core.Models.UserAuth;

namespace SolarVoyage.Core.Interfaces.UserAuth;

public interface IExternalAuthService
{
    Task<Result<UserAuthToken>> PasswordSignInAsync(string username, string password, CancellationToken ct);
    Task<Result> ConfirmSignUpAsync(string confirmationCode, string username, CancellationToken ct);
    Task<Result> ResendConfirmationCodeAsync(string username, CancellationToken ct);
    Task<Result> ForgotPasswordAsync(string username, CancellationToken ct);
    Task<Result<Guid>> SignUpAsync(UserSignUpRequest request, CancellationToken ct);
}