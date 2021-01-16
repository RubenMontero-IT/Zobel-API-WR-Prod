using DhubSolutions.Core.Domain.Entity;

using System;

namespace DhubSolutions.Reports.Domain.Entities.DataUploader
{
    public class FileContent : BaseEntity
    {
        public DateTime UploadDate { get; set; }

        public byte[] Content { get; set; }

        public string StoredFileId { get; set; }

        public StoredFile StoredFile { get; set; }

    }
}
