using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Core
{
    public class FileServer
    {
        private readonly HttpListener _listener = new();
        private readonly string _distPath;

        public FileServer(string distPath, int port = 21337)
        {
            _distPath = distPath;
            _listener.Prefixes.Add($"http://localhost:{port}/");
        }

        public void Start()
        {
            _listener.Start();
            Task.Run(Listen);
        }

        public void Stop() => _listener.Stop();

        private async Task Listen()
        {
            while (_listener.IsListening)
            {
                try
                {
                    var ctx = await _listener.GetContextAsync();
                    _ = Task.Run(() => HandleRequest(ctx));
                }
                catch { break; }
            }
        }

        private void HandleRequest(HttpListenerContext ctx)
        {
            var urlPath = ctx.Request.Url!.AbsolutePath.TrimStart('/');
            if (string.IsNullOrEmpty(urlPath)) urlPath = "index.html";

            var filePath = Path.Combine(_distPath, urlPath.Replace('/', Path.DirectorySeparatorChar));

            // SPA fallback
            if (!File.Exists(filePath))
                filePath = Path.Combine(_distPath, "index.html");

            var ext = Path.GetExtension(filePath);
            ctx.Response.ContentType = ext switch
            {
                ".html" => "text/html",
                ".js" => "application/javascript",
                ".css" => "text/css",
                ".svg" => "image/svg+xml",
                ".png" => "image/png",
                ".ico" => "image/x-icon",
                _ => "application/octet-stream"
            };

            var bytes = File.ReadAllBytes(filePath);
            ctx.Response.ContentLength64 = bytes.Length;
            ctx.Response.OutputStream.Write(bytes, 0, bytes.Length);
            ctx.Response.OutputStream.Close();
        }
    }
}