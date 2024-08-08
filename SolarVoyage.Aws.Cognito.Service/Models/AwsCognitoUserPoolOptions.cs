using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Options;

namespace SolarVoyage.Aws.Cognito.Service.Models;

public class AwsCognitoUserPoolOptions: IOptions<AwsCognitoUserPoolOptions>
{
    public static readonly string Section = "AWS";
    public AwsCognitoUserPoolOptions Value => this;
    
    public string Region {get; set;}
    [Required]
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string UserPoolId { get; set; }
}