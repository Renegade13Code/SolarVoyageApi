namespace SolarVoyage.Core.Models.UserAuth;

public record UserAuthToken
{
    public string AccessToken { get; init; }
    public int ExpiresIn { get; init; }
    public string IdToken { get; init; }
    public string RefreshToken { get; init; }
    public string TokenType { get; init; }
}