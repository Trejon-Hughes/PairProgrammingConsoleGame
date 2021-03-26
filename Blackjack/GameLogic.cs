using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    public class GameLogic
    {
        //We create a new deck to store the cards
        //As well as lists to act as the player's and dealer's hands
        Deck deck = new Deck();
        List<Cards> playerHand = new List<Cards>();
        List<Cards> dealerHand = new List<Cards>();
        bool playerTurn = true;
        bool player = true;
        public int playerTotal = 0;
        public int dealerTotal = 0;

        //These all just allow the main program to access the variables here without making them public, as should have been done in Betting. Would not be needed if all of the game logic was in here and not split between here and the main program
        public void PlayerTurn()
        {
            playerTurn = true;
        }
        public void DealerTurn()
        {
            playerTurn = false;
        }
        public void Player()
        {
            player = true;
        }
        public void Dealer()
        {
            player = false;
        }
        //This adds cards to our current deck
        public void PopulateDeck(int numberOfDecks)
        {
            deck.AddCardsToDeck(numberOfDecks);
        }
        
        //GameStart is all the logic needed at the start of every round
        public void GameStart()
        {
            Console.Clear();
            playerHand.Clear();
            dealerHand.Clear();
            playerHand.Add(deck.DrawCard());
            playerHand.Add(deck.DrawCard());
            dealerHand.Add(deck.DrawCard());
            dealerHand.Add(deck.DrawCard());
            player = false;
            DealerTotalCalc();
            player = true;
            PlayerTotalCalc();
        }

        //This draws from the deck and adds the card to the respective player
        public void Draw(bool playerTurn)
        {
            Console.Clear();
            if (playerTurn)
            {
                playerHand.Add(deck.DrawCard());
            }
            else
            {
                dealerHand.Add(deck.DrawCard());
            }
        }

        //This and DealerTotalCalc both handle calculating the hand's total worth
        //The only difference between the two is if it's the player's turn we only count the first card in the dealer's hand. Could probably be combined into one method, but this way is probably easier to use and read.
        public void PlayerTotalCalc()
        {
            int numbersOfEleven = 0;
            int cardCount = 0;
            foreach (Cards card in playerHand)
            {
                if (card.Rank == "J" || card.Rank == "Q" || card.Rank == "K")
                {
                    cardCount += 10;
                }
                else if (card.Rank == "A")
                {
                    cardCount += 11;
                }
                else
                {
                    cardCount += Int32.Parse(card.Rank);
                }
                if (card.Rank == "A")
                {
                    numbersOfEleven++;
                }
                if (cardCount > 21 && numbersOfEleven > 0)
                {
                    cardCount -= 10;
                    numbersOfEleven--;
                }
            }
            playerTotal = cardCount;
            player = true;
            deck.PrintCard(playerHand, playerTurn, player);
            Console.WriteLine($"Player Hand: {playerTotal}");
            Console.WriteLine();
        }

        public void DealerTotalCalc()
        {
            int numbersOfEleven = 0;
            int cardCount = 0;
            if (playerTurn)
            {
                if (dealerHand[0].Rank == "J" || dealerHand[0].Rank == "Q" || dealerHand[0].Rank == "K")
                {
                    cardCount += 10;
                }
                else if (dealerHand[0].Rank == "A")
                {
                    cardCount += 11;
                }
                else
                {
                    cardCount += Int32.Parse(dealerHand[0].Rank);
                }
            }
            else
            {
                foreach(Cards card in dealerHand)
            {
                    if (card.Rank == "J" || card.Rank == "Q" || card.Rank == "K")
                    {
                        cardCount += 10;
                    }
                    else if (card.Rank == "A")
                    {
                        cardCount += 11;
                    }
                    else
                    {
                        cardCount += Int32.Parse(card.Rank);
                    }
                    if (card.Rank == "A")
                    {
                        numbersOfEleven++;
                    }
                    if (cardCount > 21 && numbersOfEleven > 0)
                    {
                        cardCount -= 10;
                        numbersOfEleven--;
                    }
                }
            }
            dealerTotal = cardCount;
            player = false;
            deck.PrintCard(dealerHand, playerTurn, player);
            Console.WriteLine($"Dealer Hand: {dealerTotal}");
            Console.WriteLine();
        }
        //This runs ShuffleDeck from the Deck class if it returns true
        //Just a method for our game logic to run the check when we ask
        public bool ShuffleCheck(bool forceShuffle)
        {
            if (deck.ShuffleDeck(forceShuffle))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
