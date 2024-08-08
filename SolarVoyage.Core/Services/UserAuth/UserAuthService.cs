using SolarVoyage.Core.Interfaces.UserAuth;
using SolarVoyage.Core.Models;
using SolarVoyage.Core.Models.UserAuth;

namespace SolarVoyage.Core.Services.UserAuth;

public class UserAuthService : IUserAuthService
{
    private readonly IExternalAuthService _userAuthService;

    public UserAuthService(IExternalAuthService userAuthService)
    {
        _userAuthService = userAuthService;
    }

    public Task<Result<Guid>> SignUpAsync(UserSignUpRequest request, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);
        
        return _userAuthService.SignUpAsync(request, ct);
    }

    public Task<Result> ConfirmSignUpAsync(ConfirmUserSignUpRequest confirmUserSignUpRequest, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(confirmUserSignUpRequest);

        return _userAuthService.ConfirmSignUpAsync(confirmUserSignUpRequest.ConfirmationCode, confirmUserSignUpRequest.Username, ct);
    }

    public Task<Result> ResendConfirmationCodeAsync(string username, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNullOrEmpty(username);

        return _userAuthService.ResendConfirmationCodeAsync(username, ct);
    }

    public Task<Result> ForgotPasswordAsync(GetResetPasswordEmailRequest request, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        return _userAuthService.ForgotPasswordAsync(request.Username, ct);
    }

    public Task<Result<UserAuthToken>> PasswordSignInAsync(string username, string password, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        ArgumentException.ThrowIfNullOrEmpty(username);
        ArgumentException.ThrowIfNullOrEmpty(password);

        return _userAuthService.PasswordSignInAsync(username, password, ct);
    }
}