using System.Threading.Tasks;

namespace CConv.Services.Storage
{
    public interface IFileWriter
    {
        Task Write(string fileName, string data);
        Task<string> Read(string fileName);
    }
}
