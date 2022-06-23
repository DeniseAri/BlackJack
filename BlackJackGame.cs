using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace BlackJack
{
    internal class BlackJackGame
    {

        Deck deck = new Deck();
        List<Card> playerCards = new List<Card>();
        List<Card> dealerCards = new List<Card>();

        public void Start()
        {

            DealCardToDealer();
            DealFacedownCardToDealer();
            DealCardToPlayer();
            DealCardToPlayer();
        }

        public void Hit()
        {
            DealCardToPlayer();

        }

        
        
        public Card DealCardToPlayer()
        {
            Card c = deck.GetTopCard();
            playerCards.Add(c);
            GetPlayerSum();
            ((MainWindow)System.Windows.Application.Current.MainWindow).ShowImage(c.GetName() + ".png");
            ((MainWindow)System.Windows.Application.Current.MainWindow).ScorePanel.Children.Clear();
            ((MainWindow)System.Windows.Application.Current.MainWindow).ScoreText((GetPlayerSum()).ToString());

            if (GetPlayerSum() < 21)
            {
                return c;
            }
            else if (GetPlayerSum() > 21) //if the card Player picks up makes total more than 21
            {
                ((MainWindow)System.Windows.Application.Current.MainWindow).btnHit.IsEnabled = false;
                ((MainWindow)System.Windows.Application.Current.MainWindow).btnStand.IsEnabled = false;
                ScoreCheck();
                return c;
            }
            else if(GetPlayerSum() == 21)// if the card Player picks up makes total == 21
            {
                
                ((MainWindow)System.Windows.Application.Current.MainWindow).btnHit.IsEnabled = false;
                ((MainWindow)System.Windows.Application.Current.MainWindow).btnStand.IsEnabled = false;
                ScoreCheck();
                return c;
            }

            return c;
        }

        public void DealCardToDealer()
        {
            Card c = deck.GetTopCard();
            dealerCards.Add(c);
            GetDealerSum();
            ((MainWindow)System.Windows.Application.Current.MainWindow).ShowImageDealer(c.GetName() + ".png");
        }

        public void DealFacedownCardToDealer()
        {
            Card c = deck.GetTopCard();
            dealerCards.Add(c);
            GetDealerSum();
            ((MainWindow)System.Windows.Application.Current.MainWindow).ShowImageDealer("CardBack.png");
        }

        internal int GetDealerSum()
        {
            int sum = 0;
            foreach (Card c in dealerCards)
            {
                if (sum <= 10 && (c.GetName() == "Ahearts" || c.GetName() == "Adiamonds" || c.GetName() == "Aspades" || c.GetName() == "Aclubs"))
                {
                    sum += 10;
                }
                sum += GetBlackjackValue(c);
            }
            return sum;
        }

        public List<Card> GetDealerCards()
        {
            return dealerCards;
        }

        internal int GetPlayerSum()
        {
            int sum = 0;
            foreach (Card c in playerCards)
            {
                if(sum <= 10 && (c.GetName() == "Ahearts" || c.GetName() == "Adiamonds" || c.GetName() == "Aspades" || c.GetName() == "Aclubs"))
                {
                    sum += 10; //Aces are equal to 11 if the hand total is less than 10
                }
                sum += GetBlackjackValue(c);

            }
            return sum;
        }

        public int GetBlackjackValue(Card c)
        {
            if (c.Rank > 10)
            {
                return 10;
            }
            return c.Rank;
        }

        public List<Card> GetPlayerCards()
        {
            return playerCards;
        }

        public void DeckRedo()
        {
            foreach (Card c in playerCards.ToList())
            {
                playerCards.Remove(c);
                deck.Insert(c);
            }
            foreach (Card c in dealerCards.ToList())
            {
                dealerCards.Remove(c);
                deck.Insert(c);
            }
            ((MainWindow)System.Windows.Application.Current.MainWindow).btnHit.IsEnabled = true;
            ((MainWindow)System.Windows.Application.Current.MainWindow).btnStand.IsEnabled = true;
            deck.Shuffle();


            DealCardToDealer();
            DealFacedownCardToDealer();
            DealCardToPlayer();
            DealCardToPlayer();
        }

        public String ScoreCheck()
        {

            ((MainWindow)System.Windows.Application.Current.MainWindow).DealerCardPanel.Children.Clear();

            foreach (Card c in dealerCards)
            {
                ((MainWindow)System.Windows.Application.Current.MainWindow).ShowImageDealer(c.GetName() + ".png");
            }

            string winner = " ";

            if (GetDealerSum() > 21 && GetPlayerSum() < 21) //If Dealer busts and player didnt
            {
                winner = "YOU";
            }
            
            else if(GetPlayerSum() > 21 && GetDealerSum() < 21) //If Player busts and dealer didn't
            {
                winner = "DEALER";
            }
            else if(GetDealerSum() > 21 && GetPlayerSum() > 21) //If both player and dealer busts
            {
                winner = "DRAW";
            }
            else if(GetPlayerSum() == 21 && GetDealerSum() == 21) //If both Player and Dealer get BlackJack
            {
                winner = "DRAW";
            }
            else if (GetDealerSum() == 21 && GetPlayerSum() != 21) //If Dealer gets BlackJack
            {
                winner = "DEALER";
                ((MainWindow)System.Windows.Application.Current.MainWindow).btnHit.IsEnabled = false;
                ((MainWindow)System.Windows.Application.Current.MainWindow).btnStand.IsEnabled = false;
            }
            else if (GetPlayerSum() == 21 && GetDealerSum() != 21) //If Player gets BlackJack
            {
                winner = "BLACKJACK";
            }
            else
            {

                if (GetDealerSum() > GetPlayerSum()) //If player stands and Dealer has higher score a
                {
                    winner = "DEALER";
                }
                else if(GetPlayerSum() > GetDealerSum()) //If player stands and player has higher score
                {
                    winner = "YOU";
                }
                else //if player stands and they have the same score as the dealer
                {
                    winner = "DRAW";
                }

            }

            if(winner == "YOU" || winner == "BLACKJACK")
            {
                //Play win sound
                System.IO.Stream str = Properties.Resources.win;
                System.Media.SoundPlayer playerwin = new System.Media.SoundPlayer(str);
                playerwin.Play();
            }
            else if(winner == "DEALER")
            {
                //Play lose soud
                System.IO.Stream str = Properties.Resources.lose;
                System.Media.SoundPlayer playerlose = new System.Media.SoundPlayer(str);
                playerlose.Play();
            }

           

            ((MainWindow)System.Windows.Application.Current.MainWindow).DealerScorePanel.Children.Clear();
            ((MainWindow)System.Windows.Application.Current.MainWindow).DealerScoreText((GetDealerSum()).ToString());
            ((MainWindow)System.Windows.Application.Current.MainWindow).WinnerText(winner);
            return winner;

        }

    }
}
