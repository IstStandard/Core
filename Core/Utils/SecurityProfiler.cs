namespace Core.Utils;

using System;
using System.Linq;
using Core.Database;
using Core.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


public class SecurityProfiler
{
    private readonly IServiceScope _scope;
    private readonly HttpContext _httpContext;
    public User? User;
    public string RemoteIp = null!;
    public string? Token;

    public SecurityProfiler(IHttpContextAccessor contextAccessor, IServiceScopeFactory scopeFactory)
    {
        _scope = scopeFactory.CreateScope();
        _httpContext = contextAccessor.HttpContext!;

        Init();
    }

    private void Init()
    {
        using var db = _scope.ServiceProvider.GetRequiredService<ApplicationContext>();
        var userUid = _httpContext?.Items["userUid"];
        RemoteIp = _httpContext!.Connection.RemoteIpAddress!.ToString();

       if (userUid == null)
            return;

        User = db.Users.Include(x => x.RefreshTokens).FirstOrDefault(x => x.Uid == (Guid)userUid);
        Token = _httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", "");
    }
}

