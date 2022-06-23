using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    internal class Card
    {
        
        public string Suit;
        public int Rank;

        public Card(String S, int R)
        {
            Suit = S;
            Rank = R;
        }

        public string GetName()
        {
            if (Rank == 1)
            {
                return "A" + Suit;
            }
            if (Rank == 11)
            {
                return "J" + Suit;
            }
            if (Rank == 12)
            {
                return "Q" + Suit;
            }
            if (Rank == 13)
            {
                return "K" + Suit;
            }
            return Rank +  Suit;
        }

        public string GetFilename()
        {
            throw new Exception("Write me");
        }
    }

   
    }
