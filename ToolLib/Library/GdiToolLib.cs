using System;
using System.Runtime.InteropServices;
using System.Drawing;

namespace ToolLib.GdiToolLib
{
    public class GdiTool
    {
        #region Win32 API

        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateSolidBrush(int crColor);

        [DllImport("gdi32.dll")]
        private static extern IntPtr SelectObject(IntPtr hdc, IntPtr h);

        [DllImport("gdi32.dll")]
        private static extern bool Rectangle(IntPtr hdc, int left, int top, int right, int bottom);

        [DllImport("gdi32.dll")]
        private static extern bool Ellipse(IntPtr hdc, int left, int top, int right, int bottom);

        [DllImport("gdi32.dll")]
        private static extern bool RoundRect(IntPtr hdc, int left, int top, int right, int bottom, int width, int height);

        [DllImport("gdi32.dll")]
        private static extern bool Polygon(IntPtr hdc, POINT[] points, int count);

        [DllImport("gdi32.dll")]
        private static extern bool MoveToEx(IntPtr hdc, int x, int y, IntPtr lpPoint);

        [DllImport("gdi32.dll")]
        private static extern bool LineTo(IntPtr hdc, int x, int y);

        [DllImport("gdi32.dll")]
        private static extern bool SetPixel(IntPtr hdc, int x, int y, int crColor);

        [DllImport("gdi32.dll")]
        private static extern bool TextOut(IntPtr hdc, int nXStart, int nYStart, string lpString, int cbString);

        [DllImport("gdi32.dll")]
        private static extern int SetBkMode(IntPtr hdc, int iBkMode);

        [DllImport("gdi32.dll")]
        private static extern uint SetTextColor(IntPtr hdc, int crColor);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool InvalidateRect(IntPtr hWnd, IntPtr lpRect, bool bErase);

        #endregion

        private const int TRANSPARENT = 1;

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }

        private static IntPtr GetScreenDC() => GetDC(IntPtr.Zero);

        private static void ReleaseScreenDC(IntPtr hdc) => ReleaseDC(IntPtr.Zero, hdc);

        #region 绘图方法

        /// <summary>
        /// 绘制文本
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="x">坐标X</param>
        /// <param name="y">坐标Y</param>
        /// <param name="color">颜色</param>
        public static void DrawText(string text, int x, int y, Color color)
        {
            IntPtr hdc = GetScreenDC();
            if (hdc != IntPtr.Zero)
            {
                SetBkMode(hdc, TRANSPARENT);
                SetTextColor(hdc, ColorTranslator.ToWin32(color));
                TextOut(hdc, x, y, text, text.Length);
                ReleaseScreenDC(hdc);
            }
        }

        /// <summary>
        /// 绘制矩形边框
        /// </summary>
        /// <param name="left">左侧坐标</param>
        /// <param name="top">顶部坐标</param>
        /// <param name="right">右侧坐标</param>
        /// <param name="bottom">底部坐标</param>
        /// <param name="color">颜色</param>
        public static void DrawRectangle(int left, int top, int right, int bottom, Color color)
        {
            IntPtr hdc = GetScreenDC();
            if (hdc != IntPtr.Zero)
            {
                IntPtr brush = CreateSolidBrush(ColorTranslator.ToWin32(color));
                IntPtr oldBrush = SelectObject(hdc, brush);
                Rectangle(hdc, left, top, right, bottom);
                SelectObject(hdc, oldBrush);
                ReleaseScreenDC(hdc);
            }
        }

        /// <summary>
        /// 绘制填充矩形
        /// </summary>
        /// <param name="left">左侧坐标</param>
        /// <param name="top">顶部坐标</param>
        /// <param name="right">右侧坐标</param>
        /// <param name="bottom">底部坐标</param>
        /// <param name="color">颜色</param>
        public static void DrawFilledRectangle(int left, int top, int right, int bottom, Color color)
        {
            DrawRectangle(left, top, right, bottom, color); // GDI Rectangle 默认填充
        }

        /// <summary>
        /// 绘制椭圆
        /// </summary>
        /// <param name="left">左侧坐标</param>
        /// <param name="top">顶部坐标</param>
        /// <param name="right">右侧坐标</param>
        /// <param name="bottom">底部坐标</param>
        /// <param name="color">颜色</param>
        public static void DrawEllipse(int left, int top, int right, int bottom, Color color)
        {
            IntPtr hdc = GetScreenDC();
            if (hdc != IntPtr.Zero)
            {
                IntPtr brush = CreateSolidBrush(ColorTranslator.ToWin32(color));
                IntPtr oldBrush = SelectObject(hdc, brush);
                Ellipse(hdc, left, top, right, bottom);
                SelectObject(hdc, oldBrush);
                ReleaseScreenDC(hdc);
            }
        }

        /// <summary>
        /// 绘制圆角矩形
        /// </summary>
        /// <param name="left">左侧坐标</param>
        /// <param name="top">顶部坐标</param>
        /// <param name="right">右侧坐标</param>
        /// <param name="bottom">底部坐标</param>
        /// <param name="arcWidth">宽度圆角弧度</param>
        /// <param name="arcHeight">高度圆角弧度</param>
        /// <param name="color">颜色</param>
        public static void DrawRoundRect(int left, int top, int right, int bottom, int arcWidth, int arcHeight, Color color)
        {
            IntPtr hdc = GetScreenDC();
            if (hdc != IntPtr.Zero)
            {
                IntPtr brush = CreateSolidBrush(ColorTranslator.ToWin32(color));
                IntPtr oldBrush = SelectObject(hdc, brush);
                RoundRect(hdc, left, top, right, bottom, arcWidth, arcHeight);
                SelectObject(hdc, oldBrush);
                ReleaseScreenDC(hdc);
            }
        }

        /// <summary>
        /// 绘制线
        /// </summary>
        /// <param name="x1">起点X</param>
        /// <param name="y1">起点Y</param>
        /// <param name="x2">终点X</param>
        /// <param name="y2">终点Y</param>
        /// <param name="color">颜色</param>
        public static void DrawLine(int x1, int y1, int x2, int y2, Color color)
        {
            IntPtr hdc = GetScreenDC();
            if (hdc != IntPtr.Zero)
            {
                SetPixel(hdc, x1, y1, ColorTranslator.ToWin32(color)); // 起点
                MoveToEx(hdc, x1, y1, IntPtr.Zero);
                LineTo(hdc, x2, y2);
                ReleaseScreenDC(hdc);
            }
        }

        /// <summary>
        /// 绘制多边形
        /// </summary>
        /// <param name="points">顶点</param>
        /// <param name="color">颜色</param>
        public static void DrawPolygon(POINT[] points, Color color)
        {
            if (points == null || points.Length < 3) return;
            IntPtr hdc = GetScreenDC();
            if (hdc != IntPtr.Zero)
            {
                IntPtr brush = CreateSolidBrush(ColorTranslator.ToWin32(color));
                IntPtr oldBrush = SelectObject(hdc, brush);
                Polygon(hdc, points, points.Length);
                SelectObject(hdc, oldBrush);
                ReleaseScreenDC(hdc);
            }
        }

        /// <summary>
        /// 绘制像素(只绘制一个)
        /// </summary>
        /// <param name="x">X坐标</param>
        /// <param name="y">Y坐标</param>
        /// <param name="color">颜色</param>
        public static void DrawPixel(int x, int y, Color color)
        {
            IntPtr hdc = GetScreenDC();
            if (hdc != IntPtr.Zero)
            {
                SetPixel(hdc, x, y, ColorTranslator.ToWin32(color));
                ReleaseScreenDC(hdc);
            }
        }

        #endregion

        #region 清屏

        /// <summary>
        /// 清屏
        /// </summary>
        public static void ClearScreen()
        {
            InvalidateRect(IntPtr.Zero, IntPtr.Zero, true);
        }

        #endregion

        #region 额外重载
        /// <summary>
        /// 绘制矩形(Rectangle类型传入)
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <param name="color">颜色</param>
        public static void DrawRectangle(Rectangle rect, Color color)
        {
            DrawRectangle(rect.Left, rect.Top, rect.Right, rect.Bottom, color);
        }

        /// <summary>
        /// 绘制填充矩形(Rectangle类型传入)
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <param name="color">颜色</param>
        public static void DrawFilledRectangle(Rectangle rect, Color color)
        {
            DrawFilledRectangle(rect.Left, rect.Top, rect.Right, rect.Bottom, color);
        }

        /// <summary>
        /// 绘制椭圆(Rectangle类型传入)
        /// </summary>
        /// <param name="rect">矩形区域</param>
        /// <param name="color">颜色</param>
        public static void DrawEllipse(Rectangle rect, Color color)
        {
            DrawEllipse(rect.Left, rect.Top, rect.Right, rect.Bottom, color);
        }

        /// <summary>
        /// 绘制圆角矩形(Rectangle类型传入)
        /// </summary>
        /// <param name="rect">矩形区域</param>
        /// <param name="arcWidth">宽度圆角弧度</param>
        /// <param name="arcHeight">高度圆角弧度</param>
        /// <param name="color">颜色</param>
        public static void DrawRoundRect(Rectangle rect, int arcWidth, int arcHeight, Color color)
        {
            DrawRoundRect(rect.Left, rect.Top, rect.Right, rect.Bottom, arcWidth, arcHeight, color);
        }

        /// <summary>
        /// 绘制线(Point类型传入)
        /// </summary>
        /// <param name="pos1">起点坐标</param>
        /// <param name="pos2">终点坐标</param>
        /// <param name="color">颜色</param>
        public static void DrawLine(Point pos1,Point pos2,Color color)
        {
            DrawLine(pos1.X, pos1.Y, pos2.X, pos2.Y, color);
        }

        #endregion
    }
}
