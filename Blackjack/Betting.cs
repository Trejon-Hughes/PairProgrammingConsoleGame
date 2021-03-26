using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    class Betting
    {
        //This class just handles keeping track of the money for betting
        //The wallet is public so the program can access and manipulate it
        //Would probably be better to write helper methods to retrieve the value and manipulate it.
        public decimal wallet = 100;
        
        public void Win(decimal bet)
        {
            wallet += bet;

        }

        public void Lose(decimal bet)
        {
            wallet -= bet;
        }

        public void Blackjack (decimal bet)
        {
            wallet += bet * 1.5m;
        }
    }
}
