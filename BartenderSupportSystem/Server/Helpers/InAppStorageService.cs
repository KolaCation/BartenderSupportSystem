using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BartenderSupportSystem.Server.Helpers
{
    public sealed class InAppStorageService : IStorageService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public InAppStorageService(IWebHostEnvironment environment, IHttpContextAccessor httpContextAccessor)
        {
            _environment = environment;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> SaveFile(byte[] content, string extenstion, string domainName)
        {
            var fileName = $"{Guid.NewGuid()}.{extenstion}";
            var domainDirectory = Path.Combine(_environment.WebRootPath, domainName);
            if (!Directory.Exists(domainDirectory))
            {
                Directory.CreateDirectory(domainDirectory);
            }

            var filePath = Path.Combine(domainDirectory, fileName);
            await File.WriteAllBytesAsync(filePath, content);
            var currentUrl =
                $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            var pathForDb = Path.Combine(currentUrl, domainName, fileName);
            return pathForDb;
        }

        public async Task DeleteFile(string fileRoute, string domainName)
        {
            var fileName = Path.GetFileName(fileRoute);
            var filePath = Path.Combine(_environment.WebRootPath, domainName, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            await Task.FromResult(0);
        }

        public async Task<string> EditFile(byte[] content, string extension, string domainName, string fileRoute)
        {
            if (!string.IsNullOrEmpty(fileRoute))
            {
                await DeleteFile(fileRoute, domainName);
            }

            return await SaveFile(content, extension, domainName);
        }
    }
}