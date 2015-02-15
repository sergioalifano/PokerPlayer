﻿using System;
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
            PokerPlayer player = new PokerPlayer();

            Card one = new Card(2, 1);
            Card two = new Card(3, 1);
            Card three = new Card(10, 1);
            Card four = new Card(5, 1);
            Card five = new Card(6, 1);

            List<Card> myCards= new List<Card>() { one,two,three,four,five };

            //give the player the cards
            player.DrawHand(myCards);
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
            OnePair=15,
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
                if (this.HasPair())
                    return HandType.OnePair;
                else if (this.HasTwoPair())
                    return HandType.TwoPair;
                else if (this.HasThreeOfAKind())
                    return HandType.ThreeOfAKind;
                else if (this.HasStraight())
                    return HandType.Straight;
                else if (this.HasFlush())
                    return HandType.Flush;
                else if (this.HasFullHouse())
                    return HandType.FullHouse;
                else if (this.HasFourOfAKind())
                    return HandType.FourOfAKind;
                else if (this.HasStraightFlush())
                    return HandType.StraightFlush;
                else if (this.HasRoyalFlush())
                    return HandType.RoyalFlush;

                else return HandType.HighCard;
            }
    
            
        }

        // Constructor that isn't used
        public PokerPlayer(){}

        //set the CurrentHand with 5 cards
        public void DrawHand(List<Card> myCards)
        {
            CurrentHand = myCards;
        }

        public void ShowHand()
        {
            //bool gotNothing=!(this.HasPair()||this.HasThreeOfAKind()||this.HasStraight()||this.HasFlush()||this.HasFullHouse()||this.HasFourOfAKind()||this.HasStraight()||this.HasRoyalFlush());

            //if (gotNothing)
            //{
            //    //this function sort the CurrentHand list by rank
            //    SortCards();

            //   // this.HandRank = CurrentHand[CurrentHand.Count - 1].Rank;
            //    //Take the last card of the list and show it
            //    Console.WriteLine();
            //}
            foreach (Card item in CurrentHand)
            {
                Console.Write(item.Rank+""+item.Suit+" ");               
            }
            Console.WriteLine();
            Console.WriteLine(this.HandRank);
            
        }

        public bool HasPair()
        {

            SortCards();
            bool flag1 = false;

            //check if has a better rank
            if (HasThreeOfAKind() || HasFourOfAKind() || HasTwoPair() || HasFullHouse())
            {
                return false;                
            }

            //otherwise check for pair
            for (int i = 0; i < CurrentHand.Count-1; i++)
            {
                flag1 = flag1 || (CurrentHand[i].Rank == CurrentHand[i + 1].Rank);
            }
        
            return flag1 ;
        }
        public bool HasTwoPair()
        {
            SortCards();
            bool flag1 = false, flag2 = false, flag3=false;

            if (HasThreeOfAKind() || HasFourOfAKind() || HasFullHouse())
            {
                return false;               
            }

            flag1 = CurrentHand[0].Rank == CurrentHand[1].Rank && CurrentHand[2].Rank == CurrentHand[3].Rank;
            flag2 = CurrentHand[1].Rank == CurrentHand[2].Rank && CurrentHand[3].Rank == CurrentHand[4].Rank;
            flag2 = CurrentHand[0].Rank == CurrentHand[1].Rank && CurrentHand[3].Rank == CurrentHand[4].Rank;
            return flag1 || flag2 || flag3;
        }
        public bool HasThreeOfAKind()
        {
            SortCards();
            bool flag1=false, flag2=false, flag3=false;

            //if it's not a FourOfAKind or a Full House then at least 2 cards must be different from the rest
            if (HasFourOfAKind() || HasFullHouse())
            {
                return false;
                
            }

            //check the first 3 cards
            flag1 = CurrentHand[0].Rank == CurrentHand[1].Rank &&
                CurrentHand[1].Rank == CurrentHand[2].Rank;

            //check the last 3 cards
            flag2 = CurrentHand[2].Rank == CurrentHand[3].Rank &&
                CurrentHand[3].Rank == CurrentHand[4].Rank;

            //check the 3 cards in between
            flag3 = CurrentHand[1].Rank == CurrentHand[2].Rank &&
                CurrentHand[2].Rank == CurrentHand[3].Rank;

            return flag1 || flag2 || flag3;
        }
        public bool HasStraight()
        {
            SortCards();
            bool flag1 = true;

            //case 10,J,Q,K,A
            if(CurrentHand[0].Rank == Rank.Ace && CurrentHand[4].Rank ==Rank.King)
            {
                for (int i = 1; i < 3; i++)
                {
                    flag1 = flag1 && (CurrentHand[i].Rank == CurrentHand[i + 1].Rank-1);
                }
            }
           
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    flag1 = flag1 && (CurrentHand[i].Rank == CurrentHand[i + 1].Rank-1);
                }
            }
            return flag1;
        }
        public bool HasFlush()
        {  
          return CurrentHand.GroupBy(x => x.Suit).Count() == 5;
        }
        public bool HasFullHouse()
        {
            bool flag1, flag2;

            flag1 = CurrentHand[0].Rank == CurrentHand[1].Rank && CurrentHand[1].Rank == CurrentHand[2].Rank && CurrentHand[3].Rank == CurrentHand[4].Rank;
            flag2 = CurrentHand[0].Rank == CurrentHand[1].Rank && CurrentHand[2].Rank == CurrentHand[3].Rank && CurrentHand[3].Rank == CurrentHand[4].Rank;

            return flag1 || flag2;
        }
        public bool HasFourOfAKind()
        {
            bool flag1, flag2;
            //sort cards
            SortCards();

            //flag1 is True if only the last card is different from the others
            flag1 = CurrentHand[0].Rank == CurrentHand[1].Rank &&
                CurrentHand[1].Rank == CurrentHand[2].Rank &&
                CurrentHand[2].Rank == CurrentHand[3].Rank;

            //flag2 is True if only the first card is different from the others
            flag2 = CurrentHand[1].Rank == CurrentHand[2].Rank &&
                CurrentHand[2].Rank == CurrentHand[3].Rank &&
                CurrentHand[3].Rank == CurrentHand[4].Rank;

            return flag1 || flag2;
        }
        public bool HasStraightFlush()
        {
            SortCards();
            return HasFlush() && HasStraight();
        }
        public bool HasRoyalFlush()
        {
            SortCards();
            if (HasStraightFlush() && ((CurrentHand[0].Rank == Rank.Ace) && (CurrentHand[4].Rank == Rank.King)))
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

            Random gnr = new Random();
            for (int i = 0; i < DeckOfCards.Count; i++)
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
                    DeckOfCards.Add(new Card((int)suit, (int)rank));
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
