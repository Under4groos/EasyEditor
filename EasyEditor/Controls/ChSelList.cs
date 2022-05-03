using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EasyEditor.Controls
{
    public class ChSelList : Border
    {
        StackPanel stackPanel = new StackPanel()
        {
            Background = Brushes.Transparent,
        };

        public ChSelList()
        {
            this.Child = stackPanel;
            //this.CornerRadius = new CornerRadius(7);
            this.MinWidth = 150;
            this.MinHeight = 10;
            this.Background = this.FindResource("ba_window") as Brush;
        }
        public void Add(UIElement uIElement)
        {
            this.stackPanel.Children.Add(uIElement);
        }
        public void Add(string text , Action action)
        {
            Border b = new Border()
            {
                Style = this.FindResource("style_border_hover") as Style,

            };
            
            Label label = new Label();
            label.Content = text;
            label.HorizontalContentAlignment = HorizontalAlignment.Left;
            label.Padding = new Thickness(10, 5, 10, 5);
            label.MouseLeftButtonDown += (sender, e) => 
            {
                Console.WriteLine(label.Content);
                action(); 
            
            };

            b.Child = label;

            this.stackPanel.Children.Add(b);
        }
        public void Add(string text, Action<string> action)
        {
            Border b = new Border()
            {
                Style = this.FindResource("style_border_hover") as Style,

            };

            Label label = new Label();
            label.Content = text;
            label.HorizontalContentAlignment = HorizontalAlignment.Left;
            label.Padding = new Thickness(10, 5, 10, 5);
            label.MouseLeftButtonDown += (sender, e) =>
            {
                //Console.WriteLine(label.Content);
                action(label.Content as string);

            };

            b.Child = label;

            this.stackPanel.Children.Add(b);
        }
    }
    public class MiltiChSelList : Grid
    {
        public Dictionary<string, ChSelList> list = new Dictionary<string, ChSelList>();

        public MiltiChSelList()
        {
            this.VerticalAlignment = VerticalAlignment.Top;
            this.HorizontalAlignment = HorizontalAlignment.Left;

            this.MouseLeave += (o, e) =>
            {
                this.Visibility = Visibility.Collapsed;
            };
        }
        public void Add(string str, ChSelList chSelList)
        {
            if (list.ContainsKey(str))
            {
                list[str] = chSelList;
            }
            else
            {
                list.Add(str, chSelList);
            }
            
            
        }
        public void chadd()
        {
            this.Visibility = Visibility.Collapsed;
            this.Children.Clear();
            foreach (var v in list)
                this.Children.Add(v.Value);
        }
        public void SetHideAll(string str)
        {
            this.Visibility = Visibility.Visible;
            foreach (var item in list)
            {
                if(item.Key != str)
                {
                    SetVisibility(item.Key, false);
                }
                else
                {
                    SetVisibility(item.Key, true);
                }
               
            }
        }
        public void SetVisibility(string str , bool b)
        {
            if (list.ContainsKey(str))
            {
                list[str].Visibility = b ? Visibility.Visible : Visibility.Collapsed;
            }
        }
    }
}
