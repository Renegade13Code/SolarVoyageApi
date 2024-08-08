namespace SolarVoyage.API.DtoModels;

public record UserSignUpRequest
{
    //TODO: add model validation
    public string Email { get; init; }
    public string Password { get; init; }
}