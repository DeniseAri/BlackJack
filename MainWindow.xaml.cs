using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
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

namespace BlackJack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        BlackJackGame game = new BlackJackGame();

        bool hit = false;
        bool stand = false;

        public MainWindow()
        {
            InitializeComponent();
            game.Start();
        }


        private void ButtonHit(object sender, RoutedEventArgs e)
        {
            System.IO.Stream str = Properties.Resources.hit;
            System.Media.SoundPlayer playerhit = new System.Media.SoundPlayer(str);
            playerhit.Play();
            Card c = game.DealCardToPlayer();

        }

        private void ButtonStand(object sender, RoutedEventArgs e)
        {
            btnHit.IsEnabled = false;
            btnStand.IsEnabled = false;
            game.ScoreCheck();

        }

        public void ShowImage(string filename)
        {
            Uri uri = new Uri(@"images/Cards/" + filename, UriKind.Relative);
            BitmapImage bitmap = new BitmapImage(uri);
            Image image = new Image();
            image.Margin = new Thickness(-15);
            image.Source = bitmap;
            image.Width = 130;

            UserCardPanel.Children.Add(image);
        }

        public void ShowImageDealer(string filename)
        {
            Uri uri = new Uri(@"images/Cards/" + filename, UriKind.Relative);
            BitmapImage bitmap = new BitmapImage(uri);
            Image image = new Image();
            image.Margin = new Thickness(-15);
            image.Source = bitmap;
            image.Width = 130;

            DealerCardPanel.Children.Add(image);
        }

        public void ScoreText( string text)
        {

            TextBlock textBlock = new TextBlock();

            textBlock.Text = "Your Score: " + text;

            textBlock.FontSize = 25;

            textBlock.Foreground = Brushes.White;

            ScorePanel.Children.Add(textBlock);
        }

        public void DealerScoreText(string text)
        {

            TextBlock textBlock = new TextBlock();

            textBlock.Text = "Dealer's Score: " + text;

            textBlock.FontSize = 25;

            textBlock.Foreground = Brushes.White;

            DealerScorePanel.Children.Add(textBlock);

        }

        public void WinnerText(string text)
        {

            TextBlock textBlock = new TextBlock();

            if ((game.GetDealerSum() != game.GetPlayerSum()) && game.GetPlayerSum() != 21) {
                textBlock.Text = " " + text + " WON! ";
            }
            else
            {
                textBlock.Text = " " + text + " ";
            }

            textBlock.FontSize = 32;

            textBlock.Background = Brushes.GreenYellow;

            textBlock.Foreground = Brushes.Black;

            Winner.Children.Add(textBlock);

        }

        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            System.IO.Stream str = Properties.Resources.coin;
            System.Media.SoundPlayer playerbuy = new System.Media.SoundPlayer(str);
            playerbuy.Play();
            DealerScorePanel.Children.Clear();
            DealerCardPanel.Children.Clear();
            ScorePanel.Children.Clear();
            UserCardPanel.Children.Clear();
            Winner.Children.Clear();

            game.DeckRedo();

        }
    }
}
