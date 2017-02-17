using System;
using System.Collections.Generic;
using System.Text;

namespace Agfc.Scraper.Scraping
{
    class FileParser
    {
        public FileParser()
        {
           // HtmlAgilityPack.
        }

        public void ExtractCalendar(HtmlAgilityPack.HtmlDocument doc)
        {

            foreach(var div in doc.DocumentNode.ChildNodes)
            {
                //div.
            }
        }
    }
}
