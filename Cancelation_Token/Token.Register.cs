using System;
using System.Net;
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
                using (var wc = new WebClient())
                {
                    wc.DownloadStringCompleted +=
                    (o, e) =>
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
                    else
                    {
                        Console.WriteLine($"Cancellation has Requested");
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
