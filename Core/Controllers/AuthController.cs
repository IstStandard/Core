using Core.Communicators;
using Core.Models;
using Eah.source.auth.attributes;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers;

[ApiController]
[Route("/api/auth")]
public class AuthController : ControllerBase
{
    private readonly SarfCommunicator _sarfCommunicator;
    public AuthController(SarfCommunicator sarfCommunicator)
    {
        _sarfCommunicator = sarfCommunicator;
    }

    [HttpPost("sign_in")]
    public IActionResult OnSignIn([FromBody] SarfCommunicator.SignInRequest model)
    {
        var result = _sarfCommunicator.SignIn(model);
        return new GenericResponseModel(result.Status, new
        {
            Message = result.Message,
            RefreshToken = result.RefreshToken,
            JwtToken = result.JwtToken
        }, result.StatusCode);
    }

    [HttpPost("sign_up")]
    public IActionResult OnSignUp([FromBody] SarfCommunicator.SignUpRequest model)
    {
        var result = _sarfCommunicator.SignUp(model);
        return new GenericResponseModel(result.Status, new
        {
            Message = result.Message
        }, result.StatusCode);
    }

    [AuthNeeded]
    [HttpPost("logout")]
    public IActionResult OnLogout()
    {
        var result = _sarfCommunicator.Logout();
        return new GenericResponseModel(result.Status, new
        {
            Message = result.Message
        }, result.StatusCode);
    }

    [HttpPost("refresh")]
    public IActionResult OnRefresh([FromBody] SarfCommunicator.RefreshRequest model)
    {
        var result = _sarfCommunicator.Refresh(model);
        return new GenericResponseModel(result.Status, new
        {
            Message = result.Message,
            RefreshToken = result.RefreshToken,
            JwtToken = result.JwtToken
        }, result.StatusCode);
    }

    [AuthNeeded]
    [HttpPost("is_token_valid")]
    public IActionResult OnTokenValid()
    {
        var result = _sarfCommunicator.IsTokenValid();
        return new GenericResponseModel(result.Status, new
        {
            Message = String.Empty
        }, result.StatusCode);
    }


}