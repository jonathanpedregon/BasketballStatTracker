using BasketballStatTracker.Models;
using BasketballStatTracker.Windows;
using System.Collections.Generic;
using System.Windows;

namespace BasketballStatTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Game> Games { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Games = new List<Game>();
        }

        private void CreateNewGameButton_Click(object sender, RoutedEventArgs e)
        {
            var createGameWindow = new CreateGameWindow();
            var successfulCreation = createGameWindow.ShowDialog();
            Games.Add(createGameWindow.Game);
        }
    }
}
