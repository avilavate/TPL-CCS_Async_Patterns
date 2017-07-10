using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cancelation_Token
{
    class Token
    {
        public static void DownloadAsync(string path = @"https://github.com/avilavate")
        {
            var ct = new CancellationTokenSource();
            var token = ct.Token;

            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(5000);
                using (var wc = new WebClient())
                {
                    wc.DownloadStringCompleted += (o, e) =>
                    {
                        if (!e.Cancelled)
                        {
                            Console.WriteLine("The download has completed:\n");
                            Console.WriteLine(e.Result + "\n\nPress any key.");
                        }
                        else
                        {
                            Console.WriteLine("The download was canceled.");
                        }
                    };
                    if (!token.IsCancellationRequested)
                    {
                        using (var reg = token.Register(() => { wc.CancelAsync(); }))
                        {
                            Console.WriteLine("Request Started!");
                            wc.DownloadStringAsync(new Uri(path));
                        }

                    }

                }

            }, token);

            Console.WriteLine("Press 'c' to cancel.\n");
            char ch = Console.ReadKey().KeyChar;
            Console.WriteLine();
            if (ch == 'c')
                ct.Cancel();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            ct.Dispose();
        }
    }
}
