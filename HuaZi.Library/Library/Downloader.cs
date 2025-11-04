namespace HuaZi.Library.Downloader
{
    /// <summary>
    /// 好用的下载器(真的非常好用!)
    /// </summary>
    public class Downloader
    {
        /// <summary>
        /// 异步下载文件
        /// </summary>
        /// <param name="url">文件URL</param>
        /// <param name="savePath">保存路径</param>
        /// <param name="progress">进度回调(单位: %)</param>
        /// <param name="cancellationToken">取消Token</param>
        /// <returns></returns>
        public static async Task DownloadFileAsync(string url,string savePath,Action<double> progress,CancellationToken cancellationToken)
        {
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
            {
                response.EnsureSuccessStatusCode();

                var totalBytes = response.Content.Headers.ContentLength ?? -1L;
                var canReportProgress = totalBytes != -1 && progress != null;

                using (Stream stream = await response.Content.ReadAsStreamAsync())
                using (FileStream fileStream = new FileStream(savePath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true))
                {
                    byte[] buffer = new byte[8192];
                    long totalRead = 0;
                    int bytesRead;

                    while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0)
                    {
                        await fileStream.WriteAsync(buffer, 0, bytesRead, cancellationToken);
                        totalRead += bytesRead;

                        if (canReportProgress)
                        {
                            double percent = (double)totalRead / totalBytes * 100;
                            progress!(percent);
                        }
                    }
                }
            }
        }

        #region Example
        /*CancellationTokenSource cts;

        private async void btnDownload_Click(object sender, EventArgs e)
        {
            cts = new CancellationTokenSource();
            progressBar1.Value = 0;

            try
            {
                await Downloader.DownloadFileAsync(
                    "https://example.com/largefile.zip",
                    @"C:\path\file.zip",
                    percent => progressBar1.Invoke((Action)(() => progressBar1.Value = (int)percent)),
                    cts.Token
                );
                MessageBox.Show("下载完成！");
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("下载已取消！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("下载失败：" + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cts?.Cancel();
        }*/
        #endregion
    }
}
