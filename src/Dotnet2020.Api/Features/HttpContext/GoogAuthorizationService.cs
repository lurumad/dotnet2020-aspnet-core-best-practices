using Microsoft.AspNetCore.Http;

namespace Dotnet2020.Api.Features.HttpContext
{
    public class AuthorizationService
    {
        public const string AdminRole = "administrator";
        private readonly IHttpContextAccessor httpContextAccessor;

        public AuthorizationService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor ?? throw new System.ArgumentNullException(nameof(httpContextAccessor));
        }

        public bool IsAdmin()
        {
            var httpContext = httpContextAccessor.HttpContext;

            return httpContext is object
                   && httpContextAccessor.HttpContext.User.IsInRole(AdminRole);
        }
    }
}
