using System;

namespace Blackjack
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Majority of the logic in this class could be moved to GameLogic and we just call on methods here.
            //The OOP design of this app means that new features such as custom betting amounts, bigger card shoes, splitting pairs, doubling down, new rulesets, ect. could be added fairly easily, given some cleaning up of the code here and some parameters in some of the other methods
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            bool bettingRound = true;
            bool gameGoing = false;
            bool blackjack = false;
            bool bust = false;
            bool playerTurn = true;
            GameLogic game = new GameLogic();
            Audio audio = new Audio();
            Betting betting = new Betting();
            decimal bet = 0;
            string playerResponse;

            Console.WriteLine("Press 1 play Blackjack. Press 3 to exit");
            string playerPlayGame = Console.ReadLine();
            //Prompt the user to reenter if they don't enter 1 or 3
            while (playerPlayGame != "1" && playerPlayGame != "3")
            {
                Console.Clear();
                Console.WriteLine("Please select 1 to play Blackjack or 3 to exit.");
                playerPlayGame = Console.ReadLine();
            }
            game.PopulateDeck(1);
            //pauses the program momentarially to allow the audio to play
            System.Threading.Thread.Sleep(1000);
            //runs the main game while the selection isn't 3
            while (playerPlayGame != "3")
            {
                //this runs the check for "shuffling" our deck (refilling it with new cards)
                if (game.ShuffleCheck(false))
                {
                    Console.Clear();
                    Console.WriteLine("Shuffling deck. Please wait...");
                    System.Threading.Thread.Sleep(1000);

                }
                game.PlayerTurn();
                game.Player();
                Console.Clear();
                do
                {
                    //Prompts the user to bet an amount and if the amount isn't within 0.01 and their current max reprompts them.
                    Console.WriteLine($"Remaining Cash: ${betting.wallet.ToString("F")}\n" +
                    $"How much would you like to bet?");
                    string betAmount = Console.ReadLine();
                    decimal.TryParse(betAmount.Replace("$", " "), out bet);
                    //This one was honestly impressive. The last two checks return true or false together. It checks if the bet rounded down to two places is the same number as the bet and it also checks if the user input is different from the bet. Both cases mean the user put in a number with more than 2 decimal places. The first check is for numbers a few decimal places long, the second check is for numbers that are so long that TryParse starts rounding them up to the next number.
                    if (bet <= 0 || bet > betting.wallet || (decimal.Round(bet, 2) != bet ||betAmount != Convert.ToString(bet)))
                    {
                        Console.Clear();
                        Console.WriteLine($"Please choose a number between $0.01 and ${betting.wallet.ToString("F")}. Two decimals max.");
                        System.Threading.Thread.Sleep(2000);
                        Console.Clear();
                        bet = 0;
                    }
                    else
                    {
                        bettingRound = false;
                        audio.BetSound();
                    }
                } while (bettingRound == true);
                game.GameStart();
                do
                {
                    //resets the variables every new round
                    playerTurn = true;
                    bust = false;
                    blackjack = false;
                    bettingRound = true;

                    //if the opening hand is 21, immediately skips to the end
                    if (game.playerTotal == 21)
                    {
                        playerTurn = false;
                        blackjack = true;
                        playerResponse = "2";
                    }
                    else
                    {

                        Console.WriteLine("Press 1 to hit or 2 to stay.");
                        playerResponse = Console.ReadLine();
                    }

                    if (playerResponse == "1")
                    {
                        game.Draw(playerTurn);
                        game.DealerTotalCalc();
                        game.PlayerTotalCalc();
                    }
                    else if (playerResponse == "2")
                    {
                        playerTurn = false;
                    }
                    else
                    {
                        Console.Clear();
                        game.DealerTotalCalc();
                        game.PlayerTotalCalc();
                    }
                    //If the player's hand ever reaches 21, skips to the end
                    //Similarly if it ever goes over 21, skips to the end
                    if (game.playerTotal == 21)
                    {
                        playerTurn = false;
                        blackjack = true;
                    }
                    if (game.playerTotal > 21)
                    {
                        playerTurn = false;
                        bust = true;
                    }

                } while (playerTurn);

                game.DealerTurn();
                game.Dealer();
                //dealer only actually plays if his total isn't above 16, and the player didn't bust or get 21
                while (game.dealerTotal <= 16 && !bust && !blackjack && game.dealerTotal < game.playerTotal)
                {
                    game.Draw(playerTurn);
                    game.DealerTotalCalc();
                    game.PlayerTotalCalc();
                    //pauses the app to give the user time to comprehend whats going on between card draws
                    System.Threading.Thread.Sleep(2000);
                }
                Console.Clear();
                //One final update to the player and dealer totals
                game.DealerTotalCalc();
                game.PlayerTotalCalc();
                if (game.playerTotal > 21)
                {
                    Console.WriteLine($"Bust, House wins. -${bet.ToString("F")}");
                    betting.Lose(bet);
                    audio.LoseSound();
                }
                else if (game.playerTotal == 21)
                {
                    Console.WriteLine($"Blackjack, you win! 1.5X payout! +${(bet * 1.5m).ToString("F")}");
                    betting.Blackjack(bet);
                    audio.WinSound();
                }
                else if (game.dealerTotal > 21)
                {
                    Console.WriteLine($"Dealer bust, you win! +${bet.ToString("F")}");
                    betting.Win(bet);
                    audio.WinSound();
                }
                else if (game.playerTotal == game.dealerTotal)
                {
                    Console.WriteLine("Draw. Bet returned.");
                }
                else
                {
                    if (game.playerTotal > game.dealerTotal)
                    {
                        Console.WriteLine($"You win! +${bet.ToString("F")}");
                        betting.Win(bet);
                        audio.WinSound();
                    }
                    else
                    {
                        Console.WriteLine($"You lose! -${bet.ToString("F")}");
                        betting.Lose(bet);
                        audio.LoseSound();
                    }
                }
                //Prompts the user if they still have money
                if (betting.wallet > 0)
                {
                    Console.WriteLine($"New total: ${betting.wallet.ToString("F")}");
                    Console.WriteLine("Press 1 to continue, 2 to restart, or 3 to exit");
                    playerPlayGame = Console.ReadLine();
                    if (playerPlayGame == "2")
                    {
                        //This resets their wallet to $100 and shuffles the deck. Essentially restarting the game
                        betting.wallet = 100;
                        game.ShuffleCheck(true);

                    }
                }
                else //prompts the user if they have ran out of money
                {
                    Console.WriteLine("You're out of funds");
                    Console.WriteLine("Press 1 to restart or 3 to exit");
                    playerPlayGame = Console.ReadLine();
                    if (playerPlayGame == "1")
                    {
                        betting.wallet = 100;
                        game.ShuffleCheck(true);
                    }
                }
            }
            while (gameGoing) ;
        }
    }
}
