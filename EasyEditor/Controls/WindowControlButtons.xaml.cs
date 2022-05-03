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
 
    public partial class WindowControlButtons : UserControl
    {
        public MainWindow window { get; set; }
        public WindowControlButtons()
        {
            InitializeComponent();

            this.min.PreviewMouseLeftButtonDown += (o, e) =>
            {
                window.WindowState = WindowState.Minimized;
            };
            this.max.PreviewMouseLeftButtonDown += (o, e) =>
            {
                switch (window.WindowState)
                {
                    case WindowState.Normal:
                        window.WindowState = WindowState.Maximized;
                        window.SetMargin(7);
                        break;
                    case WindowState.Minimized:
                        window.SetMargin(0);
                        break;
                    case WindowState.Maximized:
                        window.WindowState = WindowState.Normal;
                        window.SetMargin(0);
                        break;
                    default:
                        break;
                }
               
            };
            this.close.PreviewMouseLeftButtonDown += (o, e) =>
            {
                window.Close();
            };
        }

        
    }
}
