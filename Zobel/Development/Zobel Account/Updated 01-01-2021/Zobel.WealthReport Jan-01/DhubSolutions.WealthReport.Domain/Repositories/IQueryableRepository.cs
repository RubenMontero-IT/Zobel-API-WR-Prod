using DhubSolutions.Core.Domain.Data;

namespace DhubSolutions.WealthReport.Domain.Repositories
{
    public interface IQueryableRepository : ISql
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        void SetConnectionString(string connectionString);

    }
}
