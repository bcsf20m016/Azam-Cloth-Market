using Sukhdari_Server.Service.IService;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Sukhdari_Server.Service
{
    public class ImageUpload : IImageUpload
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        public ImageUpload(IWebHostEnvironment web, IConfiguration configuration)
        {
            _webHostEnvironment = web;
            //_httpContextAccessor = httpContextAccessor;
            _configuration = configuration;

        }
        public bool DeleteImage(string imageName)
        {
            
            try
            {
                var path = $"{_webHostEnvironment.WebRootPath}\\ProductImages\\{imageName}";
                if(File.Exists(path))
                {
                    File.Delete(path);
                    return true;
                }
                return false;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<string> uploadImage(IBrowserFile image)
        {
            try
            {
                FileInfo info = new FileInfo(image.Name);

                var imageName = Guid.NewGuid().ToString() + info.Extension;
                var folderDirectory = $"{_webHostEnvironment.WebRootPath}\\ProductImages";
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "ProductImages", imageName);

                var memoryStream = new MemoryStream();
                await image.OpenReadStream().CopyToAsync(memoryStream);

                if(!Directory.Exists(folderDirectory))
                {
                    Directory.CreateDirectory(folderDirectory);
                }

                await using(var fs= new FileStream(path,FileMode.Create,FileAccess.Write))
                {
                    memoryStream.WriteTo(fs);
                }
                var url = $"{_configuration.GetValue<string>("ServerUrl")}";
                var fullPath = $"{url}ProductImages/{imageName}";
                return fullPath;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
