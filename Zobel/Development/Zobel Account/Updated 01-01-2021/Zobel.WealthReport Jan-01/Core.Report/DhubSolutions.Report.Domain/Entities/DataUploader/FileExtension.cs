using DhubSolutions.Core.Domain.Entity;

namespace DhubSolutions.Reports.Domain.Entities.DataUploader
{
    public class FileExtension : BaseEntity
    {
        public string FileExtensionValue { get; set; }

        public string FileExtensionIcon { get; set; }
    }
}
