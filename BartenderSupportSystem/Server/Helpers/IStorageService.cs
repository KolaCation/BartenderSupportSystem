using System.Threading.Tasks;

namespace BartenderSupportSystem.Server.Helpers
{
    public interface IStorageService
    {
        Task DeleteFile(string fileRoute, string domainName);
        Task<string> EditFile(byte[] content, string extension, string domainName, string fileRoute);
        Task<string> SaveFile(byte[] content, string extenstion, string domainName);
    }
}
