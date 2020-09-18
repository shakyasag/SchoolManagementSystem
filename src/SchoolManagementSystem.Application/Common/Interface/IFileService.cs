using Microsoft.AspNetCore.Http;
using SchoolManagementSystem.Shared.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Application.Common.Interface
{
    public interface IFileService
    {
         Task<List<UploadFileDto>> UploadFile(List<IFormFile> uploadedFiles);
        
    }
}
