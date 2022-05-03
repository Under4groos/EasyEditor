using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EasyEditor.Controls
{
    /// <summary>
    /// Логика взаимодействия для WResizerControl.xaml
    /// </summary>
    public partial class WResizerControl : UserControl
    {
        public Window window;
        WindowResizer windowResizer;
        public WResizerControl()
        {
            InitializeComponent();
            this.Loaded += (o, e) =>
            {
                if (window == null)
                    return;
                windowResizer = new WindowResizer(window);

                windowResizer.addResizerDown(d);
                windowResizer.addResizerLeft(l);
                windowResizer.addResizerRight(r);
                windowResizer.addResizerUp(t);
                windowResizer.addResizerRightDown(rd);
            };
        }
    }
}
