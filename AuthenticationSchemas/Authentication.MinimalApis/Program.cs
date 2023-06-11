using Authentication.MinimalApis.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDataProtection();
//builder.Services.AddHttpContextAccessor();

//builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddAuthentication("cookie").AddCookie("cookie");

var app = builder.Build();

//app.Use((ctx, next) =>
//{
//    var dataProtectionProvider = ctx.RequestServices.GetRequiredService<IDataProtectionProvider>();

//    var protector = dataProtectionProvider.CreateProtector("auth-cookie");
//    var authCookie = ctx.Request.Headers.Cookie.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x) && x.StartsWith("auth="));
//    if (!string.IsNullOrWhiteSpace(authCookie))
//    {
//        var protectedPayload = authCookie.Split("=").Last();
//        if (!string.IsNullOrWhiteSpace(protectedPayload))
//        {
//            var payload = protector.Unprotect(protectedPayload);
//            var parts = payload.Split(":");
//            if (parts != null && parts.Length > 1)
//            {
//                var key = parts[0];
//                var value = parts[1];

//                var claims = new List<Claim>() {
//                    new Claim(key, value),
//                };
//                var identity = new ClaimsIdentity(claims);

//                ctx.User = new ClaimsPrincipal(identity);
//            }
//        }
//    }
//    return next();
//});

app.UseAuthentication();

app.MapGet("/username", (HttpContext httpContext) =>
{
    return httpContext.User.FindFirstValue("usr");
});

app.MapGet("/login", async (HttpContext httpContext) =>
{
    var claims = new List<Claim>() { new Claim("usr", "anton") };
    var identity = new ClaimsIdentity(claims, "cookie");

    var user = new ClaimsPrincipal(identity);
    await httpContext.SignInAsync("cookie", user);
    return "ok";
});

//app.MapGet("/login", (IAuthService authService) =>
//{
//    authService.SignIn();
//    return "ok";
//});

app.Run();