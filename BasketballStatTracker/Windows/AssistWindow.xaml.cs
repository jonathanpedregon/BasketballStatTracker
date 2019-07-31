using BasketballStatTracker.Models;
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
using System.Windows.Shapes;

namespace BasketballStatTracker.Windows
{
    /// <summary>
    /// Interaction logic for AssistWindow.xaml
    /// </summary>
    public partial class AssistWindow : Window
    {
        public string AssistPlayer { get; set; }
        public AssistWindow(List<Player> players)
        {
            InitializeComponent();
            foreach(var player in players)
            {
                var button = new Button { Name = player.Name, Content = player.Name, Width = 100, Margin = new Thickness(8) };
                button.Click += new RoutedEventHandler(AssistClick);
                MainStackBox.Children.Add(button);
            }
        }

        private void AssistClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
                AssistPlayer = button.Name;
            Close();
        }
    }
}
