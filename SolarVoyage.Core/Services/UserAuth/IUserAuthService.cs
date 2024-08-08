using SolarVoyage.Core.Models;
using SolarVoyage.Core.Models.UserAuth;

namespace SolarVoyage.Core.Services.UserAuth;

public interface IUserAuthService
{
    Task<Result<UserAuthToken>> PasswordSignInAsync(string username, string password, CancellationToken ct);
    Task<Result> ConfirmSignUpAsync(ConfirmUserSignUpRequest confirmUserSignUpRequest, CancellationToken ct);
    Task<Result> ResendConfirmationCodeAsync(string username, CancellationToken ct);
    Task<Result> ForgotPasswordAsync(GetResetPasswordEmailRequest request, CancellationToken ct);
    Task<Result<Guid>> SignUpAsync(UserSignUpRequest request, CancellationToken ct);
}