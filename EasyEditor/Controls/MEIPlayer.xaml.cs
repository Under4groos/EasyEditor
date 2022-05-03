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
    /// Логика взаимодействия для MEIPlayer.xaml
    /// </summary>
    public partial class MEIPlayer : UserControl
    {
        public MEIPlayer()
        {
            InitializeComponent();

        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            (sender as MediaElement).Position = new TimeSpan(0, 0, 0, 0, 1);
            (sender as MediaElement).Play();
        }

        private void MediaElement_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as MediaElement).Play();
        }
    }
}
