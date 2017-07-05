using System;
using System.IO;
using System.Threading.Tasks;

namespace TaskCompletionSourseDemo
{
    class Program
    {
        public static StreamReader _reader { get; set; }
        static Program()
        {
            try
            {
                _reader = new StreamReader(@"ratings.txt");
            }
            catch (Exception)
            {
                _reader = null;
            }
          
        }

        public TaskCompletionSource<string> tsc = new TaskCompletionSource<string>();

        static void Main(string[] args)
        {
            Program p = new Program();

            try
            {
                var t_line = p.GetLine();

            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
            }

            var tsc_task = p.tsc.Task;

            tsc_task.ContinueWith((t) =>
            {
                if (t.IsFaulted)
                {
                    Console.WriteLine(t.Exception.InnerExceptions);
                }
                else
                {
                    Console.WriteLine(t.Result);
                }
            });

            Console.Read();
        }

        private Task GetLine()
        {
            if (null == _reader)
            {
                throw new NullReferenceException("No file detected");
            }

            return _reader.ReadLineAsync().ContinueWith(t =>
            {
                string line;
                if (t.IsCompleted)
                {
                    line = t.Result as string;
                    if (line == null)
                    {
                        this.tsc.SetResult("Done");
                        _reader.Close();
                    }
                    else
                    {
                        Console.WriteLine(t.Result.ToUpper());
                        GetLine();
                    }
                }
                else
                {
                    _reader.Close();
                    tsc.SetException(t.Exception.InnerExceptions);
                }
            });
        }
    }
}
