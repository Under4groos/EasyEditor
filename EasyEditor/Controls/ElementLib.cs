using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EasyEditor.Controls
{
    public static class ElementLib
    {
        public static void LeftButtonDown(this UIElement ui , UIElement ui2,  Action<Point> action)
        {
            ui.PreviewMouseLeftButtonDown += (o, e) =>
            {
                Vector parent = (Vector)e.GetPosition(ui2);
                Vector child = (Vector)e.GetPosition(ui);
                Point childPosition = (Point)(parent - child);

                action(childPosition);
            };
        }
    }
}
