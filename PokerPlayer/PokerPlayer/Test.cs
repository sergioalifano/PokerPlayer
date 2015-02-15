using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PokerPlayer
{
    [TestFixture]
    class Test
    {
        PokerPlayer player;
        List<Card> testHand;
        [SetUp]
        public void testSetup()
        {
            player = new PokerPlayer();
            testHand = new List<Card>();
        }
        [Test]
        public void testHighCard()
        {
            testHand.Add(new Card((int)Rank.Ten, (int)Suit.Club));
            testHand.Add(new Card((int)Rank.Five, (int)Suit.Diamond));
            testHand.Add(new Card((int)Rank.Three, (int)Suit.Club));
            testHand.Add(new Card((int)Rank.Four, (int)Suit.Diamond));
            testHand.Add(new Card((int)Rank.Seven, (int)Suit.Club));
            player.DrawHand(testHand);
            Assert.That(player.HandRank == PokerPlayer.HandType.HighCard);
            Assert.IsFalse(player.HasPair());
            Assert.IsFalse(player.HasFlush());
            Assert.IsFalse(player.HasFourOfAKind());
            Assert.IsFalse(player.HasFullHouse());
            Assert.IsFalse(player.HasRoyalFlush());
            Assert.IsFalse(player.HasStraight());
            Assert.IsFalse(player.HasStraightFlush());
            Assert.IsFalse(player.HasThreeOfAKind());
            Assert.IsFalse(player.HasTwoPair());
        }
        [Test]
        public void testHasPair()
        {
            testHand.Add(new Card((int)Rank.Ten, (int)Suit.Club));
            testHand.Add(new Card((int)Rank.Ten, (int)Suit.Diamond));
            testHand.Add(new Card((int)Rank.Eight, (int)Suit.Club));
            testHand.Add(new Card((int)Rank.Seven, (int)Suit.Heart));
            player.DrawHand(testHand);
            Assert.That(player.HasPair());
            Assert.That(player.HandRank == PokerPlayer.HandType.OnePair);
            Assert.False(player.HasThreeOfAKind());
        }
        [Test]
        public void testHasTwoPair()
        {
            testHand.Add(new Card((int)Rank.Ten, (int)Suit.Club));
            testHand.Add(new Card((int)Rank.Ten, (int)Suit.Diamond));
            testHand.Add(new Card((int)Rank.Eight, (int)Suit.Club));
            testHand.Add(new Card((int)Rank.Eight, (int)Suit.Diamond));
            player.DrawHand(testHand);
            Assert.That(player.HasTwoPair());
            Assert.That(player.HandRank == PokerPlayer.HandType.TwoPair);
            Assert.IsFalse(player.HasThreeOfAKind());
        }
        [Test]
        public void testHasThreeOfAKind()
        {
            testHand.Add(new Card((int)Rank.Ten, (int)Suit.Club));
            testHand.Add(new Card((int)Rank.Ten, (int)Suit.Diamond));
            testHand.Add(new Card((int)Rank.Ten, (int)Suit.Heart));
            testHand.Add(new Card((int)Rank.Eight, (int)Suit.Club));
            testHand.Add(new Card((int)Rank.Two, (int)Suit.Diamond));
            player.DrawHand(testHand);
            Assert.That(player.HasThreeOfAKind());
            Assert.That(player.HandRank == PokerPlayer.HandType.ThreeOfAKind);
        }
        [Test]
        public void testHasStraight()
        {
            testHand.Add(new Card((int)Rank.Two, (int)Suit.Club));
            testHand.Add(new Card((int)Rank.Three, (int)Suit.Diamond));
            testHand.Add(new Card((int)Rank.Five, (int)Suit.Heart));
            testHand.Add(new Card((int)Rank.Four, (int)Suit.Club));
            testHand.Add(new Card((int)Rank.Six, (int)Suit.Diamond));
            player.DrawHand(testHand);
            Assert.That(player.HasStraight());
            Assert.That(player.HandRank == PokerPlayer.HandType.Straight);
            Assert.IsFalse(player.HasPair());
            testHand.Clear();
            testHand.Add(new Card((int)Rank.Two, (int)Suit.Club));
            testHand.Add(new Card((int)Rank.Three, (int)Suit.Diamond));
            testHand.Add(new Card((int)Rank.King, (int)Suit.Heart));
            testHand.Add(new Card((int)Rank.Four, (int)Suit.Club));
            testHand.Add(new Card((int)Rank.Ace, (int)Suit.Diamond));
            player.DrawHand(testHand);
            Assert.IsFalse(player.HasStraight());
            Assert.That(player.HandRank != PokerPlayer.HandType.Straight);
            testHand.Clear();
            testHand.Add(new Card((int)Rank.Queen, (int)Suit.Club));
            testHand.Add(new Card((int)Rank.Jack, (int)Suit.Diamond));
            testHand.Add(new Card((int)Rank.King, (int)Suit.Heart));
            testHand.Add(new Card((int)Rank.Ace, (int)Suit.Club));
            testHand.Add(new Card((int)Rank.Ten, (int)Suit.Diamond));
            player.DrawHand(testHand);
            Assert.That(player.HasStraight());
            Assert.That(player.HandRank == PokerPlayer.HandType.Straight);
        }
        [Test]
        public void testHasFlush()
        {
            testHand.Add(new Card((int)Rank.Queen, (int)Suit.Club));
            testHand.Add(new Card((int)Rank.Jack, (int)Suit.Club));
            testHand.Add(new Card((int)Rank.Four, (int)Suit.Club));
            testHand.Add(new Card((int)Rank.Ace, (int)Suit.Club));
            testHand.Add(new Card((int)Rank.Ten, (int)Suit.Club));
            player.DrawHand(testHand);
            Assert.That(player.HasFlush());
            Assert.That(player.HandRank == PokerPlayer.HandType.Flush);
            testHand.Clear();
            testHand.Add(new Card((int)Rank.Queen, (int)Suit.Club));
            testHand.Add(new Card((int)Rank.Jack, (int)Suit.Diamond));
            testHand.Add(new Card((int)Rank.King, (int)Suit.Heart));
            testHand.Add(new Card((int)Rank.Ace, (int)Suit.Club));
            testHand.Add(new Card((int)Rank.Ten, (int)Suit.Diamond));
            player.DrawHand(testHand);
            Assert.IsFalse(player.HasFlush());
            Assert.That(player.HandRank != PokerPlayer.HandType.Flush);
        }
        [Test]
        public void testHasFullHouse()
        {
            testHand.Add(new Card((int)Rank.Ten, (int)Suit.Club));
            testHand.Add(new Card((int)Rank.Ten, (int)Suit.Diamond));
            testHand.Add(new Card((int)Rank.Ten, (int)Suit.Heart));
            testHand.Add(new Card((int)Rank.Eight, (int)Suit.Club));
            testHand.Add(new Card((int)Rank.Eight, (int)Suit.Diamond));
            player.DrawHand(testHand);
            Assert.That(player.HasFullHouse());
            Assert.That(player.HandRank == PokerPlayer.HandType.FullHouse);
            testHand.Clear();
            testHand.Add(new Card((int)Rank.Ten, (int)Suit.Club));
            testHand.Add(new Card((int)Rank.Ten, (int)Suit.Diamond));
            testHand.Add(new Card((int)Rank.Ten, (int)Suit.Heart));
            testHand.Add(new Card((int)Rank.Eight, (int)Suit.Club));
            testHand.Add(new Card((int)Rank.Two, (int)Suit.Diamond));
            player.DrawHand(testHand);
            Assert.IsFalse(player.HasFullHouse());
            Assert.That(player.HandRank != PokerPlayer.HandType.FullHouse);
        }
        [Test]
        public void testHasFourOfAKind()
        {
            testHand.Add(new Card((int)Rank.Ten, (int)Suit.Club));
            testHand.Add(new Card((int)Rank.Ten, (int)Suit.Diamond));
            testHand.Add(new Card((int)Rank.Ten, (int)Suit.Heart));
            testHand.Add(new Card((int)Rank.Ten, (int)Suit.Spade));
            testHand.Add(new Card((int)Rank.Queen, (int)Suit.Spade));
            player.DrawHand(testHand);
            Assert.That(player.HasFourOfAKind());
            Assert.That(player.HandRank == PokerPlayer.HandType.FourOfAKind);
            testHand.Clear();

            testHand.Add(new Card((int)Rank.Ten, (int)Suit.Club));
            testHand.Add(new Card((int)Rank.Ten, (int)Suit.Diamond));
            testHand.Add(new Card((int)Rank.Ten, (int)Suit.Club));
            testHand.Add(new Card((int)Rank.Ten, (int)Suit.Spade));
            testHand.Add(new Card((int)Rank.Queen, (int)Suit.Spade));
            player.DrawHand(testHand);
            Assert.IsFalse(player.HasFourOfAKind());
            Assert.That(player.HandRank != PokerPlayer.HandType.FourOfAKind);
        }
        [Test]
        public void testHasStraightFlush()
        {
            testHand.Add(new Card((int)Rank.Two, (int)Suit.Club));
            testHand.Add(new Card((int)Rank.Three, (int)Suit.Club));
            testHand.Add(new Card((int)Rank.Five, (int)Suit.Club));
            testHand.Add(new Card((int)Rank.Four, (int)Suit.Club));
            testHand.Add(new Card((int)Rank.Six, (int)Suit.Club));
            player.DrawHand(testHand);
            Assert.That(player.HasStraightFlush());
            Assert.That(player.HandRank == PokerPlayer.HandType.StraightFlush);
            Assert.IsFalse(player.HasPair());
            testHand.Clear();
            testHand.Add(new Card((int)Rank.Two, (int)Suit.Diamond));
            testHand.Add(new Card((int)Rank.Three, (int)Suit.Diamond));
            testHand.Add(new Card((int)Rank.King, (int)Suit.Diamond));
            testHand.Add(new Card((int)Rank.Four, (int)Suit.Diamond));
            testHand.Add(new Card((int)Rank.Ace, (int)Suit.Diamond));
            player.DrawHand(testHand);
            Assert.IsFalse(player.HasStraightFlush());
            Assert.That(player.HandRank != PokerPlayer.HandType.StraightFlush);
            testHand.Clear();
            testHand.Add(new Card((int)Rank.Queen, (int)Suit.Heart));
            testHand.Add(new Card((int)Rank.Jack, (int)Suit.Heart));
            testHand.Add(new Card((int)Rank.King, (int)Suit.Heart));
            testHand.Add(new Card((int)Rank.Nine, (int)Suit.Heart));
            testHand.Add(new Card((int)Rank.Ten, (int)Suit.Heart));
            player.DrawHand(testHand);
            Assert.That(player.HasStraightFlush());
            Assert.That(player.HandRank == PokerPlayer.HandType.StraightFlush);
        }
        [Test]
        public void testHasRoyalFlush()
        {
            testHand.Add(new Card((int)Rank.Two, (int)Suit.Club));
            testHand.Add(new Card((int)Rank.Three, (int)Suit.Club));
            testHand.Add(new Card((int)Rank.Five, (int)Suit.Club));
            testHand.Add(new Card((int)Rank.Four, (int)Suit.Club));
            testHand.Add(new Card((int)Rank.Six, (int)Suit.Club));
            player.DrawHand(testHand);
            Assert.IsFalse(player.HasRoyalFlush());
            Assert.That(player.HandRank != PokerPlayer.HandType.RoyalFlush);
            Assert.IsFalse(player.HasPair());
            testHand.Add(new Card((int)Rank.Two, (int)Suit.Diamond));
            testHand.Add(new Card((int)Rank.Three, (int)Suit.Diamond));
            testHand.Add(new Card((int)Rank.King, (int)Suit.Diamond));
            testHand.Add(new Card((int)Rank.Four, (int)Suit.Diamond));
            testHand.Add(new Card((int)Rank.Ace, (int)Suit.Diamond));
            player.DrawHand(testHand);
            Assert.IsFalse(player.HasRoyalFlush());
            Assert.That(player.HandRank != PokerPlayer.HandType.RoyalFlush);
            testHand.Clear();
            testHand.Add(new Card((int)Rank.Queen, (int)Suit.Heart));
            testHand.Add(new Card((int)Rank.Jack, (int)Suit.Heart));
            testHand.Add(new Card((int)Rank.King, (int)Suit.Heart));
            testHand.Add(new Card((int)Rank.Ace, (int)Suit.Heart));
            testHand.Add(new Card((int)Rank.Ten, (int)Suit.Heart));
            player.DrawHand(testHand);
            Assert.That(player.HasRoyalFlush());
            Assert.That(player.HandRank == PokerPlayer.HandType.RoyalFlush);
        }
    }
}
