using System.Globalization;
using System.Net;
using Amazon.AspNetCore.Identity.Cognito.Extensions;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.CognitoIdentityProvider.Model.Internal.MarshallTransformations;
using AutoMapper;
using Microsoft.Extensions.Options;
using SolarVoyage.Aws.Cognito.Service.Models;
using SolarVoyage.Core.Interfaces.UserAuth;
using SolarVoyage.Core.Models;
using ExternalServiceException = Amazon.CognitoIdentity.Model.ExternalServiceException;
using CoreModels = SolarVoyage.Core.Models.UserAuth;
using System.Security.Cryptography;
using System.Text;
using Amazon.Runtime.Internal.Transform;
using SolarVoyage.Aws.Cognito.Service.Utils;

namespace SolarVoyage.Aws.Cognito.Service.Services;

public class AwsCognitoUserAuthService: IExternalAuthService
{
    private readonly IAmazonCognitoIdentityProvider _cognitoService;
    private readonly IMapper _mapper;
    private readonly AwsCognitoUserPoolOptions _options;

    public AwsCognitoUserAuthService(IAmazonCognitoIdentityProvider cognitoService, IOptions<AwsCognitoUserPoolOptions> options, IMapper mapper)
    {
        _cognitoService = cognitoService;
        _mapper = mapper;
        _options = options.Value;
    }

    public async Task<Result<Guid>> SignUpAsync(CoreModels::UserSignUpRequest request, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        try
        {
            var userAttrs = new List<AttributeType>()
            {
                new(){
                    Name = "email",
                    Value = request.Email,
                }
            };

            var signUpRequest = new SignUpRequest
            {
                UserAttributes = userAttrs,
                Username = request.Email,
                ClientId = _options.ClientId, 
                SecretHash = HmacHelper.ComputeBase64HmacSha256(_options.ClientSecret, request.Email, _options.ClientId),
                Password = request.Password
            };

            var response = await _cognitoService.SignUpAsync(signUpRequest, ct).ConfigureAwait(false);
            return Result.Success<Guid>(Guid.Parse(response.UserSub));
        }
        catch (Exception ex)
        {
            //log error
            throw new ExternalServiceException("Error occurred during sign up operation", ex);
        }
    }

    public async Task<Result> ConfirmSignUpAsync(string confirmationCode, string username, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        ArgumentException.ThrowIfNullOrEmpty(confirmationCode);
        ArgumentException.ThrowIfNullOrEmpty(username);
        try
        {
            var request = new ConfirmSignUpRequest
            {
                ClientId = _options.ClientId,
                SecretHash = HmacHelper.ComputeBase64HmacSha256(_options.ClientSecret, username, _options.ClientId),
                ConfirmationCode = confirmationCode,
                Username = username
            };
            var response = await _cognitoService.ConfirmSignUpAsync(request, ct).ConfigureAwait(false);

            if (response.HttpStatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Http status code did not indicate success: {response.HttpStatusCode}");
            }
            
            return Result.Success();
        }
        catch (Exception ex)
        {
            // Log error
            throw new ExternalServiceException("Error occurred during confirm sign up operation", ex);
        }
    }

    public async Task<Result> ResendConfirmationCodeAsync(string username, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        ArgumentException.ThrowIfNullOrEmpty(username);
        try
        {
            var request = new ResendConfirmationCodeRequest()
            {
                ClientId = _options.ClientId,
                SecretHash = HmacHelper.ComputeBase64HmacSha256(_options.ClientSecret, username, _options.ClientId),
                Username = username
            };
            var response = await _cognitoService.ResendConfirmationCodeAsync(request, ct).ConfigureAwait(false);

            if (response.HttpStatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Http status code did not indicate success: {response.HttpStatusCode}");
            }
            
            return Result.Success();
        }
        catch (Exception ex)
        {
            // Log error
            throw new ExternalServiceException("Error occurred during resend confirmation code operation", ex);
        }
    }

    public async Task<Result> ForgotPasswordAsync(string username, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNullOrEmpty(username);
        try
        {
            var request = new ForgotPasswordRequest()
            {
                ClientId = _options.ClientId,
                SecretHash = HmacHelper.ComputeBase64HmacSha256(_options.ClientSecret, username, _options.ClientId),
                Username = username
            };
            var response = await _cognitoService.ForgotPasswordAsync(request, ct).ConfigureAwait(false);

            if (response.HttpStatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Http status code did not indicate success: {response.HttpStatusCode}");
            }
            
            return Result.Success();
        }
        catch (Exception ex)
        {
            // Log error
            throw new ExternalServiceException("Error occurred during forgot password operation", ex);
        }
        
    }

    public async Task<Result<CoreModels::UserAuthToken>> PasswordSignInAsync(string username, string password, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        ArgumentException.ThrowIfNullOrEmpty(password);
        ArgumentException.ThrowIfNullOrEmpty(username);

        try
        {
            var authParameters = new Dictionary<string, string>();
            authParameters.Add("USERNAME", username);
            authParameters.Add("PASSWORD", password);
            authParameters.Add("SECRET_HASH", HmacHelper.ComputeBase64HmacSha256(_options.ClientSecret, username, _options.ClientId));

            var request = new InitiateAuthRequest()
            {
                ClientId = _options.ClientId,
                AuthParameters = authParameters,
                AuthFlow = AuthFlowType.USER_PASSWORD_AUTH
            };

            var signInResult = await _cognitoService.InitiateAuthAsync(request, ct).ConfigureAwait(false);

            CoreModels::UserAuthToken result;
            //TODO:: review if this is really needed. Should only be needed if users are created on cognito UI
            if (signInResult.ChallengeName == ChallengeNameType.NEW_PASSWORD_REQUIRED)
            {
                var respondToAuthChallengeRequest = new RespondToAuthChallengeRequest()
                {
                    ChallengeName = ChallengeNameType.NEW_PASSWORD_REQUIRED,
                    ClientId = _options.ClientId,
                    ChallengeResponses = new Dictionary<string, string>()
                    {
                        { "USERNAME", username },
                        { "NEW_PASSWORD", password },
                        { "userAttributes.family_name", "surname"},
                        { "userAttributes.given_name", "firstname"}
                    },
                    Session = signInResult.Session
                };
                var authChallengeResponse = await _cognitoService.RespondToAuthChallengeAsync(respondToAuthChallengeRequest)
                    .ConfigureAwait(false);
                result = _mapper.Map<CoreModels::UserAuthToken>(authChallengeResponse.AuthenticationResult);
                return Result.Success<CoreModels::UserAuthToken>(result);
            }
            
            result = _mapper.Map<CoreModels::UserAuthToken>(signInResult.AuthenticationResult);
            return Result.Success<CoreModels::UserAuthToken>(result);
        }
        catch (Exception ex)
        {
            // Log error
            throw new ExternalServiceException("Error occurred during password sign in operation", ex);
        }
    }
}