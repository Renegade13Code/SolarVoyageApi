using System.ComponentModel.DataAnnotations;

namespace SolarVoyage.API.DtoModels;

public class GetResetPasswordEmailRequest
{
    [Required(ErrorMessage = "Username is a required field")]
    [MinLength(1, ErrorMessage = "Username cannot be empty")]
    public string Username { get; set; }
}