using Core.Utils;
using Grpc.Net.Client;

namespace Core.Communicators;

public class SarfCommunicator : BaseCommunicator
{
    #region MetaData

    public class GenericResponse
    {
        public bool Status { get; init; }
        public string? Message { get; init; }
        public int StatusCode { get; init; }
    }

    public class SignInRequest
    {
        public string Username { get; init; } = null!;
        public string Password { get; init; } = null!;
    }

    public class SignInResponse : GenericResponse
    {
        public string? RefreshToken { get; init; }
        public string? JwtToken { get; init; }
    }

    public class SignUpRequest
    {
        public string Username { get; init; }
        public string Password { get; init; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? Patronymic { get; init; }
        public int? Age { get; init; }
        public long? Permissions { get; init; }
        public int? Status { get; init; }
        public string Email { get; init; }
    }

    public class SignUpResponse : GenericResponse
    {

    }

    public class LogoutResponse : GenericResponse
    {

    }

    public class IsTokenValidRequest
    {
        public string? Token { get; init; }
    }

    public class IsTokenValidReponse : GenericResponse
    {
        public Guid UserUid { get; init; }
    }

    public class RefreshRequest
    {
        public string RefreshToken { get; init; } = null!;
    }

    public class RefreshResponse : SignInResponse
    {

    }
    

    #endregion
    private readonly AuthService.AuthServiceClient _authService;

    public SarfCommunicator(SecurityProfiler securityProfiler, IConfiguration configuration) : base(securityProfiler)
    {
        var host = configuration["Sarf:Host"];
        var port = configuration["Sarf:Port"];

        var channel = GrpcChannel.ForAddress($"http://{host}:{port}");
        _authService = new AuthService.AuthServiceClient(channel);
    }

    public SignInResponse SignIn(SignInRequest model)
    {
        var result = _authService.SignIn(new Communicators.SignInRequest
        {
            Username = model.Username,
            Password = model.Password,
            Ip = SecurityProfiler.RemoteIp
        });

        return new SignInResponse
        {
            Status = result.Status,
            Message = result.Message,
            RefreshToken = result.RefreshToken,
            JwtToken = result.Token,
            StatusCode = result.StatusCode
        };
    }

    public SignUpResponse SignUp(SignUpRequest model)
    {
        var result = _authService.SignUp(new Communicators.SignUpRequest
        {
            Username = model.Username,
            Password = model.Password,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Patronymic = model.Patronymic,
            Age = model.Age,
            Email = model.Email,
            Permissions = model.Permissions,
            Ip = SecurityProfiler.RemoteIp,
            Status = model.Status
        });


        return new SignUpResponse { Status = result.Status, Message = result.Message, StatusCode = result.StatusCode };
    }

    public LogoutResponse Logout()
    {
        var result = _authService.Logout(new RequestWithTokenAndIp
        {
            Ip = SecurityProfiler.RemoteIp,
            Token = SecurityProfiler.Token
        });

        return new LogoutResponse
        {
            Status = result.Status,
            Message = result.Message,
            StatusCode = result.StatusCode
        };
    }

    public IsTokenValidReponse IsTokenValid(IsTokenValidRequest? model = null)
    {
        var result = _authService.IsTokenValid(new RequestWithToken
        {
            Token = model?.Token ?? SecurityProfiler.Token
        });



        return new IsTokenValidReponse
        {
            Status = result.Status,
            StatusCode = result.StatusCode,
            UserUid = new Guid(result.UserUid)
        };
    }

    public RefreshResponse Refresh(RefreshRequest model)
    {
        var result = _authService.Refresh(new RequestWithTokenAndIp
        {
            Ip = SecurityProfiler.RemoteIp,
            Token = model.RefreshToken
        });

        return new RefreshResponse
        {
            Status = result.Status,
            Message = result.Message,
            RefreshToken = result.RefreshToken,
            JwtToken = result.Token,
            StatusCode = result.StatusCode
        };
    }

}