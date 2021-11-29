using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

namespace TexasHoldEm
{
    public class TexasHoldEmWhoWonManager : MonoBehaviour
    {
        public Canvas Canvas;
        public Image HoleCard1;
        public Image HoleCard2;
        public Image HoleCard3;
        public Image HoleCard4;
        public Image SharedCard1;
        public Image SharedCard2;
        public Image SharedCard3;
        public Image SharedCard4;
        public Image SharedCard5;
        public Image DeckPlaceholder;
        
        public TextMeshProUGUI Hand1Text;
        public TextMeshProUGUI Hand2Text;
        public PrintTestResults PrintResults;

        private Dictionary<string, Sprite> Clubs = new Dictionary<string, Sprite>();
        private Dictionary<string, Sprite> Diamonds = new Dictionary<string, Sprite>();
        private Dictionary<string, Sprite> Hearts = new Dictionary<string, Sprite>();
        private Dictionary<string, Sprite> Spades = new Dictionary<string, Sprite>();
        private Dictionary<Card, Image> ImageForCard = new Dictionary<Card, Image>();

        public enum PrintTestResults
        {
            All, PassingOnly, FailingOnly, None
        }
        
        private void Awake()
        {
            #if UNITY_EDITOR
            RunTests();
            #endif
            
            LoadImages();
            StartCoroutine("SetupGame");
        }

        IEnumerator SetupGame()
        {
            HoleCard1.gameObject.SetActive(false);
            HoleCard2.gameObject.SetActive(false);
            HoleCard3.gameObject.SetActive(false);
            HoleCard4.gameObject.SetActive(false);
            SharedCard1.gameObject.SetActive(false);
            SharedCard2.gameObject.SetActive(false);
            SharedCard3.gameObject.SetActive(false);
            SharedCard4.gameObject.SetActive(false);
            SharedCard5.gameObject.SetActive(false);
            
            Hand1Text.gameObject.SetActive(false);
            Hand2Text.gameObject.SetActive(false);
            
            DeckPlaceholder.gameObject.SetActive(true);

            var placeholderRT = DeckPlaceholder.GetComponent<RectTransform>();

            var delay = 0.1f;

            yield return new WaitForSeconds(5f);
            
            yield return new WaitForSeconds(delay);
            DealCard(HoleCard1.gameObject, placeholderRT);
            
            yield return new WaitForSeconds(delay);
            DealCard(HoleCard2.gameObject, placeholderRT);
            
            yield return new WaitForSeconds(delay);
            DealCard(HoleCard3.gameObject, placeholderRT);
            
            yield return new WaitForSeconds(delay);
            DealCard(HoleCard4.gameObject, placeholderRT);
            
            yield return new WaitForSeconds(delay);
            DealCard(SharedCard1.gameObject, placeholderRT);
            
            yield return new WaitForSeconds(delay);
            DealCard(SharedCard2.gameObject, placeholderRT);
            
            yield return new WaitForSeconds(delay);
            DealCard(SharedCard3.gameObject, placeholderRT);
            
            yield return new WaitForSeconds(delay);
            DealCard(SharedCard4.gameObject, placeholderRT);
            
            yield return new WaitForSeconds(delay);
            DealCard(SharedCard5.gameObject, placeholderRT);
        }

        void DealCard(GameObject target, RectTransform placeholderRT)
        {
            var card = Instantiate(DeckPlaceholder);
            card.gameObject.SetActive(true);
            card.transform.SetParent(Canvas.transform);
            var cardRT = card.GetComponent<RectTransform>();
            cardRT.anchoredPosition = placeholderRT.anchoredPosition;
            cardRT
                .DOAnchorPos(target.GetComponent<RectTransform>().anchoredPosition, 0.2f)
                .SetEase(Ease.InBack)
                .OnComplete(() =>
            {
                target.SetActive(true);
                Destroy(card);
            });
        }

        void LoadImages()
        {
            var clubImages = Resources.LoadAll("Playing Cards/Clubs", typeof(Sprite));
            foreach (var clubImage in clubImages)
            {
                Clubs[clubImage.name] = (Sprite)clubImage;
            }
            
            var spadeImages = Resources.LoadAll("Playing Cards/Spades", typeof(Sprite));
            foreach (var spadeImage in spadeImages)
            {
                Spades[spadeImage.name] = (Sprite)spadeImage;
            }
            
            var diamondImages = Resources.LoadAll("Playing Cards/Diamonds", typeof(Sprite));
            foreach (var diamondImage in diamondImages)
            {
                Diamonds[diamondImage.name] = (Sprite)diamondImage;
            }
            
            var heartImages = Resources.LoadAll("Playing Cards/Hearts", typeof(Sprite));
            foreach (var heartImage in heartImages)
            {
                Hearts[heartImage.name] = (Sprite)heartImage;
            }
        }
        
        void RunTests()
        {
            if (PrintResults == PrintTestResults.None)
            {
                return;
            }
            
            var results = TexasHoldEmWhoWonTests.Test();

            if (PrintResults == PrintTestResults.All || PrintResults == PrintTestResults.FailingOnly)
            {
                foreach (var s in results.FailedTests)
                {
                    print(s);
                }
            }

            if (PrintResults == PrintTestResults.All || PrintResults == PrintTestResults.PassingOnly)
            {
                foreach (var s in results.PassedTests)
                {
                    print(s);
                }
            }
        }

        Sprite SpriteForCard(Card card)
        {
            switch (card.suit)
            {
                case (Suit.Clubs):
                    return Clubs[card.SpriteString()];
                case (Suit.Spades):
                    return Spades[card.SpriteString()];
                case (Suit.Hearts):
                    return Hearts[card.SpriteString()];
                case (Suit.Diamonds):
                    return Diamonds[card.SpriteString()];
            }

            return null;
        }

        public void DrawAndRank()
        {
            var hand = DrawCards(7);
            
            HoleCard1.sprite = SpriteForCard(hand[0]);
            ImageForCard[hand[0]] = HoleCard1;
            
            HoleCard2.sprite = SpriteForCard(hand[1]);
            ImageForCard[hand[1]] = HoleCard2;
            
            SharedCard1.sprite = SpriteForCard(hand[2]);
            ImageForCard[hand[2]] = SharedCard1;
            
            SharedCard2.sprite = SpriteForCard(hand[3]);
            ImageForCard[hand[3]] = SharedCard2;
            
            SharedCard3.sprite = SpriteForCard(hand[4]);
            ImageForCard[hand[4]] = SharedCard3;
            
            SharedCard4.sprite = SpriteForCard(hand[5]);
            ImageForCard[hand[5]] = SharedCard4;
            
            SharedCard5.sprite = SpriteForCard(hand[6]);
            ImageForCard[hand[6]] = SharedCard5;

            var rank = Rank(hand);
            var rankText = "";

            switch (rank.Rank)
            {
                case HandRank.RoyalFlush:
                    rankText = "Royal flush";
                    break;
                case HandRank.StraightFlush:
                    rankText = "Straight flush";
                    break;
                case HandRank.FourOfAKind:
                    rankText = "Four of a kind";
                    break;
                case HandRank.FullHouse:
                    rankText = "Full house";
                    break;
                case HandRank.Flush:
                    rankText = "Flush";
                    break;
                case HandRank.Straight:
                    rankText = "Straight";
                    break;
                case HandRank.ThreeOfAKind:
                    rankText = "Three of a kind";
                    break;
                case HandRank.TwoPair:
                    rankText = "Two pair";
                    break;
                case HandRank.OnePair:
                    rankText = "One pair";
                    break;
                case HandRank.HighCard:
                    rankText = "High card";
                    break;
            }

            Hand1Text.text = rankText;
            HighlightCards(rank.Cards);
        }

        void HighlightCards(List<Card> cards)
        {
            var faded = Color.white;
            faded.a = 0.3f;

            foreach (Image image in ImageForCard.Values.ToList())
            {
                image.color = faded;
            }
            
            foreach (Card card in cards)
            {
                ImageForCard[card].color = Color.white;
            }
        }
        
        List<Card> DrawCards(int number)
        {
            // Make a new deck
            List<Card> deck = new List<Card>();

            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (CardRank cardRank in Enum.GetValues(typeof(CardRank)))
                {
                    deck.Add(new Card(suit, cardRank));
                }
            }

            // Draw cards into a new hand
            List<Card> hand = new List<Card>();
            for (int x = 0; x < number; x++)
            {
                var ind = UnityEngine.Random.Range(0, deck.Count);
                var c = deck[ind];
                if (x < 2)
                {
                    c.cardLocation = CardLocation.Hole;
                }
                else
                {
                    c.cardLocation = CardLocation.Community;
                }
                hand.Add(c);
                deck.RemoveAt(ind);
            }

            return hand;
        }

        public static HandRankAndCards Rank(List<Card> unsortedHand)
        {
            // First sort the cards from lowest to highest
            List<Card> hand = unsortedHand.OrderBy(x => (int) x.cardRank).ToList();
            var isFlush = IsFlush(hand);
            var isStraight = IsStraight(hand);

            if (isFlush != null && isStraight != null)
            {
                // Get the overlapping cards
                var overlappingCards = isFlush.Cards.Intersect(isStraight.Cards).OrderByDescending(x => (int) x.cardRank).ToList();

                if (overlappingCards.Count >= 5)
                {
                    if (overlappingCards.Exists(x => x.cardRank == CardRank.Ace) &&
                        overlappingCards.Exists(x => x.cardRank == CardRank.King) &&
                        overlappingCards.Exists(x => x.cardRank == CardRank.Queen) &&
                        overlappingCards.Exists(x => x.cardRank == CardRank.Jack) &&
                        overlappingCards.Exists(x => x.cardRank == CardRank.Ten)
                    )
                    {
                        return new HandRankAndCards(HandRank.RoyalFlush, overlappingCards.GetRange(0, 5));
                    }

                    return new HandRankAndCards(HandRank.StraightFlush, overlappingCards.GetRange(0, 5));
                }
            }

            if (isFlush != null)
            {
                return isFlush;
            }

            if (isStraight != null)
            {
                return isStraight;
            }

            Dictionary<CardRank, int> cardRankFrequency = new Dictionary<CardRank, int>();
            foreach (Card card in hand)
            {
                if (cardRankFrequency.ContainsKey(card.cardRank))
                {
                    cardRankFrequency[card.cardRank] += 1;
                }
                else
                {
                    cardRankFrequency[card.cardRank] = 1;
                }
            }

            var orderedFrequency = cardRankFrequency.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            
            if (orderedFrequency.First().Value == 4)
            {
                return new HandRankAndCards(HandRank.FourOfAKind, hand.FindAll(x => x.cardRank == orderedFrequency.First().Key));
            }

            if (orderedFrequency.First().Value == 3 && orderedFrequency.ContainsValue(2))
            {
                List<Card> relevantCards = new List<Card>();
                relevantCards.AddRange(hand.FindAll(x => x.cardRank == orderedFrequency.First().Key));

                var twoCardsCardRank = orderedFrequency.First(x => x.Value == 2).Key;
                relevantCards.AddRange(hand.FindAll(x => x.cardRank == twoCardsCardRank));
                
                return new HandRankAndCards(HandRank.FullHouse, relevantCards);
            }

            if (orderedFrequency.First().Value == 3)
            {
                return new HandRankAndCards(HandRank.ThreeOfAKind, hand.FindAll(x => x.cardRank == orderedFrequency.First().Key));
            }

            if (orderedFrequency.ElementAt(0).Value == 2 && orderedFrequency.ElementAt(1).Value == 2)
            {
                // We can have 2 or more pairs here, and we need to make sure we get the highest 2 pairs
                var relevantCards = new List<Card>();
                foreach (KeyValuePair<CardRank, int> pair in orderedFrequency)
                {
                    if (pair.Value == 2)
                    {
                        relevantCards.AddRange(hand.FindAll(x => x.cardRank == pair.Key));
                    }
                }

                relevantCards = relevantCards.OrderByDescending(x => (int) x.cardRank).ToList();
                return new HandRankAndCards(HandRank.TwoPair, relevantCards.GetRange(0, 4));
            }

            if (orderedFrequency.ElementAt(0).Value == 2)
            {
                return new HandRankAndCards(HandRank.OnePair, hand.FindAll(x => x.cardRank == orderedFrequency.ElementAt(0).Key));
            }

            var holeCards = hand.FindAll(x => x.cardLocation == CardLocation.Hole).OrderByDescending(x => (int) x.cardRank).ToList();
            return new HandRankAndCards(HandRank.HighCard, new List<Card> { holeCards[0] });
        }

        public static HandRankAndCards IsStraight(List<Card> unsortedHand)
        {
            var printLogs = false;
            var id = Guid.NewGuid();
            
            List<Card> hand = unsortedHand.OrderBy(x => (int) (x.cardRank)).ToList();
            
            if (printLogs)
            {
                foreach (Card card in hand)
                {
                    print(id + " " + card.DebugString());
                }
            }
            
            // To cover the A, 2, 3, 4, 5 straight, add ace to the beginning if present
            if (hand.Last().cardRank == CardRank.Ace)
            {
                hand.Insert(0, new Card(hand.Last().suit, hand.Last().cardRank));
            }

            var streak = new List<Card>{hand[0]};
            var bestStreak = new List<Card>();

            for ( var x = 1; x < hand.Count; x++)
            {
                if (printLogs)
                {
                    print(id + " loop #" + x);
                    print(id + " previous card: " + hand[x - 1].DebugString());
                    print(id + " current card: " + hand[x].DebugString());
                }
                
                // Case 1: Same rank so ignore
                if (hand[x].cardRank == hand[x - 1].cardRank)
                {
                    if (printLogs)
                    {
                        print(id + " case 1: same rank so ignore");
                    }
                    continue;
                }

                // Case 2: Ascending rank so add to streak
                if ((int) hand[x].cardRank - 1 == (int) hand[x - 1].cardRank ||
                    hand[x - 1].cardRank == CardRank.Ace && hand[x].cardRank == CardRank.Deuce)
                {
                    if (printLogs)
                    {
                        print(id + " case 2: ascending rank so add to streak");
                    }
                    streak.Add(hand[x]);
                    continue;
                }
                
                // Case 3: Not ascending so optionally save then start a new streak
                if (streak.Count >= bestStreak.Count)
                {
                    bestStreak = new List<Card>(streak);
                    if (printLogs)
                    {
                        print(id + " update bestStreak. New count is " + bestStreak.Count);
                    }
                }
                
                streak.Clear();
                streak.Add(hand[x]);
            }
            
            // We might have ended on a streak
            if (streak.Count >= bestStreak.Count)
            {
                bestStreak = streak;
            }

            if (printLogs)
            {
                print(id + " bestStreak count is " + bestStreak.Count);
                foreach (Card card in bestStreak)
                {
                    print(id + " best streak " + card.DebugString());
                }
            }

            if (bestStreak.Count >= 5)
            {
                bestStreak = bestStreak.OrderBy(x => (int) (x.cardRank)).ToList();
                return new HandRankAndCards(HandRank.Straight, bestStreak.GetRange(bestStreak.Count - 5, 5));
            }
            else
            {
                return null;
            }
        }

        public static HandRankAndCards IsFlush(List<Card> hand)
        {
            int spadeCount = 0;
            int clubCount = 0;
            int diamondCount = 0;
            int heartCount = 0;

            foreach (Card card in hand)
            {
                switch (card.suit)
                {
                    case Suit.Spades:
                        spadeCount++;
                        break;
                    case Suit.Clubs:
                        clubCount++;
                        break;
                    case Suit.Diamonds:
                        diamondCount++;
                        break;
                    case Suit.Hearts:
                        heartCount++;
                        break;
                }
            }

            if (spadeCount < 5 && clubCount < 5 && diamondCount < 5 && heartCount < 5)
            {
                return null;
            }

            List<Card> cards = new List<Card>();
            if (spadeCount >= 5)
            {
                cards = hand.FindAll(x => x.suit == Suit.Spades);
            } else if (clubCount >= 5)
            {
                cards = hand.FindAll(x => x.suit == Suit.Clubs);
            } else if (diamondCount >= 5)
            {
                cards = hand.FindAll(x => x.suit == Suit.Diamonds);
            }
            else
            {
                cards = hand.FindAll(x => x.suit == Suit.Hearts);
            }

            return new HandRankAndCards(HandRank.Flush, cards.OrderBy(x => (int) (x.cardRank)).ToList());
        }
    }
}