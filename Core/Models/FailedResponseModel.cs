// TO SharedLib

namespace Core.Models;

public sealed class FailedResponseModel : GenericResponseModel
{
    public FailedResponseModel(string error, int statusCode = 400) : base(false, new
    {
        Error = error
    }, statusCode)
    {
    }
}