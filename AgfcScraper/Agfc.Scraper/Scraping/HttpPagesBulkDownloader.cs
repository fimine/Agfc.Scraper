using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace Agfc.Scraper.Scraping
{
    
    public class HttpPagesBulkDownloader : IHttpPagesBulkDownloader
    {
        private readonly Encoding _encoding;

        public HttpPagesBulkDownloader(Encoding Encoding)
        {
            this._encoding = Encoding;
        }

        public async Task<List<string>> DownloadPagesAsync(string BaseQuery, IEnumerable<string[]> queryParams)
        {
            var ret = new List<string>();
            using (var client = new HttpClient())
            {
                var downloadTasks = queryParams.Select(prm => Task.Run(async () =>
                {

                    var query = BuildQuery(BaseQuery, prm);
                    var queryUri = new UriBuilder(query).Uri;
                    var downloadedPage = await DownloadPageAsync(client, queryUri);
                    if (!string.IsNullOrEmpty(downloadedPage))
                        ret.Add(downloadedPage);

                }));

                await Task.WhenAll(downloadTasks);
            }
            return ret;
        }
        
         async Task<string> DownloadPageAsync(HttpClient client, Uri address, int? retryCount = 5)
        {
            var ret = string.Empty;

            for (var i = 0; i < retryCount; i++)
            {
                try
                {
                    Out.Write("Downloading: " + address.ToString() + "; Attempt " + i);
                    var response = await client.GetByteArrayAsync(address);
                    ret =  _encoding.GetString(response);
                    Out.WriteLine("  Success; Downloaded " + ret.Length + " characters");
                    return ret;
                }
                catch (Exception)
                {
                    Out.WriteLine(address.ToString() + "Error. Retrying...  " + i);
                    Thread.Sleep(1000);
                }
            }
            return ret;

        }

        string BuildQuery(string baseQuery, string[] args)
        {
            var temp = string.Format(baseQuery, args);
            return temp;
        }

    }
}
