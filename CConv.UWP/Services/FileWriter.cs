using System;
using System.IO;
using System.Threading.Tasks;
using CConv.Services.Storage;
using CConv.UWP.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileWriter))]
namespace CConv.UWP.Services
{
    public class FileWriter : IFileWriter
    {
        public async Task Write(string fileName, string data)
        {
            var documentsPath = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
            var filePath = Path.Combine(documentsPath, fileName);
            await Task.Run(() => File.WriteAllText(filePath, data));
        }

        public async Task<string> Read(string fileName)
        {
            var documentsPath = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
            var filePath = Path.Combine(documentsPath, fileName);

            if (File.Exists(filePath))
            {
                return await Task.Run(() => File.ReadAllText(filePath));
            }

            return await Task.Run(() => string.Empty);
        }
    }
}
