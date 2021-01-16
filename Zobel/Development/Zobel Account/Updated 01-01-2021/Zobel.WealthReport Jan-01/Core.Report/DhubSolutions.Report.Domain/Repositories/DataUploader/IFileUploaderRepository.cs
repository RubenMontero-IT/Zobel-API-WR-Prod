using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Core.Domain.Data.Repositories;
using DhubSolutions.Reports.Domain.Entities.DataUploader;

namespace DhubSolutions.Reports.Domain.Repositories.DataUploader
{
    public interface IFileUploaderRepository : IRepository<FileContent>
    {
        FileExtension GetFileExtension(string extension);

        string InsertStoredFile(string fileName, string period, FileExtension fileExtension, User uploadedBy, Organization organization, string description = null);

        int InsertFileContent(byte[] fileAddress, string StoreFileId);

        string SetDeleteDate(string FileContentID);
    }
}
