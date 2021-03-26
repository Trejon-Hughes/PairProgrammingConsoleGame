using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    public class Deck
    {
        Random random = new Random();
        List<Cards> deck = new List<Cards>();
        Audio shuffle = new Audio();
        List<string> suits = new List<string>() { "Hearts", "Spades", "Diamonds", "Clubs" };
        List<string> ranks = new List<string>() { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
        int numberOfCardsInDeck;

        //This method refills the deck/shoe list with the same cards as a standard playing deck
        //numberOfDecks simulates creating a shoe with that many decks
        public void AddCardsToDeck(int numberOfDecks)
        {
            deck.Clear();
            for (int i = 0; i < numberOfDecks; i++)
            {
                foreach (string suit in suits)
                {
                    foreach (string rank in ranks)
                    {
                        Cards card = new Cards(suit, rank);
                        deck.Add(card);
                    }
                }
            }
            numberOfCardsInDeck = deck.Count;
            shuffle.ShuffleSound();
        }
        //This is just a check that accepts a bool override
        //If the number of cards in the deck is less than half, runs AddCardsToDeck
        public bool ShuffleDeck(bool forceShuffle)
        {
            if (forceShuffle)
            {
                AddCardsToDeck(1);
                return true;
            }
            else
            {
                if (deck.Count < numberOfCardsInDeck / 2)
                {
                    AddCardsToDeck(1);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        //This is the method for printing the cards, and boy is it a doozie
        //If its the player's turn, and we're printing the dealer's cards, it obscures the second card for the dealer, otherwise everything prints as normal. If the card symbols don't display, may need to change the encoding for the console encoding to UTF-8 using Console.OutputEncoding = System.Text.Encoding.UTF8
        public void PrintCard(List<Cards> hand, bool playerTurn, bool player)
        {
            Dictionary<string, string> suits = new Dictionary<string, string>() { { "Spades", "♠" }, { "Diamonds", "♦" }, { "Hearts", "♥" }, { "Clubs", "♣" } };
            string suit;
            string rank;
            string space;
            foreach (Cards card in hand)
            {
                Console.Write("┌─────────┐ ");
            }
            Console.WriteLine();
            int counter = 1;
            foreach (Cards card in hand)
            {
                if (playerTurn && !player && counter == 2)
                {
                    Console.Write("|░░░░░░░░░| ");
                }
                else
                {
                    rank = card.Rank;
                    if (rank == "10")
                    {
                        space = "";
                    }
                    else
                    {
                        space = " ";
                    }
                    Console.Write($"│{rank}{space}       │ ");
                    counter++;
                }
            }
            Console.WriteLine();
            counter = 1;
            foreach (Cards card in hand)
            {
                if (playerTurn && !player && counter == 2)
                {
                    Console.Write("|░░░░░░░░░| ");
                }
                else
                {
                    Console.Write("│         │ ");
                    counter++;
                }
            }
            Console.WriteLine();
            counter = 1;
            foreach (Cards card in hand)
            {
                if (playerTurn && !player && counter == 2)
                {
                    Console.Write("|░░░░░░░░░| ");
                }
                else
                {
                    Console.Write("│         │ ");
                    counter++;
                }
            }
            Console.WriteLine();
            counter = 1;
            foreach (Cards card in hand)
            {
                if (playerTurn && !player && counter == 2)
                {
                    Console.Write("|░░░░░░░░░| ");
                }
                else
                {
                    suit = card.Suit;
                    Console.Write($"│    {suits[suit]}    │ ");
                    counter++;
                }
            }
            Console.WriteLine();
            counter = 1;
            foreach (Cards card in hand)
            {
                if (playerTurn && !player && counter == 2)
                {
                    Console.Write("|░░░░░░░░░| ");
                }
                else
                {
                    Console.Write("│         │ ");
                    counter++;
                }
            }
            Console.WriteLine();
            counter = 1;
            foreach (Cards card in hand)
            {
                if (playerTurn && !player && counter == 2)
                {
                    Console.Write("|░░░░░░░░░| ");
                }
                else
                {
                    Console.Write("│         │ ");
                    counter++;
                }
            }
            Console.WriteLine();
            counter = 1;
            foreach (Cards card in hand)
            {
                suit = card.Suit;
                rank = card.Rank;
                if (playerTurn && !player && counter == 2)
                {
                    Console.Write("|░░░░░░░░░| ");
                }
                else
                {
                    if (rank == "10")
                    {
                        space = "";
                    }
                    else
                    {
                        space = " ";
                    }
                    Console.Write($"│       {space}{rank}│ ");
                    counter++;
                }
            }
            Console.WriteLine();
            foreach (Cards card in hand)
            {
                Console.Write("└─────────┘ ");
            }
            Console.WriteLine();
        }

        //This is the method used to "draw" a card. It takes a random card from the deck list and returns it, while also deleting it from the list to simulate a card draw
        public Cards DrawCard()
        {
            int listPlacement = random.Next(0, deck.Count);
            Cards drawnCard = deck[listPlacement];
            deck.RemoveAt(listPlacement);
            return drawnCard;
        }


    }
}
