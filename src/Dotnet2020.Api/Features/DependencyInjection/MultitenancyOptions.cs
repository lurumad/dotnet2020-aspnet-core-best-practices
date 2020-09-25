using System;
using System.Collections.Generic;

namespace Dotnet2020.Api.Features.DependencyInjection
{
    public class MultitenancyOptions
    {
        public IEnumerable<Guid> TenantIds { get; set; }
    }
}
