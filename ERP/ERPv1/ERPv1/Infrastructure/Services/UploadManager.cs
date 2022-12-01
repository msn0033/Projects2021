using ERPv1.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.Infrastructure.Services
{
    public class UploadManager : IUploadManager
    {
        private readonly IWebHostEnvironment _env;

        public UploadManager(IWebHostEnvironment env)
        {

            _env = env;
        }
        public string UploadedFile(IFormFile pic, string FolderName)
        {
            string uniqueFileName = null;
            if (pic != null)
            {
                string uploadsFolder = Path.Combine(_env.WebRootPath, FolderName);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + pic.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileSteam = new FileStream(filePath, FileMode.Create))
                {
                    pic.CopyTo(fileSteam);
                }
            }
            return uniqueFileName;
        }
    }
}
