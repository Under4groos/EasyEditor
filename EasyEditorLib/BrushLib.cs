using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace EasyEditorLib
{
    public static class BrushLib
    {
        public static Brush ToBrush(this string str)
        {
            if(str == "null" || str == null)
                return Brushes.Transparent;
            SolidColorBrush solidColorBrush = new BrushConverter().ConvertFromString(str) as SolidColorBrush;
            return solidColorBrush;
        }
    }
}
