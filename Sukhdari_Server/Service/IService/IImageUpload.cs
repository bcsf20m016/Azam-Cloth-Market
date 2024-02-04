using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sukhdari_Server.Service.IService
{
    public interface IImageUpload
    {
        Task<string> uploadImage(IBrowserFile image);
        bool DeleteImage(string imageName);
    }
}
