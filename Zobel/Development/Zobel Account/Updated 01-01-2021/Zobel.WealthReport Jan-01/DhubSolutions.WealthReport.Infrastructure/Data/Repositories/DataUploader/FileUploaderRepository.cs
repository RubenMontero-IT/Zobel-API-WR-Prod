using DhubSolutions.Common.Domain.Entities.Admin;
using DhubSolutions.Core.Infrastructure.Data;
using DhubSolutions.Core.Infrastructure.Data.Repositories;
using DhubSolutions.Reports.Domain.Entities.DataUploader;
using DhubSolutions.Reports.Domain.Entities.DataUploader.ObjectValues;
using DhubSolutions.Reports.Domain.Repositories.DataUploader;
using DhubSolutions.WealthReport.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DhubSolutions.WealthReport.Infrastructure.Data.Repositories.DataUploader
{
    public class FileUploaderRepository : Repository<FileContent>, IFileUploaderRepository
    {
        private readonly IEntityFrameworkUnitOfWork _unitOfWork;

        public FileUploaderRepository(ProjectManagementDbContext context, IEntityFrameworkUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<FileContent> GetAll()
        {
            return _dbContext.Set<FileContent>()
                  .Include(f => f.StoredFile)
                      .ThenInclude(s => s.FileExtension)
                  .Include(f => f.StoredFile)
                      .ThenInclude(s => s.organization)
                  .Include(f => f.StoredFile)
                      .ThenInclude(s => s.UploadedBy)
                  .AsNoTracking();
        }

        public FileContent Get(string id)
        {
            return _dbContext.Set<FileContent>()
                  .Where(x => x.Id == id)
                  .Include(f => f.StoredFile)
                      .ThenInclude(s => s.FileExtension)
                  .Include(f => f.StoredFile)
                      .ThenInclude(s => s.organization)
                  .Include(f => f.StoredFile)
                      .ThenInclude(s => s.UploadedBy)
                  .AsNoTracking()
                  .SingleOrDefault();
        }

        public FileExtension GetFileExtension(string extension)
        {

            var parameters = new { extension };

            string query = $"Select * from [dbi].[FileExtension] where FileExtensionValue=@extension";

            FileExtensionObjectValue fileExtension = _unitOfWork.ExecuteQuery<FileExtensionObjectValue>(query, parameters).SingleOrDefault();

            return fileExtension == null ? null : new FileExtension
            {
                Id = fileExtension.FileExtensionID,
                FileExtensionIcon = fileExtension.FileExtensionIcon,
                FileExtensionValue = fileExtension.FileExtensionValue
            };

        }

        public string InsertStoredFile(string fileName, string period, FileExtension fileExtension, User uploadedBy, Organization organization, string description = null)
        {
            var storedFileID = $"{Guid.NewGuid()}";

            var parameters = new
            {
                storedFileID,
                fileName,
                filExtensionId = fileExtension.Id,
                uploadeById = uploadedBy.Id,
                organizationId = organization.Id,
                period,
            };

            string query = $"Insert into [dbi].[StoredFile] (StoredFileID,FileName, FileExtensionID, UploadedByID, OrganizationID, PeriodID) " +
                                        $"Values (@storedFileID, @fileName, @fileExtensionId, @uploadedById, @organizationId, @period)";

            var rowAffected = _unitOfWork.ExecuteCommand(query, parameters);
            if (rowAffected > 0)
                return storedFileID;

            return string.Empty;

        }

        public int InsertFileContent(byte[] fileContent, string storeFileId)
        {
            var parameters = new { StoredFileID = storeFileId, FileContent = fileContent };

            return _unitOfWork.ExecuteCommand("[dbi].[InsertFile]", parameters, commandType: CommandType.StoredProcedure);
        }

        public string SetDeleteDate(string FileContentID)
        {

            var parameters = new { FileContentID };
            string query = $"UPDATE [dbi].[FileContent] SET Deleted = GETDATE()  WHERE FileContentID = @FileContentID  AND Deleted IS NULL";
            var rowAffected = _unitOfWork.ExecuteCommand(query, parameters);
            if (rowAffected > 0)
                return FileContentID;
            return string.Empty;

        }


    }
}

