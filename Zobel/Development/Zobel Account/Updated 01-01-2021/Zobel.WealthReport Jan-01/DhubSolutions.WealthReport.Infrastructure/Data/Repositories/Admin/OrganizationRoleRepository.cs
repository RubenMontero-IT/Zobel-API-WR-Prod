using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Common.Domain.Repositories.Admin;
using DhubSolutions.Core.Domain.Data;
using DhubSolutions.Core.Infrastructure.Data.Repositories;
using DhubSolutions.WealthReport.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Repositories.Admin
{
    public class OrganizationRoleRepository : Repository<OrganizationRole>, IOrganizationRoleRepository
    {
        public OrganizationRoleRepository(ProjectManagementDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public IEnumerable<OrganizationRole> Find(Expression<Func<OrganizationRole, bool>> predicate, bool asNotracking = false)
        {
            return _dbContext.Set<OrganizationRole>().AsNoTracking()
                .Include(o => o.RoleValue)
                .Where(predicate);
        }

        public IEnumerable<SystemGroupMemberShip> GetSystemGroupMemberShips(string orgRoleId)
        {
            var systemGroupMemberShip = _dbContext.Set<SystemGroupMemberShip>()
                  .Include(s => s.SystemGroup)
                  .Include(s => s.OrganizationRole)
                  .Where(s => s.OrganizationRoleId == orgRoleId);

            return systemGroupMemberShip;
        }
    }
}
