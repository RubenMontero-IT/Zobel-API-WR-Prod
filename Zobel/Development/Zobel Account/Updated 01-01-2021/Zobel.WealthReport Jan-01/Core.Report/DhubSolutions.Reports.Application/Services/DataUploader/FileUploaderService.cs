using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Core.Application.Services;
using DhubSolutions.Core.Domain.Adapters;
using DhubSolutions.Core.Domain.Data;
using DhubSolutions.Reports.Application.Services.DataUploader.Base;
using DhubSolutions.Reports.Domain.Entities.DataUploader;
using DhubSolutions.Reports.Domain.Repositories.DataUploader;

namespace DhubSolutions.Reports.Application.Services.DataUploader
{
    public class FileUploaderService : ServiceMapper<FileContent>, IFileUploaderService
    {
        private IFileUploaderRepository fileUploaderRepository;
        public FileUploaderService(IUnitOfWork unitOfWork, ITypeAdapter typeAdapter, IFileUploaderRepository repository)
            : base(unitOfWork, typeAdapter, repository)
        {
            fileUploaderRepository = repository;
        }

        public FileExtension GetFileExtension(string extension)
        {
            return fileUploaderRepository.GetFileExtension(extension);
        }

        public int InsertFileContent(byte[] fileContent, string storeFileId)
        {
            return fileUploaderRepository.InsertFileContent(fileContent, storeFileId);
        }

        public string InsertStoredFile(string fileName, string period, FileExtension fileExtension, User uploadedBy, Organization organization, string description = null)
        {
            return fileUploaderRepository.InsertStoredFile(fileName, period, fileExtension, uploadedBy, organization, description);
        }

        public string SetDeleteDate(string FileContentID)
        {
            return fileUploaderRepository.SetDeleteDate(FileContentID);
        }
    }
}
