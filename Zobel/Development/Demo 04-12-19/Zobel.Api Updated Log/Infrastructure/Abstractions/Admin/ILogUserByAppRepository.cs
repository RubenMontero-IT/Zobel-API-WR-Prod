using Domain.Entities.AdminEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Abstractions.Admin
{
    public interface IOrganizationRepository : IRepository<Organization>
    {
    }
}
