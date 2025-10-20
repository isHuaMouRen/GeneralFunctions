using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ToolLib.Library.HookLib
{
    public class HookManager
    {
        // ===================== 键盘 =====================
        public class KeyboardHookEventArgs : EventArgs
        {
            public byte Key { get; }
            public bool Handled { get; set; }

            public KeyboardHookEventArgs(byte key)
            {
                Key = key;
                Handled = false;
            }
        }

        public class KeyboardHook : IDisposable
        {
            private IntPtr _hookID = IntPtr.Zero;
            private LowLevelKeyboardProc _proc;
            private readonly HashSet<byte> _keysPressed = new HashSet<byte>();

            public event EventHandler<KeyboardHookEventArgs>? KeyDownEvent;
            public event EventHandler<KeyboardHookEventArgs>? KeyUpEvent;

            public byte[] KeysPressed => _keysPressed.Count > 0 ? _keysPressed.ToArray() : Array.Empty<byte>();

            public KeyboardHook()
            {
                _proc = HookCallback;
                _hookID = SetHook(_proc);
            }

            private IntPtr SetHook(LowLevelKeyboardProc proc)
            {
                using Process curProcess = Process.GetCurrentProcess();
                using ProcessModule curModule = curProcess.MainModule!;
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }

            private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
            {
                if (nCode >= 0)
                {
                    int vkCode = Marshal.ReadInt32(lParam);
                    byte key = (byte)vkCode;
                    var args = new KeyboardHookEventArgs(key);

                    switch ((int)wParam)
                    {
                        case WM_KEYDOWN:
                        case WM_SYSKEYDOWN:
                            if (_keysPressed.Add(key))
                                KeyDownEvent?.Invoke(this, args);
                            break;
                        case WM_KEYUP:
                        case WM_SYSKEYUP:
                            if (_keysPressed.Remove(key))
                                KeyUpEvent?.Invoke(this, args);
                            break;
                    }

                    if (args.Handled)
                        return (IntPtr)1;
                }
                return CallNextHookEx(_hookID, nCode, wParam, lParam);
            }

            public void Dispose() => UnhookWindowsHookEx(_hookID);

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

        // ===================== 鼠标 =====================
        public class MouseHookEventArgs : EventArgs
        {
            public MouseButtons Button { get; }
            public bool Handled { get; set; }

            public MouseHookEventArgs(MouseButtons button)
            {
                Button = button;
                Handled = false;
            }
        }

        public enum MouseButtons : byte
        {
            None = 0,
            Left = 1,
            Right = 2,
            Middle = 3
        }

        public class MouseHook : IDisposable
        {
            private IntPtr _hookID = IntPtr.Zero;
            private LowLevelMouseProc _proc;
            private readonly HashSet<MouseButtons> _buttonsPressed = new HashSet<MouseButtons>();

            public event EventHandler<MouseHookEventArgs>? MouseDownEvent;
            public event EventHandler<MouseHookEventArgs>? MouseUpEvent;

            public MouseButtons[] ButtonsPressed => _buttonsPressed.Count > 0 ? _buttonsPressed.ToArray() : Array.Empty<MouseButtons>();

            public MouseHook()
            {
                _proc = HookCallback;
                _hookID = SetHook(_proc);
            }

            private IntPtr SetHook(LowLevelMouseProc proc)
            {
                using Process curProcess = Process.GetCurrentProcess();
                using ProcessModule curModule = curProcess.MainModule!;
                return SetWindowsHookEx(WH_MOUSE_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }

            private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
            {
                if (nCode >= 0)
                {
                    MouseButtons button = MouseButtons.None;
                    bool isDown = false;

                    switch ((int)wParam)
                    {
                        case WM_LBUTTONDOWN: button = MouseButtons.Left; isDown = true; break;
                        case WM_LBUTTONUP: button = MouseButtons.Left; isDown = false; break;
                        case WM_RBUTTONDOWN: button = MouseButtons.Right; isDown = true; break;
                        case WM_RBUTTONUP: button = MouseButtons.Right; isDown = false; break;
                        case WM_MBUTTONDOWN: button = MouseButtons.Middle; isDown = true; break;
                        case WM_MBUTTONUP: button = MouseButtons.Middle; isDown = false; break;
                    }

                    if (button != MouseButtons.None)
                    {
                        var args = new MouseHookEventArgs(button);

                        if (isDown)
                        {
                            if (_buttonsPressed.Add(button))
                                MouseDownEvent?.Invoke(this, args);
                        }
                        else
                        {
                            if (_buttonsPressed.Remove(button))
                                MouseUpEvent?.Invoke(this, args);
                        }

                        if (args.Handled)
                            return (IntPtr)1;
                    }
                }

                return CallNextHookEx(_hookID, nCode, wParam, lParam);
            }

            public void Dispose() => UnhookWindowsHookEx(_hookID);

            private const int WH_MOUSE_LL = 14;
            private const int WM_LBUTTONDOWN = 0x0201;
            private const int WM_LBUTTONUP = 0x0202;
            private const int WM_RBUTTONDOWN = 0x0204;
            private const int WM_RBUTTONUP = 0x0205;
            private const int WM_MBUTTONDOWN = 0x0207;
            private const int WM_MBUTTONUP = 0x0208;

            private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

            [DllImport("user32.dll", SetLastError = true)]
            private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn,
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
}
