//File Name: Player.cs
//Author: Nathaniel Fisher
//Purpose: The Player class the will inherit from the IPlayer interface and will be able to draw a top card from the deck
//         and is capapble of determineing if a given set of cards is a playable word using the word application object
//DATE:     Feb 7, 2020

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuiddlerLibrary
{
    public class Player : IPlayer
    {
        //properties

        //private
        private List<Card> playerHand;
        private List<Card> gameDeck;
        private Card currentDiscard;
        private counter playerCounterTracker;
        private Microsoft.Office.Interop.Word.Application appInstance;
        private int playerScore;

        //public properties
        public int CardCount { get { return playerHand.Count; } }

        public int TotalPoints { get { return playerScore; } }

        public Player(List<Card> Deck, Card deckDiscard, Microsoft.Office.Interop.Word.Application application, counter playerCounter)
        {
            gameDeck = Deck;
            appInstance = application;
            currentDiscard = deckDiscard;
            playerCounterTracker = playerCounter;
            playerScore = 0;
            playerHand = new List<Card>();
        }

        //public methods
        //dispose method that will check to see if this is the last instance of a player
        //if it is then it will then call the quit method for the word application
        public void Dispose()
        {
            if(playerCounterTracker.playerCount == 1)
            {
                appInstance.Quit();
            }
            else
            {
                playerCounterTracker.playerCount--;
            }
            
        }

        public string DrawTopCard()
        {
            Card topCard = gameDeck[0];
            playerHand.Add(topCard);
            gameDeck.RemoveAt(0);
            return topCard.CardValue;

        }


        /// <summary>
        /// will take the user submitted letter and determine if the card exists in the player's hand and will place that card onto the discard "pile" that the deck holds
        /// </summary>
        /// <param name="letter"></param>
        /// <returns> a boolean to determine if the card was dropped</returns>
        public bool DropDiscard(string letter)
        {
            bool cardExist = false;
            if(playerHand.Exists(x => x.CardValue.Equals(letter)) == true)
            {
                cardExist = true;
                
                for(int i = 0; i < playerHand.Count; i++)
                {
                    if(playerHand[i].CardValue.Equals(letter))
                    {
                        currentDiscard.CardValue = playerHand[i].CardValue;
                        currentDiscard.CardPointValue = playerHand[i].CardPointValue;
                        playerHand.RemoveAt(i);
                        break;
                    }
                }
            }
            //if no letter is given in to the function then it will default and remove the first card in the hand
            else if (letter.Equals(""))
            {
                cardExist = true;
                currentDiscard.CardValue = playerHand[0].CardValue;
                currentDiscard.CardPointValue = playerHand[0].CardPointValue;
                playerHand.RemoveAt(0);

            }

            return cardExist;
        }
        /// <summary>
        /// picks up the discarded card in the deck
        /// </summary>
        /// <returns>the letter value of the card in string format</returns>
        public string PickupDiscard()
        {
            playerHand.Add(currentDiscard);
            return currentDiscard.CardValue;
        }
        /// <summary>
        /// will play the the player has submitted and then will add the score of the word to the player's total score
        /// then will remove the cards that was needed to make the word from the hand
        /// </summary>
        /// <param name="candidate"></param>
        /// <returns>the score of the played word</returns>
        public int PlayWord(string candidate)
        {
            int score = TestWord(candidate);
            if(score == 0)
            {
                return score;
            }
            else
            {
                string[] cardsVal = candidate.Split(' ');
                foreach (string card in cardsVal)
                {
                    for (int i = 0; i < playerHand.Count; i++)
                    {
                        if (playerHand[i].CardValue.Equals(card))
                        {
                            playerHand.RemoveAt(i);
                            break;
                        }
                    }

                }
                playerScore += score;
                return score;
            }
        }
        /// <summary>
        /// tests to see if the word the user has submitted can be formed fro the player's hand currrently
        /// and will also determine with the help of the test word method if the word is an actual word
        /// </summary>
        /// <param name="candidate"></param>
        /// <returns></returns>
        public int TestWord(string candidate)
        {
            string[] cardsVal = candidate.Split(' ');
            string wordCandidate = "";
            int score = 0;
            bool cardExists;

            if(cardsVal.Length == playerHand.Count)
            {
                Console.WriteLine("Error: you must be able to discard one card!");
                Console.WriteLine("Please ensure you have one card leftover");
                return 0;
            }
            //checks to see if the words exist in the player hand but will also tally the score
            //if one card is not found in the hand it will trigger the cardExists boolean and will return an automatic zero
            foreach(string card in cardsVal)
            {
                cardExists = false;
                wordCandidate += card;

                for (int i = 0; i < playerHand.Count; i++)
                {
                    if (playerHand[i].CardValue.Equals(card))
                    {
                        score += playerHand[i].CardPointValue;
                        cardExists = true;
                        break;
                    }
                }
                //if the card was not found in the hand it will automatically exit out and return 0
                if(cardExists == false)
                {
                    return 0;
                }

            }
            //checks to see if the word candidate is and actual word
            if(appInstance.CheckSpelling(wordCandidate) == true)
            {
                return score;
            }
            else
            {
                return 0;
            }            
        }

        public override string ToString()
        {
            string displayHand = "[";

            foreach(Card card in playerHand)
            {
                displayHand += " " + card.CardValue;
            }

            displayHand += " ]";

            return displayHand;

        }
    }

    
}
