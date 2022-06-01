// TO SharedLib

namespace Core.Models;
public sealed class SuccessResponseModel : GenericResponseModel
{
    public SuccessResponseModel(object response = null, int statusCode = 200) : base(true, response, statusCode)
    {
    }
}