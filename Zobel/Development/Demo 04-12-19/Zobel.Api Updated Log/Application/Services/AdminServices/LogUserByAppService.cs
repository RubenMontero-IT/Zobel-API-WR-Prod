using Application.Interfaces.AdminInterfaces;
using Domain.Entities.AdminEntities;
using Infrastructure.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.AdminServices
{
    public class OrganizationService : BaseGenericService<Organization>, IOrganizationService
    {
        public OrganizationService(IUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.Organizations)
        {
        }
    }
}
