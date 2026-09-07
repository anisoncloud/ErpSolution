using Microsoft.AspNetCore.Hosting;
using Erp.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Core.Services
{
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _environment;

        public LocalFileStorageService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> UploadFileAsync(IFormFile file, string moduleName, string subFolder, string? customFileName = null, string[]? allowedExtensions = null)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Invalid file.");

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (allowedExtensions != null && !allowedExtensions.Contains(extension))
                throw new InvalidOperationException("Unsupported file format.");

            // Standardize paths across the entire ERP: wwwroot/uploads/accounts/vouchers/
            string relativePath = Path.Combine("uploads", moduleName.ToLower(), subFolder.ToLower());
            string absoluteFolderPath = Path.Combine(_environment.WebRootPath, relativePath);

            if (!Directory.Exists(absoluteFolderPath))
            {
                Directory.CreateDirectory(absoluteFolderPath);
            }

            string finalFileName = !string.IsNullOrWhiteSpace(customFileName)
                ? (customFileName.EndsWith(extension) ? customFileName : $"{customFileName}{extension}")
                : $"{Guid.NewGuid()}{extension}";

            string absoluteFilePath = Path.Combine(absoluteFolderPath, finalFileName);

            using (var stream = new FileStream(absoluteFilePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Returns clean web URL path: "/uploads/accounts/vouchers/xyz.pdf"
            return $"/{relativePath.Replace(Path.DirectorySeparatorChar, '/')}/{finalFileName}";
        }
    }
}
