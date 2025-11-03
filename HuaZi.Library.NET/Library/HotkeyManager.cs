using System.Runtime.InteropServices;

//https://learn.microsoft.com/zh-cn/windows/win32/inputdev/virtual-key-codes
namespace HuaZi.Library.HotkeyManager
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

        private readonly Dictionary<int, Action> _hotkeyActions = new Dictionary<int, Action>();
        private readonly List<int> _registeredHotkeys = new List<int>();
        private int _currentId;

        public HotkeyManager()
        {
            // Initialization
        }

        /// <summary>
        /// 注册热键
        /// </summary>
        /// <param name="modifier">修饰键</param>
        /// <param name="keyCode">键代码，可通过微软官方文档查询:https://learn.microsoft.com/zh-cn/windows/win32/inputdev/virtual-key-codes</param>
        /// <param name="callback">回调</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void RegisterHotkey(Modifiers modifier, int keyCode, Action callback)
        {
            _currentId++;

            // Register the hotkey with the system
            if (!RegisterHotKey(IntPtr.Zero, _currentId, (uint)modifier, (uint)keyCode))
                throw new InvalidOperationException("Failed to register hotkey!");

            // Store the callback action by hotkey ID
            _hotkeyActions[_currentId] = callback;
            _registeredHotkeys.Add(_currentId);
        }

        /// <summary>
        /// Unregisters a hotkey by its ID
        /// </summary>
        public void UnregisterHotkey(int id)
        {
            UnregisterHotKey(IntPtr.Zero, id);
            _hotkeyActions.Remove(id);
            _registeredHotkeys.Remove(id);
        }

        public void Dispose()
        {
            foreach (var id in _registeredHotkeys)
            {
                UnregisterHotKey(IntPtr.Zero, id);
            }
            _hotkeyActions.Clear();
        }
    }
}
