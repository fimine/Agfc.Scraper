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
        DownloadPagesAsync().Wait();

        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("The end!");
        Console.ReadLine();
    }

    static async Task DownloadPagesAsync()
    {
         var days = new string[DateTime.IsLeapYear(DateTime.Today.Year) ? 366 : 365];
        //var days = new string[1];

        using (var client = new HttpClient())
        {
            client.Timeout = new TimeSpan(0, 10, 0);
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var encoding = Encoding.GetEncoding("windows-1251");

            var tasks = DownloadTasks(client, encoding, days);
            await Task.WhenAll(tasks);

        }
    }



    static List<Task> DownloadTasks(HttpClient client, Encoding encoding, string[] days)
    {
        var start = new DateTime(DateTime.Today.Year, 01, 01);
        var tasks = new List<Task>();

        for (var i = 0; i < days.Length; i++)
        {
            var date = start.AddDays(i);
            var query = BuildQuery(date.Month.ToString(), date.Day.ToString());

            var builder = new UriBuilder("http://calendar.russportal.ru/");
            builder.Query = query;
            var address = builder.Uri;

            var task = Task.Run(async () =>
                {

                    await DownloadPageAsync(client, encoding, address, 5);

                });

            Console.WriteLine(date.ToString());
            tasks.Add(task);

        }
        return tasks;
    }


    static async Task<string> DownloadPageAsync(HttpClient client, Encoding encoding, Uri address, int retryCount)
    {
        var ret = string.Empty;

        for (var i = 0; i < retryCount; i++)
        {
            try
            {
                var response = await client.GetByteArrayAsync(address);
                ret = encoding.GetString(response);
                await Console.Out.WriteLineAsync(address.ToString() + "    " + ret.Length);
                return ret;
            }
            catch (Exception)
            {
                await Console.Out.WriteLineAsync(address.ToString() + " Error. Retrying attepmpt  " + i);
            }
        }
        return ret;


    }

    static string BuildQuery(string m, string d)
    {
        var y = "2016";
        var temp = string.Format("?d={0}&m={1}&y={2}&id=calendar", d, m, y);
        return temp;
    }

}