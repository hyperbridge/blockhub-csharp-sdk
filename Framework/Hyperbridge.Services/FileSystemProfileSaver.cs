using Hyperbridge.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hyperbridge.Services
{
    public class FileSystemProfileSaver : ISaver<Profile>
    {
        private string RootDirectory { get; }

        public FileSystemProfileSaver(string rootDirectory)
        {
            if (string.IsNullOrWhiteSpace(rootDirectory)) throw new ArgumentNullException(nameof(rootDirectory));
            RootDirectory = rootDirectory;
        }

        public string Save(Profile model)
        {
            var directoryPath = Path.Combine(RootDirectory, model.Id);

            // Check for directory existence. If it doesn't exist we will create it
            if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);

            var filePath = Path.Combine(directoryPath, $"{model.Id}.json");
            var json = JsonConvert.SerializeObject(model);

            File.WriteAllText(filePath, json);

            return FilePathToFileUri(filePath);
        }

        // Reference: https://stackoverflow.com/questions/1546419/convert-file-path-to-a-file-uri
        private static string FilePathToFileUri(string filePath)
        {
            StringBuilder uri = new StringBuilder();
            foreach (char v in filePath)
            {
                if ((v >= 'a' && v <= 'z') || (v >= 'A' && v <= 'Z') || (v >= '0' && v <= '9') ||
                  v == '+' || v == '/' || v == ':' || v == '.' || v == '-' || v == '_' || v == '~' ||
                  v > '\xFF')
                {
                    uri.Append(v);
                }
                else if (v == Path.DirectorySeparatorChar || v == Path.AltDirectorySeparatorChar)
                {
                    uri.Append('/');
                }
                else
                {
                    uri.Append(String.Format("%{0:X2}", (int)v));
                }
            }

            if (uri.Length >= 2 && uri[0] == '/' && uri[1] == '/') // UNC path
                uri.Insert(0, "file:");
            else
                uri.Insert(0, "file:///");

            return uri.ToString();
        }
    }
}
