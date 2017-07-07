﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APM_to_TPL
{
    class Program
    {
        static void Main(string[] args)
        {
            Task<int> t_read;var buffer = new byte[10000] ;
            FileStream fileStream = new FileStream(@"ratings.txt", FileMode.Open, FileAccess.Read);
            t_read = fileStream.ReadAsynTask(buffer);

            var t_1 = t_read.ContinueWith(t =>
              {
                  Console.WriteLine(t.Result);
                  Console.WriteLine(buffer.Length);
                  Console.WriteLine(System.Text.Encoding.UTF8.GetString(buffer));
                  fileStream.Close();
              });

            t_1.Wait();

            Console.Read();
        }
    }
}
