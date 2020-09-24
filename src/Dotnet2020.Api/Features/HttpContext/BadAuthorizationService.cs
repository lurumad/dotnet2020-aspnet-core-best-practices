using Microsoft.AspNetCore.Http;

namespace Dotnet2020.Api.Features.HttpContext
{
    public class BadAuthorizationService
    {
        public const string AdminRole = "administrator";
        private readonly Microsoft.AspNetCore.Http.HttpContext httpContext;

        public BadAuthorizationService(IHttpContextAccessor httpContextAccessor)
        {
            httpContext = httpContextAccessor.HttpContext;
        }

        public bool IsAdmin()
        {
            return httpContext.User.IsInRole(AdminRole);
        }
    }
}
