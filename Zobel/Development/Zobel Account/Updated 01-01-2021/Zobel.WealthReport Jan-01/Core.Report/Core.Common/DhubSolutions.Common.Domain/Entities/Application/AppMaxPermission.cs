using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Core.Domain.Entity;

namespace DhubSolutions.Common.Domain.Entities.Application
{
    public class AppMaxPermission : BaseEntity
    {
        public AppMaxPermission() : base()
        {

        }
        public string Userid { get; set; }
        public string MaxPermission { get; set; }

        public Apps App { get; set; }
        public Permission MaxPermissionNavigation { get; set; }
        public User User { get; set; }
    }
}
