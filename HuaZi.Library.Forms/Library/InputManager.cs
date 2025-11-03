using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

// InputLib
// 输入管理器(键鼠操作)
// Version: 2025-9-14 10:52

namespace HuaZi.Library.Input
{
    public class InputManager
    {
        public static class Mouse
        {
            // --- WinAPI ---
            private const int MOUSEEVENTF_MOVE = 0x0001;
            private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
            private const int MOUSEEVENTF_LEFTUP = 0x0004;
            private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
            private const int MOUSEEVENTF_RIGHTUP = 0x0010;
            private const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
            private const int MOUSEEVENTF_MIDDLEUP = 0x0040;
            private const int MOUSEEVENTF_WHEEL = 0x0800; // 滚轮
            private const int MOUSEEVENTF_HWHEEL = 0x1000; // 横向滚轮（部分鼠标支持）

            [DllImport("user32.dll")]
            private static extern bool SetCursorPos(int X, int Y);

            [DllImport("user32.dll")]
            private static extern bool GetCursorPos(out Point lpPoint);

            [DllImport("user32.dll")]
            private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

            /// <summary>
            /// 获取当前鼠标坐标
            /// </summary>
            /// <returns>坐标,Point 类型</returns>
            public static Point GetMousePosition()
            {
                GetCursorPos(out Point p);
                return p;
            }

            /// <summary>
            /// 设置鼠标坐标
            /// </summary>
            /// <param name="x">X坐标</param>
            /// <param name="y">Y坐标</param>
            public static void SetMousePosition(int x, int y)
            {
                SetCursorPos(x, y);
            }

            /// <summary>
            /// 设置鼠标坐标
            /// </summary>
            /// <param name="pos">坐标, Point类型</param>
            public static void SetMousePosition(Point pos)
            {
                SetCursorPos(pos.X, pos.Y);
            }

            // ========= 左键 =========
            public static class LeftButton
            {
                public static void Click()
                {
                    var pos = GetMousePosition();
                    mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, pos.X, pos.Y, 0, 0);
                }

                public static void Down()
                {
                    var pos = GetMousePosition();
                    mouse_event(MOUSEEVENTF_LEFTDOWN, pos.X, pos.Y, 0, 0);
                }

                public static void Up()
                {
                    var pos = GetMousePosition();
                    mouse_event(MOUSEEVENTF_LEFTUP, pos.X, pos.Y, 0, 0);
                }
            }

            // ========= 右键 =========
            public static class RightButton
            {
                public static void Click()
                {
                    var pos = GetMousePosition();
                    mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, pos.X, pos.Y, 0, 0);
                }

                public static void Down()
                {
                    var pos = GetMousePosition();
                    mouse_event(MOUSEEVENTF_RIGHTDOWN, pos.X, pos.Y, 0, 0);
                }

                public static void Up()
                {
                    var pos = GetMousePosition();
                    mouse_event(MOUSEEVENTF_RIGHTUP, pos.X, pos.Y, 0, 0);
                }
            }

            // ========= 中键 =========
            public static class MiddleButton
            {
                public static void Click()
                {
                    var pos = GetMousePosition();
                    mouse_event(MOUSEEVENTF_MIDDLEDOWN | MOUSEEVENTF_MIDDLEUP, pos.X, pos.Y, 0, 0);
                }

                public static void Down()
                {
                    var pos = GetMousePosition();
                    mouse_event(MOUSEEVENTF_MIDDLEDOWN, pos.X, pos.Y, 0, 0);
                }

                public static void Up()
                {
                    var pos = GetMousePosition();
                    mouse_event(MOUSEEVENTF_MIDDLEUP, pos.X, pos.Y, 0, 0);
                }
            }

            // ========= 滚轮 =========
            public static class Wheel
            {
                /// <summary>
                /// 向上滚动一格（120）
                /// </summary>
                public static void ScrollUp(int amount = 120)
                {
                    var pos = GetMousePosition();
                    mouse_event(MOUSEEVENTF_WHEEL, pos.X, pos.Y, amount, 0);
                }

                /// <summary>
                /// 向下滚动一格（-120）
                /// </summary>
                public static void ScrollDown(int amount = 120)
                {
                    var pos = GetMousePosition();
                    mouse_event(MOUSEEVENTF_WHEEL, pos.X, pos.Y, -amount, 0);
                }

                /// <summary>
                /// 向右滚动（水平滚轮，正值向右）
                /// </summary>
                public static void ScrollRight(int amount = 120)
                {
                    var pos = GetMousePosition();
                    mouse_event(MOUSEEVENTF_HWHEEL, pos.X, pos.Y, amount, 0);
                }

                /// <summary>
                /// 向左滚动（水平滚轮，负值向左）
                /// </summary>
                public static void ScrollLeft(int amount = 120)
                {
                    var pos = GetMousePosition();
                    mouse_event(MOUSEEVENTF_HWHEEL, pos.X, pos.Y, -amount, 0);
                }
            }
        }

        public static class Keyboard
        {
            // --- WinAPI ---
            [DllImport("user32.dll", SetLastError = true)]
            private static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

            private const int KEYEVENTF_KEYDOWN = 0x0000; // 按下
            private const int KEYEVENTF_KEYUP = 0x0002;   // 松开

            /// <summary>
            /// 按下某键
            /// </summary>
            /// <param name="key">键</param>
            public static void KeyDown(Keys key)
            {
                keybd_event((byte)key, 0, KEYEVENTF_KEYDOWN, 0);
            }

            /// <summary>
            /// 松开某键
            /// </summary>
            /// <param name="key">键</param>
            public static void KeyUp(Keys key)
            {
                keybd_event((byte)key, 0, KEYEVENTF_KEYUP, 0);
            }

            /// <summary>
            /// 模拟按键点击，按下再松开
            /// </summary>
            /// <param name="key">键</param>
            /// <param name="delay">延迟(单位: ms)</param>
            public async static void KeyPress(Keys key, int delay = 50)
            {
                KeyDown(key);
                await Task.Delay(delay); // 模拟真实按键的停顿
                KeyUp(key);
            }
        }
    }
}