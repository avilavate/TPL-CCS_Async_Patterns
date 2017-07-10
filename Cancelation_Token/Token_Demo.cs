using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Cancelation_Token
{
    class Token_Demo
    {
        private static TaskCompletionSource<string> _tcs = new TaskCompletionSource<string>();
        private CancellationTokenSource _ct = new CancellationTokenSource();
        public static Task<string> DownloadAsync(string url)
        {
            using (var wc = new WebClient())
            {
                wc.DownloadStringCompleted += Wc_DownloadStringCompleted;
                wc.DownloadStringAsync(new System.Uri(url));
            }
            return _tcs.Task;
        }

        private static void Wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                _tcs.SetCanceled();
                return;
            }
            if (e.Error!=null)
            {
                _tcs.SetException(new System.Exception(e.Error.Message));
                return;
            }
            if (!string.IsNullOrEmpty(e.Result))
            {
                _tcs.SetResult(e.Result);
            }
        }
    }
}
