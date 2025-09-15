using Debug_NewLibTest.Utils.PosSelectorLib;
using System.Drawing;

// PosSelectorLib
// 坐标选择器
// Version: 2025-9-15 19:41

namespace PosSelectorLib
{
    public class PosSelector
    {
        public static Point Show(double opacity = 0.2)
        {
            using (var window = new PosSelectorForm())
            {
                window.Opacity = opacity;
                window.ShowDialog();
            }

            return PosSelectorForm.Pos;
        }
    }
}
