using System;
using System.Collections.Generic;

namespace Dotnet2020.Api.Features.DependencyInjection
{
    public class MultitenantService
    {
        public IEnumerable<Guid> GetTenantIds()
        {
            return new[] { Guid.NewGuid(), Guid.NewGuid() };
        }
    }
}
