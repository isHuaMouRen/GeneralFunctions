# DownloaderLib

下载器

---

``` C#
public static async Task DownloadFileAsync(string url,string savePath,Action<double> progress,CancellationToken cancellationToken)
```
异步下载文件带进度回调与取消

|参数|类型|默认值|描述|
|-|-|-|-|
|`url`|`string`|*None*|文件URL|
|`savePath`|`string`|*None*|保存目录|
|`progress`|`Action<double>`|*None*|进度回调(单位: %)(`50.5` = `50.5%`)|
|`cancellationToken`|`CancellationToken`|*None*|取消Token|

|返回值类型|描述|
|-|-|
|`Task`|*无返回值*|

**示例:**
``` C#
CancellationTokenSource cts;

private async void btnDownload_Click(object sender, EventArgs e)
{
    cts = new CancellationTokenSource();
    progressBar1.Value = 0;

    try
    {
        await Downloader.DownloadFileAsync("https://example.com/largefile.zip",@"C:\path\file.zip",percent => progressBar1.Invoke((Action)(() => progressBar1.Value = (int)percent)),        cts.Token);

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
}
```