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
    /// Interaction logic for ReboundWindow.xaml
    /// </summary>
    public partial class ReboundWindow : Window
    {
        public string ReboundPlayer { get; set; }
        public ReboundWindow(Game game)
        {
            InitializeComponent();
            foreach (var player in game.Team1.Players)
            {
                var button = new Button { Width = 100, Name = player.Name, Margin = new Thickness(8), Content = player.Name };
                button.Click += new RoutedEventHandler(ReboundClick);
                Team1StackPanel.Children.Add(button);
            }
            foreach (var player in game.Team2.Players)
            {
                var button = new Button { Width = 100, Name = player.Name, Margin = new Thickness(8), Content = player.Name };
                button.Click += new RoutedEventHandler(ReboundClick);
                Team2StackPanel.Children.Add(button);
            }
        }

        private void ReboundClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                ReboundPlayer = button.Name;
                Close();
            }
        }
    }
}
