using Microsoft.AspNetCore.DataProtection;

namespace Authentication.MinimalApis.Services
{
    public class AuthService : IAuthService
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IDataProtectionProvider dataProtectionProvider,
            IHttpContextAccessor httpContextAccessor)
        {
            _dataProtectionProvider = dataProtectionProvider;
            _httpContextAccessor = httpContextAccessor;
        }

        public void SignIn()
        {
            var protector = _dataProtectionProvider.CreateProtector("auth-cookie");
            if (_httpContextAccessor.HttpContext != null)
            {
                _httpContextAccessor.HttpContext.Response.Headers["set-cookie"] = $"auth={protector.Protect("usr:anton")}";
            }
        }
    }
}
