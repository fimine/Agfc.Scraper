using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agfc.Scraper.Scraping
{
    public class BulkPersisterFileSystem : IBulkPersister
    {
        public async Task<int> SaveBulkAsync(IEnumerable<string> sources)
        {
            var ret = 0;

            string path = @"D:\temp\MyTest.txt";

            // Delete the file if it exists.
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            
            //Create the file.
            using (var fs = File.Create(path))
            {
                foreach (var src in sources)
                {
                    var info = new UTF8Encoding(true).GetBytes(src);
                    await fs.WriteAsync(info, 0, info.Length);
                    ret++;
                }
            }

            return ret;
        }

    }
}

