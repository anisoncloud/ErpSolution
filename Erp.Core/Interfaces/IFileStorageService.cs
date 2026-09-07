using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Core.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> UploadFileAsync(
            IFormFile file,
            string moduleName,        // e.g., "accounts", "inventory"
            string subFolder,         // e.g., "vouchers", "products"
            string? customFileName = null,
            string[]? allowedExtensions = null);
    }
}
