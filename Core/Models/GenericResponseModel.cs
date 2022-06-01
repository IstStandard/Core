// TO SharedLib

namespace Core.Models;

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


public class GenericResponseModel : ObjectResult
{
    public GenericResponseModel(bool status, object response, int statusCode) : base(JsonConvert.SerializeObject(
        new
        {
            status = status,
            response = response
        }, Formatting.Indented))
    {
        StatusCode = statusCode;
    }
}