using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Options;

namespace StickerFactory.API.AWS.Cognito.Service.Models;

public class AwsCognitoUserPoolOptions: IOptions<AwsCognitoUserPoolOptions>
{
    public static readonly string Section = "AWS";
    public AwsCognitoUserPoolOptions Value => this;
    
    public string Region {get; set;}
    [Required]
    public string UserPoolClientId { get; set; }
    public string UserPoolClientSecret { get; set; }
    public string UserPoolId { get; set; }
}