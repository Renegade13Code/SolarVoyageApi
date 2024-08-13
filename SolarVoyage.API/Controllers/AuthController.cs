using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolarVoyage.API.DtoModels;
using SolarVoyage.Core.Services.UserAuth;
using CoreModels = SolarVoyage.Core.Models;

namespace SolarVoyage.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController: ControllerBase
{
    private readonly IUserAuthService _authService;
    private readonly IMapper _mapper;

    public AuthController(IUserAuthService authService, IMapper _mapper)
    {
        _authService = authService;
        this._mapper = _mapper;
    }
    
    //TODO: setup exception mapping to 500
    [HttpPost]
    [Route("signup")]
    public async Task<IActionResult> UserSignUp([FromBody] UserSignUpRequest request, CancellationToken ct)
    {
        var signUpResult = await _authService.SignUpAsync(_mapper.Map<CoreModels::UserAuth.UserSignUpRequest>(request), ct)
            .ConfigureAwait(false);
        return Ok(signUpResult.Value);
        //TODO:Return 204 with url
        // return Created(signUpResult.Value);
    }

    [HttpPost]
    [Route("signup/confirmation-code")]
    public async Task<IActionResult> ConfirmUserSignUp([FromBody] ConfirmUserSignUpRequest confirmUserSignUpRequest,
        CancellationToken ct)
    {
        var result = await _authService
            .ConfirmSignUpAsync(_mapper.Map<CoreModels::UserAuth.ConfirmUserSignUpRequest>(confirmUserSignUpRequest), ct)
            .ConfigureAwait(false);
        
        return result.Succeeded ? Ok() : StatusCode(500);
    }
    
    [HttpGet]
    [Route("signup/confirmation-code")]
    public async Task<IActionResult> ResendConfirmationCode([FromQuery] string username, CancellationToken ct)
    {
        var result = await _authService
            .ResendConfirmationCodeAsync(username, ct)
            .ConfigureAwait(false);
        
        return result.Succeeded ? Ok() : StatusCode(500);
    }

    [HttpGet]
    [Route("signin/reset-password")]
    public async Task<IActionResult> GetResetPasswordEmailAsync([FromBody] GetResetPasswordEmailRequest request,
        CancellationToken ct)
    {
        var result = await _authService.ForgotPasswordAsync(_mapper.Map<CoreModels.UserAuth.GetResetPasswordEmailRequest>(request), ct)
            .ConfigureAwait(false);
        return Ok(result);
    }
    
    [HttpGet]
    [Route("signin/user-token")]
    public async Task<IActionResult> GetSignInTokenAsync([FromBody] GetSignInTokenRequest request, CancellationToken ct)
    {
        var signInResult = await _authService.PasswordSignInAsync(request.Username, request.Password, ct).ConfigureAwait(false);
        return Ok(signInResult.Value);
    }
    
    [Authorize]
    [HttpGet]
    [Route("Test")]
    public Task<IActionResult> Test()
    {
        return Task.FromResult<IActionResult>(Ok("success :)"));
    }
    
    [HttpGet]
    [Route("Test/Unauth")]
    public Task<IActionResult> UnauthTest()
    {
        return Task.FromResult<IActionResult>(Ok("success :)"));
    }
}