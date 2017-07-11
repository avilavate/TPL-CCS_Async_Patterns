using System.IO;
using System.Threading;

namespace Cancelation_Token
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream f = new FileStream("Program.cs", FileMode.Open, FileAccess.Read, FileShare.Read, 1000, FileOptions.Asynchronous);
            var buffer = new byte[1000];
            var cts = new CancellationTokenSource();
            cts.Cancel();
            var t_asynRead = f.ReadAsync(buffer, 0, 100, cts.Token);
            try
            {
                t_asynRead.Wait();
                System.Console.WriteLine(t_asynRead.Status);
            }
            catch (System.Exception)
            {
                System.Console.WriteLine(t_asynRead.Status);
                // throw;
            }
            //var t_wc = Token_Demo.DownloadAsync(@"https://github.com/avilavate");
            //t_wc.ContinueWith(t =>
            //{
            //    try
            //    {
            //        Console.WriteLine(t.Result);
            //    }
            //    catch (AggregateException ae)
            //    {

            //        Console.WriteLine(ae.Flatten().InnerException);
            //    }
            //});

            //  Token.DownloadAsync();
            //Console.ReadKey();
        }
    }
}
