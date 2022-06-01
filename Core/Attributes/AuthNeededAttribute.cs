namespace Eah.source.auth.attributes;

using System;
using System.Linq;
using System.Net;
using Core.Communicators;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthNeededAttribute : Attribute, IAuthorizationFilter
{
    private SarfCommunicator _sarfCommunicator = null!;
    public void OnAuthorization(AuthorizationFilterContext filterContext)
    {
        _sarfCommunicator = filterContext.HttpContext.RequestServices.GetService(typeof(SarfCommunicator)) as SarfCommunicator;

        filterContext.HttpContext.Request.Headers.TryGetValue("Authorization", out var authTokens);
        var token = authTokens.FirstOrDefault();

        if (token != null)
        {
            var authToken = token.Replace("Bearer", "").Trim();
            var isValid = _sarfCommunicator!.IsTokenValid(new SarfCommunicator.IsTokenValidRequest { Token = authToken});

            if (!isValid.Status)
            {
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                filterContext.Result = new JsonResult("NotAuthorized")
                {
                    Value = new
                    {
                        Status = false,
                        Message = "Invalid Token" // TODO: Resx
                    },
                };
            }
            else
                filterContext.HttpContext.Items["userUid"] = isValid.UserUid;

        }
        else
        {
            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;

            filterContext.Result = new JsonResult("Please Provide authToken")
            {
                Value = new
                {
                    Status = false,
                    Message = "Please Provide authToken" // TODO: Resx
                },
            };
        }
    }
}