using System.ComponentModel.DataAnnotations;

namespace SolarVoyage.API.DtoModels;

public record GetSignInTokenRequest
{
    [Required(ErrorMessage = "Username is a required field")]
    [MinLength(1, ErrorMessage = "Username cannot be empty")]
    public string Username { get; init; } = string.Empty;
    
    [Required(ErrorMessage = "Password is a required field")]
    [MinLength(1, ErrorMessage = "Password cannot be empty")]
    public string Password { get; init; } = string.Empty;
}