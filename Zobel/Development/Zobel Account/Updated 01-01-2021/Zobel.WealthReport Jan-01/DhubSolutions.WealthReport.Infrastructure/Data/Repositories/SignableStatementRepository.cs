using DhubSolutions.Core.Domain.Data;
using DhubSolutions.Core.Infrastructure.Data.Repositories;
using DhubSolutions.WealthReport.Domain.Entities;
using DhubSolutions.WealthReport.Domain.Repositories;
using DhubSolutions.WealthReport.Infrastructure.Data.Context;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Repositories
{
    public class SignableStatementRepository : Repository<SignableStatement>, ISignableStatementRepository
    {
        public SignableStatementRepository(ProjectManagementDbContext dbContext, IUnitOfWork unitOfWork)
            : base(dbContext, unitOfWork)
        { }
    }
}
