using System.Runtime.InteropServices;
using System.Drawing;

namespace ToolLib.Library.GdiToolLib
{
    public class GdiTool
    {
        #region Win32 API

        [DllImport("gdi32.dll")]
        private static extern IntPtr CreatePen(int fnPenStyle, int nWidth, int crColor);

        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateSolidBrush(int crColor);

        [DllImport("gdi32.dll")]
        private static extern IntPtr SelectObject(IntPtr hdc, IntPtr h);

        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr ho);

        [DllImport("gdi32.dll")]
        private static extern bool Rectangle(IntPtr hdc, int left, int top, int right, int bottom);

        [DllImport("gdi32.dll")]
        private static extern bool RoundRect(IntPtr hdc, int left, int top, int right, int bottom, int width, int height);

        [DllImport("gdi32.dll")]
        private static extern bool Ellipse(IntPtr hdc, int left, int top, int right, int bottom);

        [DllImport("gdi32.dll")]
        private static extern bool MoveToEx(IntPtr hdc, int x, int y, IntPtr lpPoint);

        [DllImport("gdi32.dll")]
        private static extern bool LineTo(IntPtr hdc, int x, int y);

        [DllImport("gdi32.dll")]
        private static extern bool TextOut(IntPtr hdc, int x, int y, string lpString, int c);

        [DllImport("gdi32.dll")]
        private static extern bool SetPixel(IntPtr hdc, int x, int y, int color);

        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr hWnd, IntPtr hdc);

        #endregion

        private static IntPtr screenDC = GetDC(IntPtr.Zero);

        private static int ToCOLORREF(Color color)
        {
            return (color.R | (color.G << 8) | (color.B << 16));
        }

        private static void UsePenBrush(Color border, Color fill, Action<IntPtr> drawAction)
        {
            IntPtr pen = CreatePen(0, 1, ToCOLORREF(border));
            IntPtr brush = CreateSolidBrush(ToCOLORREF(fill));
            IntPtr oldPen = SelectObject(screenDC, pen);
            IntPtr oldBrush = SelectObject(screenDC, brush);

            drawAction(screenDC);

            SelectObject(screenDC, oldPen);
            SelectObject(screenDC, oldBrush);
            DeleteObject(pen);
            DeleteObject(brush);
        }

        #region 绘制函数

        public static void DrawText(string text, Point p, Color color)
        {
            IntPtr pen = CreatePen(0, 1, ToCOLORREF(color));
            IntPtr oldPen = SelectObject(screenDC, pen);

            TextOut(screenDC, p.X, p.Y, text, text.Length);

            SelectObject(screenDC, oldPen);
            DeleteObject(pen);
        }

        public static void DrawRectangle(Rectangle rect, Color border, Color fill)
        {
            UsePenBrush(border, fill, hdc =>
            {
                Rectangle(hdc, rect.Left, rect.Top, rect.Right, rect.Bottom);
            });
        }

        public static void DrawRoundRectangle(Rectangle rect, int roundWidth, int roundHeight, Color border, Color fill)
        {
            UsePenBrush(border, fill, hdc =>
            {
                RoundRect(hdc, rect.Left, rect.Top, rect.Right, rect.Bottom, roundWidth, roundHeight);
            });
        }

        public static void DrawEllipse(Rectangle rect, Color border, Color fill)
        {
            UsePenBrush(border, fill, hdc =>
            {
                Ellipse(hdc, rect.Left, rect.Top, rect.Right, rect.Bottom);
            });
        }

        public static void DrawLine(Point p1, Point p2, Color color)
        {
            IntPtr pen = CreatePen(0, 1, ToCOLORREF(color));
            IntPtr oldPen = SelectObject(screenDC, pen);

            MoveToEx(screenDC, p1.X, p1.Y, IntPtr.Zero);
            LineTo(screenDC, p2.X, p2.Y);

            SelectObject(screenDC, oldPen);
            DeleteObject(pen);
        }

        public static void DrawPoint(Point p, Color color)
        {
            SetPixel(screenDC, p.X, p.Y, ToCOLORREF(color));
        }

        #endregion
    }
}
