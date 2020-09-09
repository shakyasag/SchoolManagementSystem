using Microsoft.AspNetCore.Http;
using SchoolManagementSystem.Shared.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Application.Common.Interface
{
    public interface IFile
    {
        Task<List<UploadFileDto>> Save(List<IFormFile> Uploadfiles, string destinationPath);
    }
}
