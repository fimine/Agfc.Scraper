using Agfc.Scraper.Scraping;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Starting...");
        Console.WriteLine();

        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        var calendarDownloader = new CalendarDownloader(new HttpPagesBulkDownloader(Encoding.GetEncoding("windows-1251")), new BulkPersisterFileSystem());
        calendarDownloader.DownloadAsync().Wait();
        calendarDownloader.SaveAsync().Wait();

        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("The end!");
        Console.ReadLine();
    }

   

}