//File Name: Deck.cs
//Author: Nathaniel Fisher
//Purpose: The Deck class that will inherit from the IDeck class and will hold a deck of 118 cards and be able to give cards to players
//         will also hold the instance of the word Application and pass references to the player class so only one copy of the application is ever created
//          will also hold a player counter class that only has a single int that will be passed to the players to help determine if the last player being disposed is the last player
//          and will call the quit method for the word object
//DATE:     Feb 7, 2020

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuiddlerLibrary
{

    public class Deck : IDeck
    {
        //properties
        //private properties the client won't have to deal with;
        private List<Card> playingDeck;
        private Card discard;
        private counter numPlayers = new counter();
        //commented out singleton idea if I can try to get this to work with the dispose method
        private Microsoft.Office.Interop.Word.Application Application;

        //public properties
        public string About {
            get { return "Quiddler (TM) Library, by Nathaniel Fisher"; }
        }

        public int CardCount
        {
            get { return playingDeck.Count(); }
        }

        public int CardsPerPlayer 
        {
            get;set;
        }

        public string Discard { get { return discard.CardValue; } }

        

        //C'Tor
        public Deck()
        {
            playingDeck = new List<Card>();
            CardsPerPlayer = 0;
            BuildPlayingDeck();
            Shuffle();
            discard = playingDeck[0];
            playingDeck.RemoveAt(0);
            
        }

        //Public Methods
        public IPlayer NewPlayer()
        {
            numPlayers.playerCount++;
            IPlayer test = new Player(playingDeck, discard, getApplication(), numPlayers);

            for(int i = 0; i < CardsPerPlayer; i++)
            {
                test.DrawTopCard();
            }
            return test;
        }

        //Private helper methods

        //helper method that will build a list of cards and then shuffle the cards
        private void BuildPlayingDeck()
        {
            Dictionary<string, int> DeckValues = GetCardReference();

            foreach(KeyValuePair<string, int> deckCardVal in DeckValues)
            {
                int numCards = deckCardVal.Value;
                for(int i = 0; i<numCards; i++)
                {
                    Card currentCard = new Card(deckCardVal.Key);
                    playingDeck.Add(currentCard);
                }

            }
        }

        //will shuffle the deck
        private void Shuffle()
        {
            int listCount = playingDeck.Count;
            Random ranNum = new Random();
            while (listCount > 1)
            {
                listCount--;
                int ranIndex = ranNum.Next(listCount + 1);
                Card shuffledCard = playingDeck[ranIndex];
                playingDeck[ranIndex] = playingDeck[listCount];
                playingDeck[listCount] = shuffledCard;

            }

        }

        //private helper method that would ensure that the application clss would act as a singletong so only one instance of word
        //is used 
        //TODO:try and figure out how to get the singleton to work if I can with the dispose method
        private Microsoft.Office.Interop.Word.Application getApplication()
        {
            if (Application == null)
            {
                Application = new Microsoft.Office.Interop.Word.Application();
            }
            return Application;
        }

        //card reference that will be used by the build deck method
        //has the card letter and the number of said card in a quibbler deck;
        private Dictionary<string, int> GetCardReference()
        {
            Dictionary<string, int> test = new Dictionary<string, int>()
            {
                {"b", 2 },
                {"c", 2 },
                {"f", 2 },
                {"h", 2 },
                {"j", 2 },
                {"k", 2 },
                {"m", 2 },
                {"p", 2 },
                {"q", 2 },
                {"v", 2 },
                {"w", 2 },
                {"x", 2 },
                {"z", 2 },
                {"cl", 2 },
                {"er", 2 },
                {"in", 2 },
                {"qu", 2 },
                {"th", 2 },
                {"d", 4 },
                {"g", 4 },
                {"l", 4 },
                {"s", 4 },
                {"y", 4 },
                {"n", 6 },
                {"r", 6 },
                {"t", 6 },
                {"u", 6 },
                {"i", 8 },
                {"o", 8 },
                {"a", 10 },
                {"e", 12 }
            };

            return test;
        }
    }

    //counter class that will be used during the dispose method;
    public class counter
    {
        public int playerCount;

        public counter()
        {
            playerCount = 0;
        }
    }
}
