namespace SolarVoyage.Core.Models.UserAuth;

public record UserSignUpRequest
{
    public string Email { get; init; }
    public string Password { get; init; }
    public string FirstName { get; init; }
    public string FamilyName { get; init; }
}