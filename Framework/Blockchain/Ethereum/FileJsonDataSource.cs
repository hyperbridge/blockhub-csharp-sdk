using System;
using System.IO;

// TODO: Let's move this out to a PCL-only assembly to remove it from the core.
//       Another assembly will prevent the need for these PCL directives all over the place
#if !PCL
namespace Hyperbridge.Blockchain.Ethereum 
{
    public class FileJsonDataSource : IJsonDataSource 
    {
        private string FilePath { get; }

        public FileJsonDataSource(string filePath)
        {
            if (filePath == null) throw new ArgumentNullException(nameof(filePath));
            // TODO: Should we check for anything that may cause an exception here, or let it blow up on GetData?

            this.FilePath = filePath;
        }

        public string GetData() {
            using (var file = File.OpenText(filePath))
                return file.ReadToEnd();
        }
    }
}
#endif