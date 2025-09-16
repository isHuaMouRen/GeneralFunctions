using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

// HotkeyManager
// 全局热键捕捉
// Version: 2025-9-15 19:54

namespace HotkeyManagerLib
{
    public class HotkeyManager : IDisposable
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [Flags]
        public enum Modifiers : uint
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            Win = 8
        }

        private class HotkeyWindow : NativeWindow, IDisposable
        {
            public event EventHandler<int> HotkeyPressed; // 触发事件，传热键ID

            public HotkeyWindow()
            {
                CreateHandle(new CreateParams());
            }

            protected override void WndProc(ref Message m)
            {
                const int WM_HOTKEY = 0x0312;
                if (m.Msg == WM_HOTKEY)
                {
                    int id = m.WParam.ToInt32();
                    HotkeyPressed?.Invoke(this, id);
                }
                base.WndProc(ref m);
            }

            public void Dispose()
            {
                DestroyHandle();
            }
        }

        private readonly HotkeyWindow _window;
        private int _currentId;
        private readonly Dictionary<int, Action> _hotkeyActions = new Dictionary<int, Action>();

        public HotkeyManager()
        {
            _window = new HotkeyWindow();
            _window.HotkeyPressed += (s, id) =>
            {
                if (_hotkeyActions.TryGetValue(id, out var action))
                {
                    action?.Invoke();
                }
            };
        }

        /// <summary>
        /// 注册热键并绑定回调函数
        /// </summary>
        public void RegisterHotkey(Modifiers modifier, Keys key, Action callback)
        {
            _currentId++;
            if (!RegisterHotKey(_window.Handle, _currentId, (uint)modifier, (uint)key))
                throw new InvalidOperationException("注册热键失败！");

            _hotkeyActions[_currentId] = callback;
        }

        /// <summary>
        /// 注销热键
        /// </summary>
        public void UnregisterHotkey(int id)
        {
            UnregisterHotKey(_window.Handle, id);
            _hotkeyActions.Remove(id);
        }

        public void Dispose()
        {
            foreach (var id in _hotkeyActions.Keys)
            {
                UnregisterHotKey(_window.Handle, id);
            }
            _window.Dispose();
            _hotkeyActions.Clear();
        }
    }
}
