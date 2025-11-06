namespace HuaZi.Library.Downloader
{
    /// <summary>
    /// 好用的下载器(真的非常好用!)
    /// </summary>
    public class Downloader
    {
        /// <summary>
        /// 下载进度和速度信息
        /// </summary>
        /// <param name="progress">下载进度百分比 (0-100)</param>
        /// <param name="speedKBps">当前下载速度 (KB/s)</param>
        public delegate void ProgressCallback(double progress, double speedKBps);

        /// <summary>
        /// 异步下载文件
        /// </summary>
        /// <param name="url">文件URL</param>
        /// <param name="savePath">保存路径</param>
        /// <param name="callback">进度与速度合并回调</param>
        /// <param name="cancellationToken">取消Token</param>
        /// <returns></returns>
        public static async Task DownloadFileAsync(
            string url,
            string savePath,
            ProgressCallback? callback = null,
            CancellationToken cancellationToken = default)
        {
            using var client = new HttpClient();
            using var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            response.EnsureSuccessStatusCode();

            var totalBytes = response.Content.Headers.ContentLength ?? -1L;
            var canReport = totalBytes != -1 && callback != null;

            using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            using var fileStream = new FileStream(savePath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true);

            var buffer = new byte[8192];
            long totalRead = 0;
            int bytesRead;

            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            long lastReportBytes = 0;
            double lastReportTimeMs = 0;

            while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0)
            {
                await fileStream.WriteAsync(buffer, 0, bytesRead, cancellationToken);
                totalRead += bytesRead;

                if (canReport)
                {
                    double currentTimeMs = stopwatch.Elapsed.TotalMilliseconds;

                    // 计算进度
                    double percent = (double)totalRead / totalBytes * 100;

                    // 计算速度 (KB/s)
                    double speedKBps = 0;
                    if (currentTimeMs > lastReportTimeMs)
                    {
                        double deltaBytes = totalRead - lastReportBytes;
                        double deltaTimeSec = (currentTimeMs - lastReportTimeMs) / 1000.0;
                        speedKBps = deltaBytes / deltaTimeSec / 1024.0;
                    }

                    // 合并回调
                    callback?.Invoke(percent, speedKBps);

                    // 更新状态
                    lastReportBytes = totalRead;
                    lastReportTimeMs = currentTimeMs;
                }

                cancellationToken.ThrowIfCancellationRequested();
            }

            // 结束时回调一次（进度 100%，速度 0）
            callback?.Invoke(100.0, 0.0);
        }
    }
}