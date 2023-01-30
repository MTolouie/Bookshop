using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookshop.Service.IService
{
    public interface IFileUpload
    {
        public Task<string> UploadFile(IBrowserFile file, string ImageType);


        public Task<bool> DeleteFile(string fileName, string ImageType);

    }
}
