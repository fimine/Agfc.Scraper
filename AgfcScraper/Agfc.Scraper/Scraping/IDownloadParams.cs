using System;
using System.Collections.Generic;
using System.Text;

namespace Agfc.Scraper.Scraping
{
    interface IDownloadParams
    {
        string Query { get; }
        Encoding Encoding { get; }
    }
}
