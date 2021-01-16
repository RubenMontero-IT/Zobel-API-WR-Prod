using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Core.Domain.Entity;

namespace DhubSolutions.Reports.Domain.Entities.DataUploader
{
    public class StoredFile : BaseEntity
    {
        public string FileName { get; set; }

        public string Description { get; set; }

        public string FileExtensionId { get; set; }

        public FileExtension FileExtension { get; set; }

        public string Period { get; set; }

        public string UploadedById { get; set; }

        public User UploadedBy { get; set; }

        public string OrganizationId { get; set; }

        public Organization organization { get; set; }
    }
}
