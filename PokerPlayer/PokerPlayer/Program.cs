using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerPlayer
{
    class Program
    {
        static void Main(string[] args)
        {
            Deck myDeck = new Deck();
            myDeck.Shuffle();
            PokerPlayer player = new PokerPlayer();           

            player.DrawHand(myDeck.Deal(5));
                            
            player.ShowHand();

            Console.ReadKey();
        }
    }
    class PokerPlayer
    {
        List<Card> CurrentHand = new List<Card>();

        // Enum of different hand types
        public enum HandType
        {
            HighCard,
            OnePair,
            TwoPair,
            ThreeOfAKind,
            Straight,
            Flush,
            FullHouse,
            FourOfAKind,
            StraightFlush,
            RoyalFlush
        }
        // Rank of hand that player holds
        public HandType HandRank 
        {
            get
            {
                if (this.HasRoyalFlush())
                    return HandType.RoyalFlush;
                else if (this.HasStraightFlush())
                    return HandType.StraightFlush;
                else if (this.HasFourOfAKind())
                    return HandType.FourOfAKind;
                else if (this.HasFullHouse())
                    return HandType.FullHouse;
                else if (this.HasFlush())
                    return HandType.Flush;                
                else if (this.HasStraight())
                    return HandType.Straight;
                else if (this.HasThreeOfAKind())
                    return HandType.ThreeOfAKind;
                else if (this.HasTwoPair())
                    return HandType.TwoPair;
                else if (this.HasPair())
                    return HandType.OnePair;

                else return HandType.HighCard;
            }                
        }

        // Constructor that isn't used
        public PokerPlayer(){}

        //set CurrentHand
        public void DrawHand(List<Card> myCards)
        {
            CurrentHand = myCards;
        }

        public void ShowHand()
        {
            //Unicode characters for suits
            String heart = "\u2665";
            String diamond = "\u2666";
            String spade = "\u2660";
            String club = "\u2663";

            //set the background color for the console
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Clear();

            //set the color and the suit symbol for each card
            foreach (Card item in CurrentHand)
            {
                switch (item.Suit)
                {
                    case Suit.Heart: 
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(item.Rank + heart + " ");
                        break;
                    case Suit.Diamond:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(item.Rank + diamond+" ");
                        break;
                    case Suit.Spade:
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(item.Rank + spade+" ");
                        break;
                    case Suit.Club:
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(item.Rank +  club+" ");
                        break;
                }                             
            }
            //Print out the rank
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\n\nYou have: "+this.HandRank);
            
        }

        public bool HasPair()
        {
            //once I group by rank I check if there is only one group with a pair
            return this.CurrentHand.GroupBy(x => x.Rank).Where(x => x.Count() == 2).Count() == 1;
        }
        public bool HasTwoPair()
        {
            return this.CurrentHand.GroupBy(x => x.Rank).Where(x => x.Count() == 2).Count() == 2;
        }
        public bool HasThreeOfAKind()
        {
            return this.CurrentHand.GroupBy(x => x.Rank).Where(x => x.Count() == 3).Count() == 1;
        }
        public bool HasStraight()
        {
            //sort ascending
            SortCards();

            //case A,2,3,4,5
            if(CurrentHand[CurrentHand.Count-1].Rank == Rank.Ace && CurrentHand[0].Rank == Rank.Two)
            {
                for (int i = 1; i < 3; i++)
                {
                    if (CurrentHand[i].Rank != CurrentHand[i + 1].Rank - 1)
                        return false;
                }
            }           
            else
            {
                //all other cases
                for (int i = 0; i < CurrentHand.Count-1; i++)
                {
                    if (CurrentHand[i].Rank != CurrentHand[i + 1].Rank - 1)
                        return false;
                }
            }
            return true;
        }
        public bool HasFlush()
        {
            //Check if there is a group with 5 suits
            return this.CurrentHand.GroupBy(x => x.Suit).Where(x => x.Count() == 5).Count() == 1;
        }
        public bool HasFullHouse()
        {
            return HasPair() && HasThreeOfAKind();
        }
        public bool HasFourOfAKind()
        {
            bool fourOfAKind = this.CurrentHand.GroupBy(x => x.Rank).Where(x => x.Count() == 4).Count() == 1;
            bool allDistinctSuits = this.CurrentHand.Select(x => x.Suit).Distinct().Count() == 4;

            return fourOfAKind && allDistinctSuits;          
        }
        public bool HasStraightFlush()
        {
            return HasFlush() && HasStraight();
        }
        public bool HasRoyalFlush()
        {
            SortCards();
            if (HasStraightFlush() && ((CurrentHand[0].Rank == Rank.Ten) && (CurrentHand[4].Rank == Rank.Ace)))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Order CurrentHands cards by rank
        /// </summary>
        public void SortCards()
        {
            CurrentHand = CurrentHand.OrderBy(x => x.Rank).ToList();
        }

    }
  

    //  *****Deck Class Start*****
    class Deck
    {
        public List<Card> DeckOfCards;
        public List<Card> DiscardedCards;
        public int CardsRemaining { get; set; }

        public Deck()
        {
            DeckOfCards = new List<Card>();
            DiscardedCards = new List<Card>();

            AddCardsToDeck();
            CardsRemaining = DeckOfCards.Count();
        }

        /// <summary>
        /// Overload constructor
        /// </summary>
        /// <param name="numberOfDeck"></param>
        public Deck(int numberOfDeck)
            : this() //call the base constructor
        {
            for (int i = 0; i < numberOfDeck - 1; i++)
            {
                AddCardsToDeck();
            }
            CardsRemaining = DeckOfCards.Count();
        }

        /// <summary>
        ///  Merges the discarded pile with the deck and shuffles the cards
        /// </summary>
        public void Shuffle()
        {
            List<Card> shuffled = new List<Card>();
            int randomCard;

            //merge together the the decks of cards  P.S.The function Concat() returns a new list.
            DeckOfCards = (List<Card>)DeckOfCards.Concat(DiscardedCards).ToList();
            CardsRemaining = DeckOfCards.Count;

            Random gnr = new Random();
            for (int i = 0; i < CardsRemaining; i++)
            {
                randomCard = gnr.Next(0, DeckOfCards.Count);

                //add the random card to the shuffled deck
                shuffled.Add(DeckOfCards[randomCard]);

                //remove from the original deck the card
                DeckOfCards.RemoveAt(randomCard);
            }

            DeckOfCards = shuffled;
        }

        /// <summary>
        /// Returns a number of cards from the top of the deck
        /// </summary>
        /// <param name="numberOfCards">number of card</param>
        /// <returns>List of cards</returns>
        public List<Card> Deal(int numberOfCards)
        {
            List<Card> myCards = new List<Card>();

            //if there are enough cards from the deck
            if (CardsRemaining > numberOfCards)
            {
                //save cards into another list
                myCards = DeckOfCards.Take(numberOfCards).ToList();

                //remove cards from the deck
                DeckOfCards.RemoveRange(0, numberOfCards);

                CardsRemaining = DeckOfCards.Count();
            }
            else
            {
                //just take the remaining cards
                myCards = DeckOfCards.Take(CardsRemaining).ToList();

                //remove cards from the deck
                DeckOfCards.RemoveRange(0, CardsRemaining);

                CardsRemaining = DeckOfCards.Count();
            }

            return myCards;
        }

        /// <summary>
        /// Returns a card from a player to the discard pile
        /// </summary>
        /// <param name="card"></param>
        public void Discard(Card card)
        {
            DiscardedCards.Add(card);
        }

        public void Discard(List<Card> cards)
        {
            DiscardedCards = (List<Card>)DiscardedCards.Concat(cards).ToList();
        }

        /// <summary>
        /// Adds all the Suits and Ranks to the deck
        /// </summary>
        public void AddCardsToDeck()
        {
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    DeckOfCards.Add(new Card((int)rank, (int)suit));
                }
            }
        }
    }    //  *****Deck Class End*******


    //  *****Card Class Start*****
    class Card
    {
        public Suit Suit { get; set; }
        public Rank Rank { get; set; }

        public Card(int rank, int suit)
        {
            this.Suit = (Suit)suit;
            this.Rank = (Rank)rank;
        }
    }  //  *****Card Class End*******

    public enum Suit
    {
        Heart,
        Diamond,
        Club,
        Spade
    }

    public enum Rank
    {
        Two = 2, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace
    }
}
