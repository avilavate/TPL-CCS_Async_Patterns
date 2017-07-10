using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace APM_to_TPL
{
    class awaitException
    {
        public static async Task GetUrl()
        {
            HttpClient ht = new HttpClient();
            try
            {
                var a1 = ht.GetStringAsync(@"https://github1.com/avilavate");
                var a2 = ht.GetStringAsync(@"https://github1.com/avilavate");


                try
                {
                    await Task.WhenAll(new Task[] { a1, a2 });
                    var c = a1.Result + a2.Result;
                }
                catch (AggregateException Ae)
                {

                    throw;
                }
                   
              
              
            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
            }
        }
    }
}
