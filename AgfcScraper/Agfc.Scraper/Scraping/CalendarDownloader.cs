using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Agfc.Scraper.Scraping
{
    public class CalendarDownloader
    {
        private readonly IHttpPagesBulkDownloader _downloader;
        private const string _baseQueryTemplate = "http://calendar.russportal.ru/js/russportal-calendar-informer.php?onlytoday=1&date={0}-{1}-{2}";
        private List<string> _pages;
        private readonly IBulkPersister _persister;

        public CalendarDownloader(IHttpPagesBulkDownloader downloader, IBulkPersister persister)
        {
            _downloader = downloader;
            _persister = persister;
        }

        public async Task DownloadAsync()
        {
            _pages = await _downloader.DownloadPagesAsync(_baseQueryTemplate, BuildQueryList());
        }

        public async Task SaveAsync()
        {
            var ret = await _persister.SaveBulkAsync(_pages);
        }

        List<string[]> BuildQueryList()
        {
            var ret = new List<string[]>();
            var days = new string[DateTime.IsLeapYear(DateTime.Today.Year) ? 366 : 365];
            var date = new DateTime(DateTime.Today.Year, 01, 01);

            for (var i = 0; i < days.Length; i++)
            {
                ret.Add(new string[] { date.ToString("yyyy"), date.ToString("MM"), date.ToString("dd") });
                date = date.AddDays(1);
            }

            return ret;

        }




    }
}
