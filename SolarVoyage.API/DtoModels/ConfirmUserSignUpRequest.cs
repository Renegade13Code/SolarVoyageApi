using System.ComponentModel.DataAnnotations;

namespace SolarVoyage.API.DtoModels;

public class ConfirmUserSignUpRequest
{
    [Required(ErrorMessage = "ConfirmationCode is a required field")]
    [MinLength(1, ErrorMessage = "ConfirmationCode cannot be empty")]
    public string ConfirmationCode { get; init; } = string.Empty;
    
    [Required(ErrorMessage = "Username is a required field")]
    [MinLength(1, ErrorMessage = "Username cannot be empty")]
    public string Username { get; init; } = string.Empty;
}