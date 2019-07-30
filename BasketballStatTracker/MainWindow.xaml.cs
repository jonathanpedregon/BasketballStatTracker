using BasketballStatTracker.Models;
using BasketballStatTracker.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
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
            if (successfulCreation != null && (bool)successfulCreation)
            {
                CurrentGame = createGameWindow.Game;
                CreatGrid();
            }
        }

        private void CreatGrid()
        {
            MainStackPanel.Children.Add(new Label { Content = "Team One", FontWeight = FontWeights.Bold });
            GetTeamStackPanels(CurrentGame.Team1);
            MainStackPanel.Children.Add(new Label { Content = "Team Two", FontWeight = FontWeights.Bold });
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

        private StackPanel GetPlayerStackPanel(Player player)
        {
            var stackPanel = new StackPanel { Orientation = Orientation.Horizontal, Name = $"{player.Name}stackpanel" };
            stackPanel.Children.Add(new Label { Content = player.Name, Width = 75 });
            stackPanel.Children.Add(GetOnePointAttemptButton(player));

            stackPanel.Children.Add(new Button { Content = "1 Pt Make", Margin = new Thickness(5, 0, 5, 0) });
            stackPanel.Children.Add(new Button { Content = "2 Pt Attempt", Margin = new Thickness(5, 0, 5, 0) });
            stackPanel.Children.Add(new Button { Content = "2 Pt Make", Margin = new Thickness(5, 0, 5, 0) });
            stackPanel.Children.Add(new Button { Content = "Block", Margin = new Thickness(5, 0, 5, 0) });
            stackPanel.Children.Add(new Button { Content = "Steal", Margin = new Thickness(5, 0, 5, 0) });
            stackPanel.Children.Add(new Button { Content = "Foul", Margin = new Thickness(5, 0, 5, 0) });
            stackPanel.Children.Add(new Label { Content = player.ToString(), Name = $"{player.Name}Label" });
            return stackPanel;
        }

        private Button GetOnePointAttemptButton(Player player)
        {
            var button = new Button { Content = "1 Pt Attempt", Name = $"{player.Name}_1PtAttempt", Margin = new Thickness(5, 0, 5, 0) };
            button.Click += new RoutedEventHandler(OnePtAttemptClick);
            return button;
        }
        private void OnePtAttemptClick(object sender, EventArgs e)
        {
            var button = sender as Button;
            var playerName = button.Name.Split('_')[0];

            var team1Player = CurrentGame.Team1.Players.SingleOrDefault(x => x.Name == playerName);
            if (team1Player != null)
            {
                team1Player.OnePointAttempts++;
                RefreshPlayerStatline(playerName, team1Player);
                return;
            }
            var team2Player = CurrentGame.Team2.Players.SingleOrDefault(x => x.Name == playerName);
            if (team2Player != null)
            {
                team2Player.OnePointAttempts++;
                RefreshPlayerStatline(playerName, team2Player);
                var statLine = team2Player.ToString();
            }
        }

        private void RefreshPlayerStatline(string playerName, Player player)
        {
            var mainStackPanel = MainStackPanel.Children;
            foreach (var child in MainStackPanel.Children)
            {
                var stackpanel = child as StackPanel;
                if (stackpanel != null && stackpanel.Name == $"{playerName}stackpanel")
                {
                    foreach (var stackpanelChild in stackpanel.Children)
                    {
                        var playerStackPanel = stackpanelChild as Label;
                        if (playerStackPanel != null && playerStackPanel.Name == $"{playerName}Label")
                            playerStackPanel.Content = player.ToString();
                    }
                }
            }
        }
    }
}
