using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    public class Cards
    {
        //This class just lays the foundation for our cards. No setter method needed because we set the values at card creation and only ever need to read the value after
        public string Suit { get; }
        public string Rank { get; }

        public Cards(string suit, string rank)
        {
            Suit = suit;
            Rank = rank;
        }
    }
}
