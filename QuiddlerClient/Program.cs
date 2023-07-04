//Program: NFisher_Project1
//Author: Nathaniel Fisher
//Date: Feb 7, 2020
//Purpose: A C# client that will demonstrate the QuiddlerLibrary byt playing a simpled version of Quiddler

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QuiddlerLibrary;
//new line
//one more
namespace QuiddlerClient
{
    class Program
    {
        static void Main(string[] args)
        {
            int numPlayers = 0;
            int numCards = 0;
            string userInput;

            IDeck gameDeck = new Deck();

            //printing out information of the game
            Console.WriteLine(gameDeck.About + "\n");
            Console.WriteLine("The deck is initialized with " + (gameDeck.CardCount+1) + " cards.\n");
            //verification loop to ensure the user enters the correct number
            do
            {
                Console.Write("Hot many players are there? (1-8): ");
                userInput = Console.ReadLine();
                int.TryParse(userInput, out numPlayers);
                if(numPlayers == 0 || numPlayers > 8)
                {
                    Console.WriteLine("Error please enter a number between 1 and 8");
                }

            } while (numPlayers == 0 || numPlayers > 8);
            

            //verification loop to ensure the user enters the correct number
            do
            {
                Console.Write("How many cards will be dealt to each player?? (3-10): ");
                userInput = Console.ReadLine();
                int.TryParse(userInput, out numCards);
                if (numCards < 3 || numCards > 10)
                {
                    Console.WriteLine("Error please enter a number between 3 and 10");
                }

            } while (numCards < 3 || numCards > 10);
            // now giving number of cards to the deck
            gameDeck.CardsPerPlayer = numCards;

            //creating the players
            List<IPlayer> players = new List<IPlayer>();
            for (int i = 0; i <numPlayers; i++)
            {
                players.Add(gameDeck.NewPlayer());
            }



            // variables that are to be used in the mast do-while loop to ensure that the game is finished
            bool gameFinished = false;
            //determines the number of turns that have passed
            int numTurns = 1;
            //determines the max number of turns until the gameFinished bool gets switched
            int maxTurns = 8;

            do
            {
                int playerNum = 1;
                char userChoice;
                foreach (IPlayer currentPlayer in players)
                {
                    Console.WriteLine("----------------------------------------------------------------");
                    Console.WriteLine("Player " + playerNum);
                    Console.WriteLine("----------------------------------------------------------------");
                    Console.WriteLine("Your cards are " + currentPlayer);
                    bool validChoice = false;
                    do
                    {
                        Console.Write("Do you want the top card in the discard pile which is '" + gameDeck.Discard + "'? (y/n) ");
                        ConsoleKeyInfo userkey = Console.ReadKey();
                        userChoice = userkey.KeyChar;
                        if(userChoice == 'y')
                        {
                            validChoice = true;
                            currentPlayer.PickupDiscard();
                        }
                        else if(userChoice == 'n')
                        {
                            validChoice = true;
                            currentPlayer.DrawTopCard();
                        }
                    } while (validChoice == false);
                    Console.WriteLine("\nYour cards are " + currentPlayer);

                    validChoice = false;
                    do
                    {
                        int score = 0;
                        string userWordSub;
                        Console.Write("Test a word for it's point value? (y/n) ");
                        userChoice = Console.ReadKey().KeyChar;


                        if (userChoice == 'y')
                        {
                            Console.Write("\nEnter a word using " + currentPlayer + " leaving a space between cards: ");
                            userWordSub = Console.ReadLine();
                            //testing to see if the word will have a point value
                            score = currentPlayer.TestWord(userWordSub);
                            Console.WriteLine("The word [" + userWordSub + "] is worth " + score);
                            //if there is a ppoint value greater than 0 will then ask if the user wishes to play the word
                            if (score != 0)
                            {
                                
                                Console.Write("Do you want to play the word [" + userWordSub + "]? (y/n) ");
                                userChoice = Console.ReadKey().KeyChar;
                                if(userChoice == 'y')
                                {
                                    currentPlayer.PlayWord(userWordSub);
                                    Console.WriteLine("\nYour cards are " + currentPlayer + " and you have " + currentPlayer.TotalPoints + " points.");
                                    if(currentPlayer.CardCount == 1)
                                    {
                                        currentPlayer.DropDiscard("");
                                    }
                                    else
                                    {
                                        bool validDiscard = false;
                                        do
                                        {
                                            Console.Write("Enter a card from your hand to drop to the discard pile ");
                                            string userDiscard = Console.ReadLine();
                                            validDiscard = currentPlayer.DropDiscard(userDiscard);
                                            if(validDiscard == false)
                                            {
                                                Console.WriteLine("Error: please enter a valid card!");
                                            }
                                        } while (validDiscard == false);
                                        
                                    }

                                    validChoice = true;
                                }

                            }
                            
                        }
                        //will be triggered and will ask the user if they wish to skip their turn
                        //TODO:update program
                        else if (userChoice == 'n')
                        {
                            Console.Write("\nAre you sure you want to skip your turn? (y/n) ");
                            userChoice = Console.ReadKey().KeyChar;
                            //commented out the discard when skipping a turn as the player will not be able to gather more cards to play a new word
                            if (userChoice == 'y')
                            {
                                //having user discard a card
                                //bool validDiscard = false;
                                //do
                                //{
                                //    Console.Write("\nEnter a card from your hand to drop to the discard pile ");
                                //    string userDiscard = Console.ReadLine();
                                //    validDiscard = currentPlayer.DropDiscard(userDiscard);
                                //    if (validDiscard == false)
                                //    {
                                //        Console.WriteLine("Error: please enter a valid card!");
                                //    }
                                //} while (validDiscard == false);
                                validChoice = true;
                            }
                            Console.WriteLine();
                            
                        }
                        else
                        {
                            Console.WriteLine("\nError please enter a valid choice");
                        }
                    } while (validChoice == false);

                    if(currentPlayer.CardCount != 0)
                    {
                        Console.WriteLine("Your cards are " + currentPlayer);
                    }
                    else
                    {
                        Console.WriteLine("***** Player " + playerNum + "is out! Game Over!!*****");
                        gameFinished = true;
                        break;
                    }


                    playerNum++;

                }
                //will keep track of the number of turns when it reaches 8 turns it will then trigger that the game is over
                if(numTurns == maxTurns)
                {
                    gameFinished = true;
                }
                else
                {
                    numTurns++;
                }

            } while (gameFinished == false);

            int playerNumber = 1;
            int topPlayerNumber = playerNumber;
            int currentPlayerScore = 0;
            //determining the winner of the game and also disposing of the players to ensure that the word application is properly disposed of
            foreach(IPlayer player in players)
            {
                if(currentPlayerScore < player.TotalPoints)
                {
                    topPlayerNumber = playerNumber;
                    currentPlayerScore = player.TotalPoints;
                }
                player.Dispose();
            }

            Console.WriteLine("Player " + topPlayerNumber + " won with a score of " + currentPlayerScore + " points.");

        }
    }
}
