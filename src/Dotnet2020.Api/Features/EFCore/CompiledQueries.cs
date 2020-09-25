using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet2020.Api.Features.EFCore
{
    public static class CompiledQueries
    {
        /// <summary>
        /// For high-scale scenarios using EF, sometimes our queries are complex using many JOINS, Includes, filters, etc.
        /// EF Core can automatically compile and cache queries, but sometimes we can obtain a performance gain bypassing the computation
        /// of the hash of the query and the cache lookup allowing our applications to use a compiled query. This example is only to 
        /// show how to create a compiled query but we know it's a simple query.
        /// </summary>
        public static Func<ApiDbContext, int, Task<Customer>> CustomerBy =
            EF.CompileAsyncQuery((ApiDbContext db, int id) =>
                db.Customers.SingleOrDefault(c => c.Id == id));
    }
}
