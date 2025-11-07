using System.Diagnostics;

namespace HuaZi.Library.Downloader
{
    /// <summary>
    /// 好用的下载器(真的很好用!)
    /// </summary>
    public class Downloader : IDisposable
    {
        private static readonly HttpClient HttpClient = new(
            new HttpClientHandler
            {
                AutomaticDecompression =
                    System.Net.DecompressionMethods.GZip |
                    System.Net.DecompressionMethods.Deflate
            })
        {
            Timeout = Timeout.InfiniteTimeSpan
        };

        public string Url { get; init; } = "";
        public string SavePath { get; init; } = "";
        public Action<double, double>? Progress { get; init; }   // (progress%, speed KB/s)
        public Action<bool, string?>? Completed { get; init; }   // (success, error)
        public int ReportIntervalMs { get; init; } = 200;        // 回调频率

        private CancellationTokenSource? _cts;
        private Task? _task;

        /// <summary>
        /// 开始下载
        /// </summary>
        public void StartDownload()
        {
            if (string.IsNullOrWhiteSpace(Url)) throw new InvalidOperationException("Url 未设置");
            if (string.IsNullOrWhiteSpace(SavePath)) throw new InvalidOperationException("SavePath 未设置");
            if (_task?.IsCompleted == false) throw new InvalidOperationException("已在下载");

            _cts?.Dispose();
            _cts = new CancellationTokenSource();
            _task = DownloadAsync(_cts.Token);
        }

        /// <summary>
        /// 停止下载
        /// </summary>
        public void StopDownload() => _cts?.Cancel();

        private async Task DownloadAsync(CancellationToken ct)
        {
            try
            {
                using var response = await HttpClient.GetAsync(Url, HttpCompletionOption.ResponseHeadersRead, ct);
                response.EnsureSuccessStatusCode();

                var total = response.Content.Headers.ContentLength ?? -1;
                var canReport = total > 0 && Progress != null;

                Directory.CreateDirectory(Path.GetDirectoryName(SavePath)!);
                if (File.Exists(SavePath)) File.Delete(SavePath);

                using var stream = await response.Content.ReadAsStreamAsync(ct);
                await using var file = new FileStream(SavePath, FileMode.Create, FileAccess.Write, FileShare.None, 32768, true);

                var buffer = new byte[32768];
                long readTotal = 0;
                var sw = Stopwatch.StartNew();
                long lastBytes = 0;
                double lastTime = 0;

                int bytes;
                while ((bytes = await stream.ReadAsync(buffer, ct)) > 0)
                {
                    await file.WriteAsync(buffer.AsMemory(0, bytes), ct);
                    readTotal += bytes;

                    if (canReport && sw.Elapsed.TotalMilliseconds - lastTime >= ReportIntervalMs)
                    {
                        var percent = readTotal * 100.0 / total;
                        var speed = (readTotal - lastBytes) / ((sw.Elapsed.TotalMilliseconds - lastTime) / 1000.0) / 1024.0;

                        Progress?.Invoke(percent, speed);

                        lastBytes = readTotal;
                        lastTime = sw.Elapsed.TotalMilliseconds;
                    }
                }

                Progress?.Invoke(100.0, 0.0);
                Completed?.Invoke(true, null);

            }
            catch (OperationCanceledException)
            {
                Completed?.Invoke(false, "已取消");
            }
            catch (Exception ex)
            {
                Completed?.Invoke(false, ex.Message);
            }
        }

        public void Dispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }
    }
}