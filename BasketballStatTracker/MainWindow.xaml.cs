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
            MainStackPanel.Children.Add(new Label { Content = "Team One: 0", FontWeight = FontWeights.Bold, Name = "Team1Label" });
            GetTeamStackPanels(CurrentGame.Team1);
            MainStackPanel.Children.Add(new Label { Content = "Team Two: 0", FontWeight = FontWeights.Bold, Name = "Team2Label" });
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
            stackPanel.Children.Add(GetOnePointMakeButton(player));
            stackPanel.Children.Add(GetTwoPointAttemptButton(player));
            stackPanel.Children.Add(GetTwoPointMakeButton(player));
            stackPanel.Children.Add(GetBlockButton(player));
            stackPanel.Children.Add(GetStealButton(player));
            stackPanel.Children.Add(GetFoulButton(player));
            stackPanel.Children.Add(new Label { Content = player.ToString(), Name = $"{player.Name}Label" });
            return stackPanel;
        }

        private Button GetFoulButton(Player player)
        {
            var button = new Button { Content = "Foul", Margin = new Thickness(5, 0, 5, 0), Name = $"{player.Name}_Foul", Width = 50 };
            button.Click += new RoutedEventHandler(FoulClick);
            return button;
        }

        private void FoulClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var playerName = button.Name.Split('_')[0];

            var team1Player = CurrentGame.Team1.Players.SingleOrDefault(x => x.Name == playerName);
            if (team1Player != null)
            {
                team1Player.Fouls++;
                RefreshPlayerStatline(playerName, team1Player);
                return;
            }
            var team2Player = CurrentGame.Team2.Players.SingleOrDefault(x => x.Name == playerName);
            if (team2Player != null)
            {
                team2Player.Fouls++;
                RefreshPlayerStatline(playerName, team2Player);
            }
        }

        private Button GetStealButton(Player player)
        {
            var button = new Button { Content = "Steal", Margin = new Thickness(5, 0, 5, 0), Name = $"{player.Name}_Steal", Width = 50 };
            button.Click += new RoutedEventHandler(StealClick);
            return button;
        }

        private void StealClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var playerName = button.Name.Split('_')[0];

            var team1Player = CurrentGame.Team1.Players.SingleOrDefault(x => x.Name == playerName);
            if (team1Player != null)
            {
                team1Player.Steals++;
                RefreshPlayerStatline(playerName, team1Player);
                return;
            }
            var team2Player = CurrentGame.Team2.Players.SingleOrDefault(x => x.Name == playerName);
            if (team2Player != null)
            {
                team2Player.Steals++;
                RefreshPlayerStatline(playerName, team2Player);
            }
        }

        private Button GetBlockButton(Player player)
        {
            var button = new Button { Content = "Block", Margin = new Thickness(5, 0, 5, 0), Name = $"{player.Name}_Block", Width= 50 };
            button.Click += new RoutedEventHandler(BlockClick);
            return button;
        }

        private void BlockClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var playerName = button.Name.Split('_')[0];

            var team1Player = CurrentGame.Team1.Players.SingleOrDefault(x => x.Name == playerName);
            if (team1Player != null)
            {
                team1Player.Blocks++;
                RefreshPlayerStatline(playerName, team1Player);
                return;
            }
            var team2Player = CurrentGame.Team2.Players.SingleOrDefault(x => x.Name == playerName);
            if (team2Player != null)
            {
                team2Player.Blocks++;
                RefreshPlayerStatline(playerName, team2Player);
            }
        }

        private Button GetTwoPointMakeButton(Player player)
        {
            var button = new Button { Content = "2 Pt Make", Margin = new Thickness(5, 0, 5, 0), Name = $"{player.Name}_2PtMake", Width = 80 };
            button.Click += new RoutedEventHandler(TwoPointMakeClick);
            return button;
        }

        private void TwoPointMakeClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var playerName = button.Name.Split('_')[0];

            var team1Player = CurrentGame.Team1.Players.SingleOrDefault(x => x.Name == playerName);
            if (team1Player != null)
            {
                team1Player.TwoPointMakes++;
                team1Player.TwoPointAttempts++;
                RefreshPlayerStatline(playerName, team1Player);
                OpenAssistWindow(CurrentGame.Team1, team1Player);
                return;
            }
            var team2Player = CurrentGame.Team2.Players.SingleOrDefault(x => x.Name == playerName);
            if (team2Player != null)
            {
                team2Player.TwoPointMakes++;
                team2Player.TwoPointAttempts++;
                RefreshPlayerStatline(playerName, team2Player);
                OpenAssistWindow(CurrentGame.Team1, team1Player);
            }
        }

        private Button GetTwoPointAttemptButton(Player player)
        {
            var button = new Button { Content = "2 Pt Attempt", Margin = new Thickness(5, 0, 5, 0), Name = $"{player.Name}_2PtAttempt", Width = 100 };
            button.Click += new RoutedEventHandler(TwoPointAttemptClick);
            return button;
        }

        private void TwoPointAttemptClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var playerName = button.Name.Split('_')[0];

            var team1Player = CurrentGame.Team1.Players.SingleOrDefault(x => x.Name == playerName);
            if (team1Player != null)
            {
                team1Player.TwoPointAttempts++;
                RefreshPlayerStatline(playerName, team1Player);
                OpenReboundWindow(CurrentGame.Team1.Players.SingleOrDefault(x => x.Name == playerName));
                return;
            }
            var team2Player = CurrentGame.Team2.Players.SingleOrDefault(x => x.Name == playerName);
            if (team2Player != null)
            {
                team2Player.TwoPointAttempts++;
                RefreshPlayerStatline(playerName, team2Player);
                OpenReboundWindow(CurrentGame.Team2.Players.SingleOrDefault(x => x.Name == playerName));
            }
        }

        private Button GetOnePointMakeButton(Player player)
        {
            var button = new Button { Content = "1 Pt Make", Margin = new Thickness(5, 0, 5, 0), Name = $"{player.Name}_1PtMake", Width = 80 };
            button.Click += new RoutedEventHandler(OnePointMakeButtonClick);
            return button;
        }

        private void OnePointMakeButtonClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var playerName = button.Name.Split('_')[0];

            var team1Player = CurrentGame.Team1.Players.SingleOrDefault(x => x.Name == playerName);
            if (team1Player != null)
            {
                team1Player.OnePointMakes++;
                team1Player.OnePointAttempts++;
                RefreshPlayerStatline(playerName, team1Player);
                OpenAssistWindow(CurrentGame.Team1, team1Player);
                return;
            }
            var team2Player = CurrentGame.Team2.Players.SingleOrDefault(x => x.Name == playerName);
            if (team2Player != null)
            {
                team2Player.OnePointMakes++;
                team2Player.OnePointAttempts++;
                RefreshPlayerStatline(playerName, team2Player);
                OpenAssistWindow(CurrentGame.Team2, team2Player);
            }
        }

        private Button GetOnePointAttemptButton(Player player)
        {
            var button = new Button { Content = "1 Pt Attempt", Name = $"{player.Name}_1PtAttempt", Margin = new Thickness(5, 0, 5, 0), Width= 100 };
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
                OpenReboundWindow(CurrentGame.Team1.Players.SingleOrDefault(x => x.Name == playerName));
                return;
            }
            var team2Player = CurrentGame.Team2.Players.SingleOrDefault(x => x.Name == playerName);
            if (team2Player != null)
            {
                team2Player.OnePointAttempts++;
                RefreshPlayerStatline(playerName, team2Player);
                OpenReboundWindow(CurrentGame.Team2.Players.SingleOrDefault(x => x.Name == playerName));
            }
        }

        private void RefreshPlayerStatline(string playerName, Player player)
        {
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
            RefreshScore();
        }

        private void OpenAssistWindow(Team team, Player scoringPlayer)
        {
            var possibleAssistPlayers = team.Players.Where(x => x != scoringPlayer).ToList();
            var assistWindow = new AssistWindow(possibleAssistPlayers);
            assistWindow.ShowDialog();
            var assistPlayer = assistWindow.AssistPlayer;
            if(assistPlayer != null)
            {
                var team1Player = CurrentGame.Team1.Players.SingleOrDefault(x => x.Name == assistPlayer);
                if (team1Player != null)
                {
                    team1Player.Assists++;
                    RefreshPlayerStatline(assistPlayer, team1Player);
                    return;
                }
                var team2Player = CurrentGame.Team2.Players.SingleOrDefault(x => x.Name == assistPlayer);
                if (team2Player != null)
                {
                    team2Player.Assists++;
                    RefreshPlayerStatline(assistPlayer, team2Player);
                }
            }
        }

        private void OpenReboundWindow(Player playerThatMissedShot)
        {
            var reboundWindow = new ReboundWindow(CurrentGame);
            reboundWindow.ShowDialog();
            var reboundPlayer = reboundWindow.ReboundPlayer;
            if(reboundPlayer != null)
            {
                var team1Player = CurrentGame.Team1.Players.SingleOrDefault(x => x.Name == reboundPlayer);
                if (team1Player != null)
                {
                    if (CurrentGame.Team1.Players.Contains(playerThatMissedShot))
                        team1Player.OffensiveRebounds++;
                    else
                        team1Player.DefensiveRebounds++;
                    RefreshPlayerStatline(reboundPlayer, team1Player);
                    return;
                }
                var team2Player = CurrentGame.Team2.Players.SingleOrDefault(x => x.Name == reboundPlayer);
                if (team2Player != null)
                {
                    if (CurrentGame.Team2.Players.Contains(playerThatMissedShot))
                        team2Player.OffensiveRebounds++;
                    else
                        team2Player.DefensiveRebounds++;
                    RefreshPlayerStatline(reboundPlayer, team2Player);
                }
            }
        }

        private void RefreshScore()
        {
            foreach(var child in MainStackPanel.Children)
            {
                var label = child as Label;
                if(label != null)
                {
                    if (label.Name == "Team1Label")
                        label.Content = $"Team One: {CurrentGame.Team1.Points}";
                    if (label.Name == "Team2Label")
                        label.Content = $"Team Two: {CurrentGame.Team2.Points}";
                }
            }
        }
    }
}
