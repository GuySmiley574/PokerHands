namespace PokerHands
{
    public class Deck
    {
        private readonly List<Card> _cards = new List<Card>();
        private readonly Stack<Card> _cardsDealt = new Stack<Card>();
        private readonly Stack<Card> _cardsRemaining = new Stack<Card>();

        public Deck()
        {
            //create the cards
            for (int i = 0; i < 4; i++) // 4 suites
            {
                for(int j = 0; j < 13; j++) // 13 values
                    _cards.Add(new Card(){SuiteIndex = i,FaceIndex = j}); 
            }
            _cards.Shuffle();
            _cards.ForEach(c=>_cardsRemaining.Push(c));
        }

        public Card GetTopCard()
        {
            if (_cardsRemaining.Count == 0) throw new Exception("Out of cards");
            var card = _cardsRemaining.Pop();
            _cardsDealt.Push(card);
            return card;
        }

        public void ReturnCardsAndShuffle()
        {
            _cardsDealt.Clear();
            _cardsRemaining.Clear();
            _cards.Shuffle();
            _cards.ForEach(c=>_cardsRemaining.Push(c));
        }
    }
}
