using Bookshop.Service.IService;
using BookShop_Common;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Bookshop.Service
{
    public class FileUpload : IFileUpload
    {
        private IWebHostEnvironment _webHostEnvironment;
        private IHttpContextAccessor _httpContextAccessor;

        public FileUpload(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> DeleteFile(string fileName,string ImageType)
        {
            try
            {
              if(ImageType == SD.AuthorAvatar)
                {
                    var path = $"{_webHostEnvironment.WebRootPath}\\AdminLayout\\images\\AuthorAvatar\\{fileName}";

                    if (File.Exists(path))
                    {
                        File.Delete(path);
                        return true;
                    }
                    return false;
                }
                else if(ImageType == SD.BookAvatar){
                    var path = $"{_webHostEnvironment.WebRootPath}\\AdminLayout\\images\\BookImage\\{fileName}";

                    if (File.Exists(path))
                    {
                        File.Delete(path);
                        return true;
                    }
                    return false;
                }
                else if(ImageType == SD.GalleryAvatar)
                {
                    var path = $"{_webHostEnvironment.WebRootPath}\\AdminLayout\\images\\BookImage\\Gallery\\{fileName}";

                    if (File.Exists(path))
                    {
                        File.Delete(path);
                        return true;
                    }
                    return false;
                }
                else
                {
                    var path = $"{_webHostEnvironment.WebRootPath}\\AdminLayout\\images\\SliderImage\\{fileName}";

                    if (File.Exists(path))
                    {
                        File.Delete(path);
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {

                return false;
            }
        }

        public async Task<string> UploadFile(IBrowserFile file,string ImageType)
        {

            try
            {
               if(ImageType == SD.AuthorAvatar)
                {
                    FileInfo fileInfo = new FileInfo(file.Name);
                    var fileName = Guid.NewGuid().ToString() + fileInfo.Extension;
                    var FolderDirectory = $"{_webHostEnvironment.WebRootPath}\\AdminLayout\\images\\AuthorAvatar";
                    var path = Path.Combine(_webHostEnvironment.WebRootPath, "AdminLayout", "images", "AuthorAvatar", fileName);

                    var memoryStream = new MemoryStream();
                    await file.OpenReadStream().CopyToAsync(memoryStream);

                    if (!Directory.Exists(FolderDirectory))
                    {
                        Directory.CreateDirectory(FolderDirectory);
                    }


                    await using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                    {
                        memoryStream.WriteTo(fs);
                    }


                    var Url = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host.Value}/";
                    var UploadedfilePath = $"{Url}AdminLayout/images/AuthorAvatar/{fileName}";
                    return UploadedfilePath;
                }
                else if (ImageType == SD.BookAvatar){
                    FileInfo fileInfo = new FileInfo(file.Name);
                    var fileName = Guid.NewGuid().ToString() + fileInfo.Extension;
                    var FolderDirectory = $"{_webHostEnvironment.WebRootPath}\\AdminLayout\\images\\BookImage";
                    var path = Path.Combine(_webHostEnvironment.WebRootPath, "AdminLayout", "images", "BookImage", fileName);

                    var memoryStream = new MemoryStream();
                    await file.OpenReadStream().CopyToAsync(memoryStream);

                    if (!Directory.Exists(FolderDirectory))
                    {
                        Directory.CreateDirectory(FolderDirectory);
                    }


                    await using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                    {
                        memoryStream.WriteTo(fs);
                    }


                    var Url = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host.Value}/";
                    var UploadedfilePath = $"{Url}AdminLayout/images/BookImage/{fileName}";
                    return UploadedfilePath;
                }
                else if(ImageType == SD.GalleryAvatar)
                {
                    FileInfo fileInfo = new FileInfo(file.Name);
                    var fileName = Guid.NewGuid().ToString() + fileInfo.Extension;
                    var FolderDirectory = $"{_webHostEnvironment.WebRootPath}\\AdminLayout\\images\\BookImage\\Gallery";
                    var path = Path.Combine(_webHostEnvironment.WebRootPath, "AdminLayout", "images", "BookImage","Gallery", fileName);

                    var memoryStream = new MemoryStream();
                    await file.OpenReadStream().CopyToAsync(memoryStream);

                    if (!Directory.Exists(FolderDirectory))
                    {
                        Directory.CreateDirectory(FolderDirectory);
                    }


                    await using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                    {
                        memoryStream.WriteTo(fs);
                    }


                    var Url = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host.Value}/";
                    var UploadedfilePath = $"{Url}AdminLayout/images/BookImage/Gallery/{fileName}";
                    return UploadedfilePath;
                }
                else
                {
                    FileInfo fileInfo = new FileInfo(file.Name);
                    var fileName = Guid.NewGuid().ToString() + fileInfo.Extension;
                    var FolderDirectory = $"{_webHostEnvironment.WebRootPath}\\AdminLayout\\images\\SliderImage";
                    var path = Path.Combine(_webHostEnvironment.WebRootPath, "AdminLayout", "images", "SliderImage", fileName);

                    var memoryStream = new MemoryStream();
                    await file.OpenReadStream().CopyToAsync(memoryStream);

                    if (!Directory.Exists(FolderDirectory))
                    {
                        Directory.CreateDirectory(FolderDirectory);
                    }


                    await using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                    {
                        memoryStream.WriteTo(fs);
                    }


                    var Url = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host.Value}/";
                    var UploadedfilePath = $"{Url}AdminLayout/images/SliderImage/{fileName}";
                    return UploadedfilePath;
                }
            }
            catch (Exception e)
            {

                throw e;
            }




        }
    }
}
