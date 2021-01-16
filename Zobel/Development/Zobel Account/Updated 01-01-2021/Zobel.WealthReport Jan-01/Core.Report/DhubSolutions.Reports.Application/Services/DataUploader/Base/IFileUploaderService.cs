using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Core.Domain.Services;
using DhubSolutions.Reports.Domain.Entities.DataUploader;

namespace DhubSolutions.Reports.Application.Services.DataUploader.Base
{
    public interface IFileUploaderService : IServiceMapper<FileContent>
    {
        FileExtension GetFileExtension(string extension);

        string InsertStoredFile(string fileName, string period, FileExtension fileExtension, User uploadedBy, Organization organization, string description = null);

        int InsertFileContent(byte[] fileContent, string storeFileId);

        string SetDeleteDate(string FileContentID);
    }
}
