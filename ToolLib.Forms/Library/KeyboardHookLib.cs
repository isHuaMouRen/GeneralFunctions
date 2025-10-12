using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ToolLib.Library.KeyboardHookLib
{
    // 自定义事件参数
    public class KeyboardHookEventArgs : EventArgs
    {
        public Keys Key { get; }
        public bool Handled { get; set; } // 是否拦截

        public KeyboardHookEventArgs(Keys key)
        {
            Key = key;
            Handled = false;
        }
    }

    public class KeyboardHook : IDisposable
    {
        private IntPtr _hookID = IntPtr.Zero;
        private LowLevelKeyboardProc _proc;

        // 按下/抬起事件
        public event EventHandler<KeyboardHookEventArgs> KeyDownEvent;
        public event EventHandler<KeyboardHookEventArgs> KeyUpEvent;

        private readonly HashSet<Keys> _keysPressed = new HashSet<Keys>();
        public Keys[] KeysPressed => _keysPressed.Count > 0 ? _keysPressed.ToArray() : Array.Empty<Keys>();

        public KeyboardHook()
        {
            _proc = HookCallback;
            _hookID = SetHook(_proc);
        }

        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                Keys key = (Keys)vkCode;
                var args = new KeyboardHookEventArgs(key);

                switch ((int)wParam)
                {
                    case WM_KEYDOWN:
                    case WM_SYSKEYDOWN:
                        if (_keysPressed.Add(key)) // 只在第一次按下时触发
                        {
                            KeyDownEvent?.Invoke(this, args);
                        }
                        break;

                    case WM_KEYUP:
                    case WM_SYSKEYUP:
                        if (_keysPressed.Remove(key))
                        {
                            KeyUpEvent?.Invoke(this, args);
                        }
                        break;
                }

                // 如果事件里设置了 Handled = true，则拦截按键
                if (args.Handled)
                {
                    return (IntPtr)1; // 推荐返回非零值，避免某些情况下系统异常
                }
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        public void Dispose()
        {
            UnhookWindowsHookEx(_hookID);
        }

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private const int WM_SYSKEYDOWN = 0x0104;
        private const int WM_SYSKEYUP = 0x0105;

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn,
            IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}
