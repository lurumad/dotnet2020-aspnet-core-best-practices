using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet2020.Api.Features.EFCore
{
    public static class CompiledQueries
    {
        public static Func<ApiDbContext, int, Task<Customer>> CustomerBy =
            EF.CompileAsyncQuery((ApiDbContext db, int id) =>
                db.Customers.SingleOrDefault(c => c.Id == id));
    }
}
