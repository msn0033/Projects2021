using Microsoft.AspNetCore.Http;

namespace ERPv1.Infrastructure.Interfaces
{
    public interface IUploadManager
    {
        string UploadedFile(IFormFile pic, string FolderName);
    }
}