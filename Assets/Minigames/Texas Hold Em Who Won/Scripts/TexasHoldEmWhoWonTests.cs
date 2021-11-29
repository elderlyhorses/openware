using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TexasHoldEm
{
    public class TexasHoldEmWhoWonTests
    {
        public class TestResults
        {
            public List<string> PassedTests;
            public List<string> FailedTests;

            public TestResults(List<string> passed, List<string> failed)
            {
                PassedTests = passed;
                FailedTests = failed;
            }
        }
        
        public static TestResults Test()
        {
            var passedTests = new List<string>();
            var failedTests = new List<string>();

            var highCardTest = HighCardTests();
            passedTests.AddRange(highCardTest.PassedTests);
            failedTests.AddRange(highCardTest.FailedTests);
            
            var pairTest = PairTests();
            passedTests.AddRange(pairTest.PassedTests);
            failedTests.AddRange(pairTest.FailedTests);
            
            var twoPairTest = TwoPairTests();
            passedTests.AddRange(twoPairTest.PassedTests);
            failedTests.AddRange(twoPairTest.FailedTests);
            
            var threeOfAKindTest = ThreeOfAKindTests();
            passedTests.AddRange(threeOfAKindTest.PassedTests);
            failedTests.AddRange(threeOfAKindTest.FailedTests);
            
            var straightTest = StraightTests();
            passedTests.AddRange(straightTest.PassedTests);
            failedTests.AddRange(straightTest.FailedTests);

            var flushTest = FlushTests();
            passedTests.AddRange(flushTest.PassedTests);
            failedTests.AddRange(flushTest.FailedTests);
            
            var fullHouseTest = FullHouseTests();
            passedTests.AddRange(fullHouseTest.PassedTests);
            failedTests.AddRange(fullHouseTest.FailedTests);
            
            var fourOfAKindTest = FourOfAKindTests();
            passedTests.AddRange(fourOfAKindTest.PassedTests);
            failedTests.AddRange(fourOfAKindTest.FailedTests);
            
            var straightFlushTest = StraightFlushTests();
            passedTests.AddRange(straightFlushTest.PassedTests);
            failedTests.AddRange(straightFlushTest.FailedTests);
            
            var royalFlushTest = RoyalFlushTests();
            passedTests.AddRange(royalFlushTest.PassedTests);
            failedTests.AddRange(royalFlushTest.FailedTests);
            
            return new TestResults(passedTests, failedTests);
        }
        
        static TestResults RoyalFlushTests()
        {
            var passedTests = new List<string>();
            var failedTests = new List<string>();

            var basicHand = new List<Card>();
            var basicExpectedCards = new List<Card>();
            basicExpectedCards.Add(new Card(Suit.Diamonds, CardRank.Ace, CardLocation.Hole));
            basicExpectedCards.Add(new Card(Suit.Diamonds, CardRank.King, CardLocation.Hole));
            basicExpectedCards.Add(new Card(Suit.Diamonds, CardRank.Queen));
            basicExpectedCards.Add(new Card(Suit.Diamonds, CardRank.Jack));
            basicExpectedCards.Add(new Card(Suit.Diamonds, CardRank.Ten));

            basicHand.AddRange(basicExpectedCards);
            basicHand.Add(new Card(Suit.Hearts, CardRank.Seven));
            basicHand.Add(new Card(Suit.Clubs, CardRank.Queen));
            basicHand = Utilities.Shuffle(basicHand);

            var basicResult = TexasHoldEmWhoWonManager.Rank(basicHand);

            if (basicResult.Rank == HandRank.RoyalFlush)
            {
                passedTests.Add("<color=#88d498>Passed: Royal flush rank</color>");
            }
            else
            {
                failedTests.Add("<color=#ef476f>Failed: Royal flush rank</color>");
            }

            if (basicResult.Cards.Count == 5 &&
                basicResult.Cards.Contains(basicExpectedCards[0]) &&
                basicResult.Cards.Contains(basicExpectedCards[1]) &&
                basicResult.Cards.Contains(basicExpectedCards[2]) &&
                basicResult.Cards.Contains(basicExpectedCards[3]) &&
                basicResult.Cards.Contains(basicExpectedCards[4]))
            {
                passedTests.Add("<color=#88d498>Passed: Royal flush cards</color>");
            }
            else
            {
                failedTests.Add("<color=#ef476f>Failed: Royal flush cards</color>");
            }
            
            var gapHand = new List<Card>();
            gapHand.Add(new Card(Suit.Diamonds, CardRank.Ace));
            gapHand.Add(new Card(Suit.Diamonds, CardRank.King));
            gapHand.Add(new Card(Suit.Diamonds, CardRank.Queen));
            gapHand.Add(new Card(Suit.Diamonds, CardRank.Nine));
            gapHand.Add(new Card(Suit.Diamonds, CardRank.Ten));
            gapHand.Add(new Card(Suit.Hearts, CardRank.Seven));
            gapHand.Add(new Card(Suit.Clubs, CardRank.Queen));
            gapHand = Utilities.Shuffle(gapHand);

            var gapResult = TexasHoldEmWhoWonManager.Rank(gapHand);

            if (gapResult.Rank != HandRank.RoyalFlush)
            {
                passedTests.Add("<color=#88d498>Passed: Gap royal flush rank</color>");
            }
            else
            {
                failedTests.Add("<color=#ef476f>Failed: Gap royal flush rank</color>");
            }
            
            // It's a flush and a ten-ace straight but not a straight flush
            var offsetFlushAndStraightHand = new List<Card>();
            offsetFlushAndStraightHand.Add(new Card(Suit.Diamonds, CardRank.Ace));
            offsetFlushAndStraightHand.Add(new Card(Suit.Diamonds, CardRank.King));
            offsetFlushAndStraightHand.Add(new Card(Suit.Diamonds, CardRank.Queen));
            offsetFlushAndStraightHand.Add(new Card(Suit.Diamonds, CardRank.Nine));
            offsetFlushAndStraightHand.Add(new Card(Suit.Hearts, CardRank.Ten));
            offsetFlushAndStraightHand.Add(new Card(Suit.Diamonds, CardRank.Seven));
            offsetFlushAndStraightHand.Add(new Card(Suit.Clubs, CardRank.Queen));
            offsetFlushAndStraightHand = Utilities.Shuffle(offsetFlushAndStraightHand);

            var offsetFlushAndStraightResult = TexasHoldEmWhoWonManager.Rank(offsetFlushAndStraightHand);

            if (offsetFlushAndStraightResult.Rank != HandRank.RoyalFlush)
            {
                passedTests.Add("<color=#88d498>Passed: offsetFlushAndStraight royal flush rank</color>");
            }
            else
            {
                failedTests.Add("<color=#ef476f>Failed: offsetFlushAndStraight royal flush rank</color>");
            }

            return new TestResults(passedTests, failedTests);
        }

        static TestResults StraightFlushTests()
        {
            var passedTests = new List<string>();
            var failedTests = new List<string>();

            var basicHand = new List<Card>();
            var basicExpectedCards = new List<Card>();
            basicExpectedCards.Add(new Card(Suit.Diamonds, CardRank.Six, CardLocation.Hole));
            basicExpectedCards.Add(new Card(Suit.Diamonds, CardRank.Seven, CardLocation.Hole));
            basicExpectedCards.Add(new Card(Suit.Diamonds, CardRank.Eight));
            basicExpectedCards.Add(new Card(Suit.Diamonds, CardRank.Nine));
            basicExpectedCards.Add(new Card(Suit.Diamonds, CardRank.Ten));

            basicHand.AddRange(basicExpectedCards);
            basicHand.Add(new Card(Suit.Hearts, CardRank.Seven));
            basicHand.Add(new Card(Suit.Clubs, CardRank.Queen));
            basicHand = Utilities.Shuffle(basicHand);

            var basicResult = TexasHoldEmWhoWonManager.Rank(basicHand);

            if (basicResult.Rank == HandRank.StraightFlush)
            {
                passedTests.Add("<color=#88d498>Passed: Straight flush rank</color>");
            }
            else
            {
                failedTests.Add("<color=#ef476f>Failed: Straight flush rank</color>");
            }

            if (basicResult.Cards.Count == 5 &&
                basicResult.Cards.Contains(basicExpectedCards[0]) &&
                basicResult.Cards.Contains(basicExpectedCards[1]) &&
                basicResult.Cards.Contains(basicExpectedCards[2]) &&
                basicResult.Cards.Contains(basicExpectedCards[3]) &&
                basicResult.Cards.Contains(basicExpectedCards[4]))
            {
                passedTests.Add("<color=#88d498>Passed: Straight flush cards</color>");
            }
            else
            {
                failedTests.Add("<color=#ef476f>Failed: Straight flush cards</color>");
            }
            
            var gapHand = new List<Card>();
            gapHand.Add(new Card(Suit.Diamonds, CardRank.Deuce));
            gapHand.Add(new Card(Suit.Diamonds, CardRank.Three));
            gapHand.Add(new Card(Suit.Diamonds, CardRank.Eight));
            gapHand.Add(new Card(Suit.Diamonds, CardRank.Nine));
            gapHand.Add(new Card(Suit.Diamonds, CardRank.Ten));
            gapHand.Add(new Card(Suit.Hearts, CardRank.Seven));
            gapHand.Add(new Card(Suit.Clubs, CardRank.Queen));
            gapHand = Utilities.Shuffle(gapHand);

            var gapResult = TexasHoldEmWhoWonManager.Rank(gapHand);

            if (gapResult.Rank != HandRank.StraightFlush)
            {
                passedTests.Add("<color=#88d498>Passed: Gap straight flush rank</color>");
            }
            else
            {
                failedTests.Add("<color=#ef476f>Failed: Gap straight flush rank</color>");
            }
            
            // It's a flush and a straight but not a straight flush
            var offsetStraightAndFlush = new List<Card>();
            offsetStraightAndFlush.Add(new Card(Suit.Diamonds, CardRank.Deuce));
            offsetStraightAndFlush.Add(new Card(Suit.Diamonds, CardRank.Three));
            offsetStraightAndFlush.Add(new Card(Suit.Diamonds, CardRank.Four));
            offsetStraightAndFlush.Add(new Card(Suit.Diamonds, CardRank.Five));
            offsetStraightAndFlush.Add(new Card(Suit.Hearts, CardRank.Six));
            offsetStraightAndFlush.Add(new Card(Suit.Hearts, CardRank.Nine));
            offsetStraightAndFlush.Add(new Card(Suit.Diamonds, CardRank.Queen));
            offsetStraightAndFlush = Utilities.Shuffle(offsetStraightAndFlush);

            var offsetStraightAndFlushResult = TexasHoldEmWhoWonManager.Rank(offsetStraightAndFlush);

            if (offsetStraightAndFlushResult.Rank != HandRank.StraightFlush)
            {
                passedTests.Add("<color=#88d498>Passed: Offset straight and flush rank</color>");
            }
            else
            {
                failedTests.Add("<color=#ef476f>Failed: Offset straight and flush rank</color>");
            }
            
            return new TestResults(passedTests, failedTests);
        }
        
        static TestResults FourOfAKindTests()
        {
            var passedTests = new List<string>();
            var failedTests = new List<string>();

            var hand1 = new List<Card>();
            var expectedCards = new List<Card>();
            expectedCards.Add(new Card(Suit.Hearts, CardRank.Ten, CardLocation.Hole));
            expectedCards.Add(new Card(Suit.Clubs, CardRank.Ten, CardLocation.Hole));
            expectedCards.Add(new Card(Suit.Spades, CardRank.Ten));
            expectedCards.Add(new Card(Suit.Diamonds, CardRank.Ten));

            hand1.AddRange(expectedCards);
            hand1.Add(new Card(Suit.Hearts, CardRank.Seven));
            hand1.Add(new Card(Suit.Diamonds, CardRank.Queen));
            hand1.Add(new Card(Suit.Spades, CardRank.Three));
            hand1 = Utilities.Shuffle(hand1);

            var result1 = TexasHoldEmWhoWonManager.Rank(hand1);

            if (result1.Rank == HandRank.FourOfAKind)
            {
                passedTests.Add("<color=#88d498>Passed: Four of a kind rank</color>");
            }
            else
            {
                failedTests.Add("<color=#ef476f>Failed: Four of a kind rank</color>");
            }

            if (result1.Cards.Count == 4 &&
                result1.Cards.Contains(expectedCards[0]) &&
                result1.Cards.Contains(expectedCards[1]) &&
                result1.Cards.Contains(expectedCards[2]) &&
                result1.Cards.Contains(expectedCards[3]))
            {
                passedTests.Add("<color=#88d498>Passed: Four of a kind cards</color>");
            }
            else
            {
                failedTests.Add("<color=#ef476f>Failed: Four of a kind cards</color>");
            }
            
            return new TestResults(passedTests, failedTests);
        }
        
        static TestResults FullHouseTests()
        {
            var passedTests = new List<string>();
            var failedTests = new List<string>();

            var hand1 = new List<Card>();
            var expectedCards = new List<Card>();
            expectedCards.Add(new Card(Suit.Hearts, CardRank.Jack, CardLocation.Hole));
            expectedCards.Add(new Card(Suit.Spades, CardRank.Jack, CardLocation.Hole));
            expectedCards.Add(new Card(Suit.Spades, CardRank.Nine));
            expectedCards.Add(new Card(Suit.Diamonds, CardRank.Nine));
            expectedCards.Add(new Card(Suit.Diamonds, CardRank.Nine));
            
            hand1.AddRange(expectedCards);
            hand1.Add(new Card(Suit.Hearts, CardRank.Seven));
            hand1.Add(new Card(Suit.Diamonds, CardRank.Queen));
            hand1 = Utilities.Shuffle(hand1);

            var result1 = TexasHoldEmWhoWonManager.Rank(hand1);

            if (result1.Rank == HandRank.FullHouse)
            {
                passedTests.Add("<color=#88d498>Passed: Full house rank</color>");
            }
            else
            {
                failedTests.Add("<color=#ef476f>Failed: Full house rank</color>");
            }

            if (result1.Cards.Count == 5 &&
                result1.Cards.Contains(expectedCards[0]) &&
                result1.Cards.Contains(expectedCards[1]) &&
                result1.Cards.Contains(expectedCards[2]) &&
                result1.Cards.Contains(expectedCards[3]) &&
                result1.Cards.Contains(expectedCards[4]))
            {
                passedTests.Add("<color=#88d498>Passed: Full house cards</color>");
            }
            else
            {
                failedTests.Add("<color=#ef476f>Failed: Full house cards</color>");
            }
            
            return new TestResults(passedTests, failedTests);
        }
        
        static TestResults StraightTests()
        {
            var passedTests = new List<string>();
            var failedTests = new List<string>();

            var lowStraightHand = new List<Card>();
            var lowStraightExpectedCards = new List<Card>();
            lowStraightExpectedCards.Add(new Card(Suit.Hearts, CardRank.Ace, CardLocation.Hole));
            lowStraightExpectedCards.Add(new Card(Suit.Spades, CardRank.Deuce, CardLocation.Hole));
            lowStraightExpectedCards.Add(new Card(Suit.Diamonds, CardRank.Three));
            lowStraightExpectedCards.Add(new Card(Suit.Clubs, CardRank.Four));
            lowStraightExpectedCards.Add(new Card(Suit.Clubs, CardRank.Five));
            
            lowStraightHand.AddRange(lowStraightExpectedCards);
            lowStraightHand.Add(new Card(Suit.Diamonds, CardRank.Queen));
            lowStraightHand.Add(new Card(Suit.Spades, CardRank.Seven));
            lowStraightHand = Utilities.Shuffle(lowStraightHand);

            var lowStraightResult1 = TexasHoldEmWhoWonManager.Rank(lowStraightHand);

            if (lowStraightResult1.Rank == HandRank.Straight)
            {
                passedTests.Add("<color=#88d498>Passed: Low straight rank</color>");
            }
            else
            {
                failedTests.Add("<color=#ef476f>Failed: Low straight rank</color>");
            }
            
            if (lowStraightResult1.Cards.Count == 5 &&
                lowStraightResult1.Cards.Contains(lowStraightExpectedCards[0]) &&
                lowStraightResult1.Cards.Contains(lowStraightExpectedCards[1]) &&
                lowStraightResult1.Cards.Contains(lowStraightExpectedCards[2]) &&
                lowStraightResult1.Cards.Contains(lowStraightExpectedCards[3]) &&
                lowStraightResult1.Cards.Contains(lowStraightExpectedCards[4]))
            {
                passedTests.Add("<color=#88d498>Passed: Low straight cards</color>");
            }
            else
            {
                failedTests.Add("<color=#ef476f>Failed: Low straight cards</color>");
            }
            
            // If the straight is longer than 5, we only want the highest 5 cards
            var longStraightHand = new List<Card>();
            var longStraightExpectedCards = new List<Card>();
            
            longStraightExpectedCards.Add(new Card(Suit.Diamonds, CardRank.Three, CardLocation.Hole));
            longStraightExpectedCards.Add(new Card(Suit.Clubs, CardRank.Four));
            longStraightExpectedCards.Add(new Card(Suit.Hearts, CardRank.Five));
            longStraightExpectedCards.Add(new Card(Suit.Hearts, CardRank.Six));
            longStraightExpectedCards.Add(new Card(Suit.Clubs, CardRank.Seven));
            
            longStraightHand.AddRange(longStraightExpectedCards);
            longStraightHand.Add(new Card(Suit.Spades, CardRank.Deuce, CardLocation.Hole));
            longStraightHand.Add(new Card(Suit.Diamonds, CardRank.Queen));
            longStraightHand = Utilities.Shuffle(longStraightHand);
            
            var longStraightResult1 = TexasHoldEmWhoWonManager.Rank(longStraightHand);
            
            if (longStraightResult1.Rank == HandRank.Straight)
            {
                passedTests.Add("<color=#88d498>Passed: Long straight rank</color>");
            }
            else
            {
                failedTests.Add("<color=#ef476f>Failed: Long straight rank</color>");
            }
            
            if (longStraightResult1.Cards.Count == 5 &&
                longStraightResult1.Cards.Contains(longStraightExpectedCards[0]) &&
                longStraightResult1.Cards.Contains(longStraightExpectedCards[1]) &&
                longStraightResult1.Cards.Contains(longStraightExpectedCards[2]) &&
                longStraightResult1.Cards.Contains(longStraightExpectedCards[3]) &&
                longStraightResult1.Cards.Contains(longStraightExpectedCards[4]))
            {
                passedTests.Add("<color=#88d498>Passed: Long straight cards</color>");
            }
            else
            {
                failedTests.Add("<color=#ef476f>Failed: Long straight cards</color>");
            }
            
            var midStraightHand = new List<Card>();
            var midStraightExpectedCards = new List<Card>();
            
            midStraightExpectedCards.Add(new Card(Suit.Hearts, CardRank.Five));
            midStraightExpectedCards.Add(new Card(Suit.Hearts, CardRank.Six));
            midStraightExpectedCards.Add(new Card(Suit.Diamonds, CardRank.Seven));
            midStraightExpectedCards.Add(new Card(Suit.Clubs, CardRank.Eight, CardLocation.Hole));
            midStraightExpectedCards.Add(new Card(Suit.Spades, CardRank.Nine));
            
            midStraightHand.AddRange(midStraightExpectedCards);
            midStraightHand.Add(new Card(Suit.Spades, CardRank.Deuce));
            midStraightHand.Add(new Card(Suit.Diamonds, CardRank.Queen, CardLocation.Hole));
            midStraightHand = Utilities.Shuffle(midStraightHand);
            
            var midStraightResult1 = TexasHoldEmWhoWonManager.Rank(midStraightHand);
            
            if (midStraightResult1.Rank == HandRank.Straight)
            {
                passedTests.Add("<color=#88d498>Passed: Mid straight rank</color>");
            }
            else
            {
                failedTests.Add("<color=#ef476f>Failed: Mid straight rank</color>");
            }
            
            if (midStraightResult1.Cards.Count == 5 &&
                midStraightResult1.Cards.Contains(midStraightExpectedCards[0]) &&
                midStraightResult1.Cards.Contains(midStraightExpectedCards[1]) &&
                midStraightResult1.Cards.Contains(midStraightExpectedCards[2]) &&
                midStraightResult1.Cards.Contains(midStraightExpectedCards[3]) &&
                midStraightResult1.Cards.Contains(midStraightExpectedCards[4]))
            {
                passedTests.Add("<color=#88d498>Passed: Mid straight cards</color>");
            }
            else
            {
                failedTests.Add("<color=#ef476f>Failed: Mid straight cards</color>");
            }
            
            var basicNoStraightHand = new List<Card>();
            basicNoStraightHand.Add(new Card(Suit.Spades, CardRank.Deuce, CardLocation.Hole));
            basicNoStraightHand.Add(new Card(Suit.Hearts, CardRank.Five, CardLocation.Hole));
            basicNoStraightHand.Add(new Card(Suit.Spades, CardRank.Five));
            basicNoStraightHand.Add(new Card(Suit.Clubs, CardRank.Ten));
            basicNoStraightHand.Add(new Card(Suit.Spades, CardRank.Ace));
            basicNoStraightHand.Add(new Card(Suit.Clubs, CardRank.Queen));
            basicNoStraightHand = Utilities.Shuffle(basicNoStraightHand);
            
            var basicNoStraightResult1 = TexasHoldEmWhoWonManager.Rank(basicNoStraightHand);
            
            if (basicNoStraightResult1.Rank != HandRank.Straight)
            {
                passedTests.Add("<color=#88d498>Passed: Basic no straight rank</color>");
            }
            else
            {
                failedTests.Add("<color=#ef476f>Failed: Basic no straight rank</color>");
            }
            
            var gapStraightHand = new List<Card>();
            gapStraightHand.Add(new Card(Suit.Hearts, CardRank.Five, CardLocation.Hole));
            gapStraightHand.Add(new Card(Suit.Hearts, CardRank.Six, CardLocation.Hole));
            gapStraightHand.Add(new Card(Suit.Diamonds, CardRank.Seven));
            gapStraightHand.Add(new Card(Suit.Clubs, CardRank.Ten));
            gapStraightHand.Add(new Card(Suit.Spades, CardRank.Jack));
            gapStraightHand.Add(new Card(Suit.Diamonds, CardRank.Queen));
            gapStraightHand = Utilities.Shuffle(gapStraightHand);
            
            var gapStraightResult1 = TexasHoldEmWhoWonManager.Rank(gapStraightHand);
            
            if (gapStraightResult1.Rank != HandRank.Straight)
            {
                passedTests.Add("<color=#88d498>Passed: Gap straight rank</color>");
            }
            else
            {
                failedTests.Add("<color=#ef476f>Failed: Gap straight rank</color>");
            }

            return new TestResults(passedTests, failedTests);
        }
        
        static TestResults ThreeOfAKindTests()
        {
            var passedTests = new List<string>();
            var failedTests = new List<string>();

            var hand1 = new List<Card>();
            var expectedCards = new List<Card>();
            expectedCards.Add(new Card(Suit.Hearts, CardRank.Ten, CardLocation.Hole));
            expectedCards.Add(new Card(Suit.Spades, CardRank.Ten));
            expectedCards.Add(new Card(Suit.Diamonds, CardRank.Ten));
            
            hand1.AddRange(expectedCards);
            hand1.Add(new Card(Suit.Diamonds, CardRank.Three, CardLocation.Hole));
            hand1.Add(new Card(Suit.Spades, CardRank.Ace));
            hand1.Add(new Card(Suit.Hearts, CardRank.Seven));
            hand1.Add(new Card(Suit.Diamonds, CardRank.Queen));
            hand1 = Utilities.Shuffle(hand1);

            var result1 = TexasHoldEmWhoWonManager.Rank(hand1);

            if (result1.Rank == HandRank.ThreeOfAKind)
            {
                passedTests.Add("<color=#88d498>Passed: Three of a kind 1 rank</color>");
            }
            else
            {
                failedTests.Add("<color=#ef476f>Failed: Three of a kind 1 rank</color>");
            }
            
            if (result1.Cards.Count == 3 &&
                result1.Cards.Contains(expectedCards[0]) &&
                result1.Cards.Contains(expectedCards[1]) &&
                result1.Cards.Contains(expectedCards[2]))
            {
                passedTests.Add("<color=#88d498>Passed: Three of a kind 1 cards</color>");
            }
            else
            {
                failedTests.Add("<color=#ef476f>Failed: Three of a kind 1 cards</color>");
            }
            
            return new TestResults(passedTests, failedTests);
        }
        
        static TestResults TwoPairTests()
        {
            var passedTests = new List<string>();
            var failedTests = new List<string>();

            // Basic 2 pair case
            var hand1 = new List<Card>();
            var expectedCards = new List<Card>();
            expectedCards.Add(new Card(Suit.Hearts, CardRank.Five, CardLocation.Hole));
            expectedCards.Add(new Card(Suit.Spades, CardRank.Five));
            expectedCards.Add(new Card(Suit.Diamonds, CardRank.Queen));
            expectedCards.Add(new Card(Suit.Hearts, CardRank.Queen, CardLocation.Hole));
            
            hand1.AddRange(expectedCards);
            hand1.Add(new Card(Suit.Spades, CardRank.Deuce));
            hand1.Add(new Card(Suit.Hearts, CardRank.Three));
            hand1.Add(new Card(Suit.Spades, CardRank.King));
            hand1.Add(new Card(Suit.Diamonds, CardRank.Seven));
            
            hand1 = Utilities.Shuffle(hand1);

            var result1 = TexasHoldEmWhoWonManager.Rank(hand1);

            if (result1.Rank == HandRank.TwoPair)
            {
                passedTests.Add("<color=#88d498>Passed: Two pair 1 rank</color>");
            }
            else
            {
                failedTests.Add("<color=#ef476f>Failed: Two pair 1 rank</color>");
            }

            if (result1.Cards.Count == 4 &&
                result1.Cards.Contains(expectedCards[0]) &&
                result1.Cards.Contains(expectedCards[1]) &&
                result1.Cards.Contains(expectedCards[2]) &&
                result1.Cards.Contains(expectedCards[3]))
            {
                passedTests.Add("<color=#88d498>Passed: Two pair 1 cards</color>");
            }
            else
            {
                failedTests.Add("<color=#ef476f>Failed: Two pair 1 cards</color>");
            }
            
            // 3 pair case: we should get the top 2 pairs
            var hand2 = new List<Card>();
            var expectedCards2 = new List<Card>();
            expectedCards2.Add(new Card(Suit.Hearts, CardRank.Jack));
            expectedCards2.Add(new Card(Suit.Spades, CardRank.Jack));
            expectedCards2.Add(new Card(Suit.Diamonds, CardRank.Nine));
            expectedCards2.Add(new Card(Suit.Hearts, CardRank.Nine));
            
            hand2.AddRange(expectedCards2);
            hand2.Add(new Card(Suit.Spades, CardRank.Deuce));
            hand2.Add(new Card(Suit.Hearts, CardRank.Three));
            hand2.Add(new Card(Suit.Spades, CardRank.Three));
            hand2.Add(new Card(Suit.Diamonds, CardRank.Seven));
            
            hand2 = Utilities.Shuffle(hand2);

            var result2 = TexasHoldEmWhoWonManager.Rank(hand2);
            if (result2.Rank == HandRank.TwoPair)
            {
                passedTests.Add("<color=#88d498>Passed: Two pair 2 rank</color>");
            }
            else
            {
                failedTests.Add("<color=#ef476f>Failed: Two pair 2 rank</color>");
            }
            
            if (result2.Cards.Count == 4 &&
                result2.Cards.Contains(expectedCards2[0]) &&
                result2.Cards.Contains(expectedCards2[1]) &&
                result2.Cards.Contains(expectedCards2[2]) &&
                result2.Cards.Contains(expectedCards2[3]))
            {
                passedTests.Add("<color=#88d498>Passed: Two pair 2 cards</color>");
            }
            else
            {
                failedTests.Add("<color=#ef476f>Failed: Two pair 2 cards</color>");
            }
            
            return new TestResults(passedTests, failedTests);
        }
        
        static TestResults PairTests()
        {
            var passedTests = new List<string>();
            var failedTests = new List<string>();

            var hand1 = new List<Card>();
            var expectedCards = new List<Card>();
            expectedCards.Add(new Card(Suit.Hearts, CardRank.Five, CardLocation.Hole));
            expectedCards.Add(new Card(Suit.Spades, CardRank.Five));
            
            hand1.AddRange(expectedCards);
            hand1.Add(new Card(Suit.Spades, CardRank.Deuce));
            hand1.Add(new Card(Suit.Hearts, CardRank.Three));
            hand1.Add(new Card(Suit.Spades, CardRank.King));
            hand1.Add(new Card(Suit.Hearts, CardRank.Seven, CardLocation.Hole));
            hand1.Add(new Card(Suit.Diamonds, CardRank.Queen));
            hand1 = Utilities.Shuffle(hand1);

            var result1 = TexasHoldEmWhoWonManager.Rank(hand1);

            if (result1.Rank == HandRank.OnePair)
            {
                passedTests.Add("<color=#88d498>Passed: One pair 1 rank</color>");
            }
            else
            {
                failedTests.Add("<color=#ef476f>Failed: One pair 1 rank</color>");
            }

            if (result1.Cards.Count == 2 && result1.Cards.Contains(expectedCards[0]) && result1.Cards.Contains(expectedCards[1]))
            {
                passedTests.Add("<color=#88d498>Passed: One pair 1 cards</color>");
            }
            else
            {
                failedTests.Add("<color=#ef476f>Failed: One pair 1 cards</color>");
            }
            
            return new TestResults(passedTests, failedTests);
        }

        static TestResults HighCardTests()
        {
            var passedTests = new List<string>();
            var failedTests = new List<string>();

            var hand1 = new List<Card>();
            var expectedHighCard1 = new Card(Suit.Clubs, CardRank.King, CardLocation.Hole);
            hand1.Add(expectedHighCard1);
            hand1.Add(new Card(Suit.Clubs, CardRank.Deuce, CardLocation.Hole));
            hand1.Add(new Card(Suit.Hearts, CardRank.Three, CardLocation.Community));
            hand1.Add(new Card(Suit.Hearts, CardRank.Five, CardLocation.Community));
            hand1.Add(new Card(Suit.Diamonds, CardRank.Seven, CardLocation.Community));
            hand1.Add(new Card(Suit.Diamonds, CardRank.Ten, CardLocation.Community));
            hand1.Add(new Card(Suit.Diamonds, CardRank.Queen, CardLocation.Community));
            hand1 = Utilities.Shuffle(hand1);

            var result1 = TexasHoldEmWhoWonManager.Rank(hand1);

            if (result1.Rank == HandRank.HighCard)
            {
                passedTests.Add("<color=#88d498>Passed: High card 1 rank</color>");
            }
            else
            {
                failedTests.Add("<color=#ef476f>Failed: High card 1 rank</color>");
            }

            if (result1.Cards.Count == 1 && result1.Cards[0] == expectedHighCard1)
            {
                passedTests.Add("<color=#88d498>Passed: High card 1 cards</color>");
            }
            else
            {
                failedTests.Add("<color=#ef476f>Failed: High card 1 cards</color>");
            }
            
            return new TestResults(passedTests, failedTests);
        }
        
        static TestResults FlushTests()
        {
            var passedTests = new List<string>();
            var failedTests = new List<string>();
            
            var flushClubs = new List<Card>();
            flushClubs.Add(new Card(Suit.Hearts, CardRank.Ace, CardLocation.Hole));
            flushClubs.Add(new Card(Suit.Spades, CardRank.Three));
            
            var expectedResults = new List<Card>();
            expectedResults.Add(new Card(Suit.Clubs, CardRank.Three, CardLocation.Hole));
            expectedResults.Add(new Card(Suit.Clubs, CardRank.Five));
            expectedResults.Add(new Card(Suit.Clubs, CardRank.Seven));
            expectedResults.Add(new Card(Suit.Clubs, CardRank.Nine));
            expectedResults.Add(new Card(Suit.Clubs, CardRank.Jack));
            expectedResults = expectedResults.OrderBy(x => (int) (x.cardRank)).ToList();
            
            flushClubs.AddRange(expectedResults);
            flushClubs = Utilities.Shuffle(flushClubs);

            var flushClubsTest = TexasHoldEmWhoWonManager.Rank(flushClubs);
            if (flushClubsTest.Rank == HandRank.Flush)
            {
                passedTests.Add("<color=#88d498>Passed: Flush clubs rank</color>");
            }
            else
            {
                failedTests.Add("<color=#ef476f>Failed: Flush clubs rank</color>");
            }
            
            if (
                flushClubsTest.Cards[0] == expectedResults[0] &&
                flushClubsTest.Cards[1] == expectedResults[1] &&
                flushClubsTest.Cards[2] == expectedResults[2] &&
                flushClubsTest.Cards[3] == expectedResults[3] &&
                flushClubsTest.Cards[4] == expectedResults[4]
            )
            {
                passedTests.Add("<color=#88d498>Passed: Flush clubs cards</color>");
            }
            else
            {
                failedTests.Add("<color=#ef476f>Failed: Flush clubs cards</color>");
            }
            
            return new TestResults(passedTests, failedTests);
        }
    }
}