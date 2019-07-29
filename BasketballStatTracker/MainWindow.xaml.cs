using BasketballStatTracker.Models;
using BasketballStatTracker.Windows;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace BasketballStatTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Game> Games { get; set; }
        public Game CurrentGame { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Games = new List<Game>();
        }

        private void CreateNewGameButton_Click(object sender, RoutedEventArgs e)
        {
            var createGameWindow = new CreateGameWindow();
            var successfulCreation = createGameWindow.ShowDialog();
            CurrentGame = createGameWindow.Game;
            CreatGrid();
        }

        private void CreatGrid()
        {
            MainStackPanel.Children.Add(new Label { Content = "Team One" });
            GetTeamStackPanels(CurrentGame.Team1);
            MainStackPanel.Children.Add(new Label { Content = "Team Two" });
            GetTeamStackPanels(CurrentGame.Team2);
        }

        private void GetTeamStackPanels(Team team)
        {
            foreach (var player in team.Players)
            {
                StackPanel stackPanel = GetPlayerStackPanel(player);
                MainStackPanel.Children.Add(stackPanel);
            }
        }

        private static StackPanel GetPlayerStackPanel(Player player)
        {
            var stackPanel = new StackPanel { Orientation = Orientation.Horizontal, Name = $"{player.Name}stackpanel" };
            stackPanel.Children.Add(new Label { Content = player.Name, Width=75 });
            stackPanel.Children.Add(new Button { Content = "1 Pt Attempt", Name = $"{player.Name}1PtAttempt", Margin= new Thickness(5,0,5,0)});
            stackPanel.Children.Add(new Button { Content = "1 Pt Make", Margin = new Thickness(5, 0, 5, 0) });
            stackPanel.Children.Add(new Button { Content = "2 Pt Attempt", Margin = new Thickness(5, 0, 5, 0) });
            stackPanel.Children.Add(new Button { Content = "2 Pt Make", Margin = new Thickness(5, 0, 5, 0) });
            stackPanel.Children.Add(new Button { Content = "Block", Margin = new Thickness(5, 0, 5, 0) });
            stackPanel.Children.Add(new Button { Content = "Steal", Margin = new Thickness(5, 0, 5, 0) });
            stackPanel.Children.Add(new Button { Content = "Foul", Margin = new Thickness(5, 0, 5, 0) });
            return stackPanel;
        }
    }
}
