namespace SolarVoyage.Core.Models.UserAuth;

public class ConfirmUserSignUpRequest
{
    public string ConfirmationCode { get; init; } = string.Empty;
    public string Username { get; init; } = string.Empty;
}