using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace await_c3
{
    class Program
    {
        static void Main(string[] args)
        {
            ProcessAsync();
            ProcessTask();
            for (int i = 0; i < 20; i++)
            {
                Thread.Sleep(50);
                Console.WriteLine(i);
            }

        }

        private static async Task ProcessAsync()
        {
            string res;
            using (var wc= new WebClient())
            {
                res = await wc.DownloadStringTaskAsync(@"https://jsonplaceholder.typicode.com/posts/1");
            }

            Console.WriteLine("Async: "+res);
        }
        private static  void ProcessTask()
        {
            try
            {
                using (var wc = new WebClient())
                {
                    wc.DownloadStringTaskAsync(@"https://jsonplaceholder.typicode.com/posts/1").ContinueWith(t => {
                        Task.Delay(90);
                        Console.WriteLine("TPL Task: "+t.Result);
                    });
                }
            }
            catch (Exception x)
            {
            }
            
        }
    }
}
