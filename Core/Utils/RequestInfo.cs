using StandardShared.Misc;

namespace Core.Utils;

using System.Linq;
using Core.Database;
using Core.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


public sealed class RequestInfo : RequestInfoGeneric
{
    public User? User;

    public RequestInfo(IHttpContextAccessor contextAccessor, IServiceScopeFactory scopeFactory) : base(contextAccessor, scopeFactory)
    {
    }

    protected override void Init()
    {
        using var db = Scope.ServiceProvider.GetRequiredService<ApplicationContext>();
        User = db.Users.Include(x => x.RefreshTokens).FirstOrDefault(x => x.Uid == UserUid);
        Token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", "");
    }

    
}

