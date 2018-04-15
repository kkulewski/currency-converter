using System.IO;
using System.Threading.Tasks;
using CConv.Droid.Services;
using CConv.Services.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileWriter))]
namespace CConv.Droid.Services
{
    internal class FileWriter : IFileWriter
    {
        public async Task Write(string fileName, string data)
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, fileName);
            await Task.Run(() => File.WriteAllText(filePath, data));
        }

        public async Task<string> Read(string fileName)
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, fileName);

            if (File.Exists(filePath))
            {
                return await Task.Run(() => File.ReadAllText(filePath));
            }

            return await Task.Run(() => string.Empty);
        }
    }
}
