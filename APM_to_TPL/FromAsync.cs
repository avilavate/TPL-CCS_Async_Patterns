using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APM_to_TPL
{
    static class FromAsync
    {
        public static Task<int> ReadAsynTask(this Stream s, byte[] buffer)
        {
            return Task<int>.Factory.FromAsync(s.BeginRead, s.EndRead, buffer, 0, 10000, null);
        }

        public static  void Get()
        {
            HttpClient ht = new HttpClient();
            try
            {
                ht.GetStringAsync(@"https://github.com/avilavate").ContinueWith(
                     t =>
                            {
                                Console.WriteLine(t.Result.Substring(0,2600));
                            });

                Task.Factory.StartNew(
                     ()=> 
                            {
                                for (int i = 0; i < 100; i++)
                                {
                                    Console.WriteLine(i);
                                }
                            });
            }
            catch(Exception e)
            {

            }
        }
    }
}
