|命名空间|类名|描述|
|-|-|-|
| `HuaZi.Library.Downloader` | `Downloader` |文件下载器|

```C#
public static async Task DownloadFileAsync(string url, string savePath, Action<double> progress, CancellationToken cancellationToken)
```

异步下载文件

|参数|描述|
|-|-|
| `url` |文件URL|
| `savePath` |保存路径|
| `progress` |进度回调(单位: %)，参数为双精度百分比值（0-100）；若无需进度可传 null|
| `cancellationToken` |取消Token，用于中断下载操作|

|返回值类型|描述|
|-|-|
| `Task` |无返回值；下载完成后任务完成。若取消或异常，将抛出相应异常。|

**备注**  
- 支持大文件下载，使用流式读取和写入，避免内存溢出。  
- 若服务器提供 Content-Length 头部且 progress 非 null，则实时报告进度。  
- 需要处理 OperationCanceledException（取消时）和其它异常（下载失败）。  
- 无需额外 using 指令（代码中已使用完整命名空间）。  
- 示例用法：结合 CancellationTokenSource 实现取消功能，并在 UI 线程更新进度条（需 Invoke）。  
- 缓冲区大小固定为 8192 字节，支持异步 I/O。  

**示例代码**  
```C#
CancellationTokenSource cts;

private async void btnDownload_Click(object sender, EventArgs e)
{
    //取消Token
    cts = new CancellationTokenSource();
    progressBar1.Value = 0;

    try
    {
        //开始下载
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
        //取消
        MessageBox.Show("下载已取消！");
    }
    catch (Exception ex)
    {
        //失败
        MessageBox.Show("下载失败：" + ex.Message);
    }
}

//取消下载
private void btnCancel_Click(object sender, EventArgs e)
{
    cts?.Cancel();
}
```