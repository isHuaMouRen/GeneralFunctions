using System;
using System.Windows.Forms;
using ToolLib.Library.ErrorReportBoxLib;

namespace ToolLib.ErrorReportLib
{
    public class ErrorReportBox
    {
        /// <summary>
        /// 显示窗体
        /// </summary>
        /// <param name="title">窗口标题</param>
        /// <param name="text">窗口内容</param>
        /// <param name="ex">错误信息</param>
        /// <param name="exTip">显示在错误信息上方的提示</param>
        /// <param name="showCloseButton">是否显示关闭按钮</param>
        /// <returns>错误提示框的返回值，默认DialogResult.Cancel、关闭按钮DialogResult.Abort、继续按钮DialogResult.OK</returns>
        public static DialogResult Show(string title, string text, Exception ex, string exTip = null, bool showCloseButton = true)
        {
            using(var window=new ErrorReportBoxForm())
            {
                window.title = title;
                window.text = text;
                if (exTip != null) window.exTip = exTip;
                window.showCloseButton = showCloseButton;
                window.ex = ex;

                return window.ShowDialog();
            }
        }

        /// <summary>
        /// 显示窗体，以字符串方式传入错误信息
        /// </summary>
        /// <param name="title">窗口标题</param>
        /// <param name="text">窗口内容</param>
        /// <param name="ex">错误信息</param>
        /// <param name="exTip">显示在错误信息上方的提示</param>
        /// <param name="showCloseButton">是否显示关闭按钮</param>
        /// <returns>错误提示框的返回值，默认DialogResult.Cancel、关闭按钮DialogResult.Abort、继续按钮DialogResult.OK</returns>
        public static DialogResult Show(string title, string text, string ex, string exTip = null, bool showCloseButton = true)
        {
            return Show(title, text, new Exception(ex), exTip, showCloseButton);
        }
    }
}
