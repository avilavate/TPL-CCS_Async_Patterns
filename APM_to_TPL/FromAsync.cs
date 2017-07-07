using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    }
}
