using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncCS5
{
    class Sample
    {
        public async Task LongList()
        {
            await Task.Factory.StartNew(() =>
             {
                 Task.Delay(30000);
                 List<string> lst = new List<string>();
                 return (from line in File.ReadLines(@"ratings.txt")
                         select line).ToList().Count();
             });
            Console.WriteLine("Long List");
            await AddAndMultiplyAsync(10, 10, 10);
         
            //  Console.WriteLine("mul"+mul);
        }
        public async Task<int> AddAndMultiplyAsync(int x, int y, int z)

        {

            int sum = await AddAsync(x, y);

            int multiply = sum * z;

            return multiply;

        }

        private async Task<int> AddAsync(int x, int y)
        {
            return x + y;
        }
    }
}
