using Microsoft.EntityFrameworkCore;

namespace DhubSolutions.Core.Infrastructure.Data.Context
{
    public interface IEntityFrameworkContextAccessor<TContext> where TContext : DbContext
    {
        /// <summary>
        /// Create a instace of DBContext
        /// </summary>
        /// <param name="nameConnectionString"></param>
        /// <returns></returns>
        TContext GetContext(string nameConnectionString);
    }

    public interface IEntityFrameworkContextAccessor : IEntityFrameworkContextAccessor<DbContext>
    {

    }
}
