using Microsoft.AspNetCore.Http;
using SchoolManagementSystem.Application.Common.Error;
using SchoolManagementSystem.Application.Common.Interface;
using SchoolManagementSystem.Shared.Common;
using SchoolManagementSystem.Shared.Common.Constant;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Infrastructures.Common
{
        public class FileServices: IFileService
      {
        private readonly IFile _file;

        public FileServices(IFile file)
        {
            _file = file;
        }


        public async Task<List<UploadFileDto>> UploadFile(List<IFormFile> uploadedFiles)
        {
            List<string> acceptedExtension = new List<string>() { ".jpg", ".jpeg", ".png", ".pdf", ".doc", ".docx", ".ppt", ".pptx", ".xls", ".xlsx" };

            if (uploadedFiles == null)
                throw new RestException(HttpStatusCode.NotFound, new { error = "Files not selected." } );

            foreach (var files in uploadedFiles)
            {
                if (!acceptedExtension.Contains(Path.GetExtension(files.FileName).ToLower()))
                {
                    throw new RestException(HttpStatusCode.BadRequest,new {errors= "Uploaded file extension not acceptable" });
                }
            }

            var webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var filePath = Path.Combine(webRootPath, StaticFileDirectory.UPLOAD_FILES);

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            List<UploadFileDto> listUploadFileDtos = await _file.Save(uploadedFiles, filePath);

            return listUploadFileDtos;
        }
    }
}
