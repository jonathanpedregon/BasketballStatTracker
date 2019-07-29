using BasketballStatTracker.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BasketballStatTracker.Windows
{
    /// <summary>
    /// Interaction logic for CreateGameWindow.xaml
    /// </summary>
    public partial class CreateGameWindow : Window
    {
        public Game Game { get; set; }
        public CreateGameWindow()
        {
            InitializeComponent();
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            var elements = Canvas.Children;
            var textBoxes = new List<TextBox>();
            foreach(var element in elements)
            {
                var textBox = element as TextBox;
                if (textBox != null && !string.IsNullOrEmpty(textBox.Text))
                    textBoxes.Add(textBox);
            }
            var teamOnePlayers = textBoxes.Where(x => x.Name.Contains("Team1")).Select(x => new Player(x.Text));
            var teamTwoPlayers = textBoxes.Where(x => x.Name.Contains("Team2")).Select(x => new Player(x.Text));
            var teamOne = new Team(teamOnePlayers.Where(x => x.Name != ));
            var teamTwo = new Team(teamTwoPlayers.ToList());
            Game = new Game(teamOne, teamTwo);
        }
    }
}
