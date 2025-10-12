using System.Drawing;

namespace ToolLib.AreaSelectorLib
{
    public class AreaSelector
    {
        public static Rectangle Show(double opacity = 0.2)
        {
            using (var window = new AreaSelectorForm())
            {
                window.Opacity = opacity;
                window.ShowDialog();

                return window.rectangle;
            }
        }
    }
}
