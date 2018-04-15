﻿using System;
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
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, fileName);
            await Task.Run(() => File.WriteAllText(filePath, data));
        }

        public async Task<string> Read(string fileName)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, fileName);
            return await Task.Run(() => File.ReadAllText(filePath));
        }
    }
}
