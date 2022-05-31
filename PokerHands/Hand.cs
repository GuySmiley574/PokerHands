namespace PokerHands
{
    public class Hand : IEquatable<Hand>, IComparable<Hand>
    {
        private string[] RankName = {"an incomplete hand","a high card","a pair", "two pair", "a three of a kind", "a straight", "a flush", "a full house", "a four of a kind", "a straight flush"};
        private readonly List<Card> _cards = new();

        public void AddCard(Card card)
        {
            if (_cards.Count == 5) return;
            _cards.Add(card);
            _cards.Sort();
        }

        public string Name => RankName[EvaluateHandRank()];

        private bool Flush => _cards.All(c => c.SuiteIndex == _cards.First().SuiteIndex);
        private bool Straight => _cards.GroupBy(x => x.FaceIndex).Count() == _cards.Count && //No pairs and
            _cards.All(c =>
            _cards.Any(c1 => c1.FaceIndex == c.FaceIndex + 1) ||
            c.FaceIndex == _cards.Max(c1 => c1.FaceIndex)); //Every card either has a buddy 1 step up or it is the top card

        private int EvaluateHandRank()
        {
            if(_cards.Count != 5) return 0; //Incomplete hand
            if (Straight && Flush) return 9; //Straight Flush
            var ofaKind = _cards.GroupBy(x => x.FaceIndex)
                .Where(g => g.Count() > 1)
                .Select(y => new { Element = y.Key, Counter = y.Count() })
                .ToList(); //find all duplicates include count
            var maxOfaKind = ofaKind.Select(x => x.Counter)
                .DefaultIfEmpty(0)
                .Max(); //highest count of a kind
            if (maxOfaKind == 4) return 8; //Four of a Kind
            if (maxOfaKind == 3 && ofaKind.Count == 2) return 7; //Full House
            if (Flush) return 6; //Flush
            if (Straight) return 5; //Straight
            if (maxOfaKind == 3) return 4; //Three of a Kind
            if (maxOfaKind == 2 && ofaKind.Count == 2) return 3; //Two Pair
            if (maxOfaKind == 2) return 2; //One Pair
            return 1;
        }

        public bool Equals(Hand? other)
        {
            return other != null && _cards.Select(c => c.FaceIndex).SequenceEqual(other._cards.Select(c => c.FaceIndex));
        }

        public int CompareTo(Hand? other)
        {
            if (other == null) return 1;
            if (EvaluateHandRank() != other.EvaluateHandRank()) return EvaluateHandRank() > other.EvaluateHandRank() ? 1 : -1;
            var thisCards = _cards.ToList();
            var otherCards = other._cards.ToList();
            var result = 0;
            while (thisCards.Count > 0 && result == 0)
            {
                var maxIndex = GetMaxIndex(thisCards);
                var maxIndexOther = GetMaxIndex(otherCards);
                if (maxIndex != maxIndexOther) result = maxIndex > maxIndexOther ? 1 : -1;
                thisCards.RemoveAll(c => c.FaceIndex == maxIndex);
                otherCards.RemoveAll(c => c.FaceIndex == maxIndexOther);
            }

            return result;
        }


        private static int GetMaxIndex(List<Card>? cards)
        {
            cards ??= new List<Card>();
            if (cards.Count == 0) return -1;
            var ofaKind = cards.GroupBy(x => x.FaceIndex)
                .Select(y => new { Element = y.Key, Counter = y.Count() })
                .ToList();
            var maxOfaKindCount = ofaKind.Select(x => x.Counter)
                .DefaultIfEmpty(-1)
                .Max();
            var maxOfaCount = ofaKind.Where(o=>o.Counter == maxOfaKindCount).Select(o=>o.Element).Max();
            return maxOfaCount;
        }

        public override string ToString()
        {
            return string.Join(" ",_cards);
        }
    }
}
