using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agfc.Scraper.Scraping
{
    public interface IHttpPagesBulkDownloader
    {
        Task<List<string>> DownloadPagesAsync(string BaseQuery, IEnumerable<string[]> queryParams);
    }
}