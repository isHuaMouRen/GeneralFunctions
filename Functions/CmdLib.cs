using System;
using System.Diagnostics;
using System.Threading.Tasks;

// CmdLib
// 控制台操作
// Version: 2025-9-14 11:00

namespace CmdLib
{
    public class CmdHelper
    {
        /// <summary>
        /// 异步执行控制台命令
        /// </summary>
        /// <param name="command">要执行的命令</param>
        /// <param name="showWindow">是否显示 cmd 窗口</param>
        /// <param name="closeAfter">执行完成后是否关闭窗口</param>
        /// <param name="workingDirectory">可选，指定工作目录</param>
        /// <returns>命令输出的字符串（仅在隐藏窗口时有效）</returns>
        public static async Task<string> RunCmdAsync(string command, bool showWindow = false, bool closeAfter = true, string workingDirectory = "")
        {
            return await Task.Run(() =>
            {
                try
                {
                    string argPrefix = closeAfter ? "/c " : "/k "; // /c 执行后关闭, /k 执行后保留

                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = argPrefix + command,
                        RedirectStandardOutput = !showWindow,
                        RedirectStandardError = !showWindow,
                        UseShellExecute = showWindow,
                        CreateNoWindow = !showWindow
                    };

                    if (!string.IsNullOrEmpty(workingDirectory))
                    {
                        psi.WorkingDirectory = workingDirectory;
                    }

                    using (Process process = Process.Start(psi))
                    {
                        if (showWindow)
                        {
                            process.WaitForExit();
                            return "";
                        }
                        else
                        {
                            string output = process.StandardOutput.ReadToEnd();
                            string error = process.StandardError.ReadToEnd();
                            process.WaitForExit();

                            return string.IsNullOrEmpty(error) ? output : output + Environment.NewLine + "错误: " + error;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return "执行异常: " + ex.Message;
                }
            });
        }
    }
}