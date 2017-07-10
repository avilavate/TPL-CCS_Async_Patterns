using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cancelation_Token
{
    class Program
    {
        static void Main(string[] args)
        {
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

            Token.DownloadAsync();
            Console.ReadKey();
        }
    }
}
