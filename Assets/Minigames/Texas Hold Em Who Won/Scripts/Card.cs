using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TexasHoldEm
{
    public enum Suit
    {
        Spades, Clubs, Hearts, Diamonds
    }

    public enum CardRank
    {
        Deuce,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }

    public enum CardLocation
    {
        Hole,
        Community
    }

    public class Card: IEquatable<Card>
    {
        public Suit suit;
        public CardRank cardRank;
        public CardLocation cardLocation;

        public bool Equals(Card otherCard)
        {
            if (otherCard == null)
            {
                return false;
            }

            return suit == otherCard.suit && cardRank == otherCard.cardRank;
        }

        public Card(Suit s, CardRank r, CardLocation location)
        {
            suit = s;
            cardRank = r;
            cardLocation = location;
        }
        
        public Card(Suit s, CardRank r)
        {
            suit = s;
            cardRank = r;
            cardLocation = CardLocation.Community;
        }

        public Card(Suit s)
        {
            suit = s;

            CardRank[] cardRanks = (CardRank[]) Enum.GetValues(typeof(CardRank));
            int ind = UnityEngine.Random.Range(0, cardRanks.Length);
            cardRank = cardRanks[ind];
            cardLocation = CardLocation.Community;
        }

        public Card(CardRank r)
        {
            cardRank = r;
            
            Suit[] suits = (Suit[]) Enum.GetValues(typeof(Suit));
            int ind = UnityEngine.Random.Range(0, suits.Length);
            suit = suits[ind];
            cardLocation = CardLocation.Community;
        }
        
        public string DebugString()
        {
            return "" + cardRank + " of " + suit;
        }

        public string SpriteString()
        {
            switch (cardRank)
            {
                case CardRank.Deuce:
                    return "2";
                case CardRank.Three:
                    return "3";
                case CardRank.Four:
                    return "4";
                case CardRank.Five:
                    return "5";
                case CardRank.Six:
                    return "6";
                case CardRank.Seven:
                    return "7";
                case CardRank.Eight:
                    return "8";
                case CardRank.Nine:
                    return "9";
                case CardRank.Ten:
                    return "10";
                case CardRank.Jack:
                    return "J";
                case CardRank.Queen:
                    return "Q";
                case CardRank.King:
                    return "K";
                case CardRank.Ace:
                    return "A";
            }

            return "";
        }
    }
    
    public class HandRankAndCards
    {
        public HandRank Rank;
        public List<Card> Cards; // Note: only includes cards relevant to the hand rank

        public HandRankAndCards(HandRank r, List<Card> c)
        {
            Rank = r;
            Cards = c;
        }
    }
    
    public enum HandRank
    {
        RoyalFlush,
        StraightFlush,
        FourOfAKind,
        FullHouse,
        Flush,
        Straight,
        ThreeOfAKind,
        TwoPair,
        OnePair,
        HighCard
    }
}