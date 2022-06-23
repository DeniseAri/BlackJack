using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    internal class Deck
    {

        List<Card> Cards = new List<Card>();
        public Deck()
        {
            Init();
            Shuffle();
        }

        private void Init()
        {
            MakeSuit("hearts");
            MakeSuit("spades");
            MakeSuit("diamonds");
            MakeSuit("clubs");
        }

        public void Shuffle()
        {
            Random random = new Random();

                for (int i = 0; i < 1000; i++)
                {
                    int rndIndex = random.Next(0, 51);

                    Card firstCard = Cards[0];
                    Card randomCard = Cards[rndIndex];

                    //Swap randomCard with the card at the firstIndex 1000 times
                    Cards[0] = randomCard;
                    Cards[rndIndex] = firstCard;
                }

                foreach (Card c in Cards)
                {
                    Console.WriteLine(c.GetName());
                }
        }

        public void MakeSuit(String suitname)
        {
            for (int i = 1; i < 14; i++)
            {
                Card c = new Card(suitname, i);
                Cards.Add(c);
            }
        }
        public void Show()
        {
            foreach (Card c in Cards)
            {
                Console.WriteLine(c.GetName());
            }
        }

        public Card GetTopCard()
        {
            Card c = Cards[0];
            Cards.RemoveAt(0);
            return c;
        }

        public void Insert(Card c)
        {
            Cards.Add(c);
        }


    }
}
