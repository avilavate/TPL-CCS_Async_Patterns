using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncCS5
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();

            p.GetList();
            // Task.Run(async () =>
            // {
            //     await p.GetList();
            // }).GetAwaiter().GetResult();

            //// Console.WriteLine($"Length: {p.GetList().Result.Count()}");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(200);
            }


            Console.Read();
        }

        private async void GetList()
        {
            //Sample s = new Sample();
            //await s.LongList();
            //for (int i = 0; i < 10; i++)
            //{
            //    i++;
            //}
            //Console.WriteLine("GetList");
            //return;


            var t_string = await new WebClient().DownloadStringTaskAsync(@"https://jsonplaceholder.typicode.com/posts/1");
            
            var t = await DoWork();
            Console.WriteLine(t_string);
            Console.WriteLine("fdgDF");

            //var t_str= TaskFactory(() =>
            //{
            //    return "Hey";
            //});

            //t_str.Start();

            
        }

        private async Task<int> DoWork()
        {
            
            int res = 0;
   
            await Task.Run(() =>
                       {
                           res = 10;
                       });
            return res;
        }


        public static Task<T> TaskFactory<T>(Func<T> func)
        {
            return new Task<T>(func);
        }
    }
}
