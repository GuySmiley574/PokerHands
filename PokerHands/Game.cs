namespace PokerHands
{
    public class Game
    {
        private readonly Deck _deck;
        private readonly List<Player> players = new();
        private string Synopsis { get; set; } = "";

        public string SynopsisAll => GetSynopsisAll();

        public Game()
        {
            _deck = new Deck();
        }

        public void AddPlayer(string name)
        {
            players.Add(new Player() { Name = name, Hand = new Hand() });
        }

        private string GetSynopsisAll()
        {
            Synopsis = "";
            var playersSort = players.OrderByDescending(x => x.Hand).ToList();
            var balance = playersSort[0].Hand.CompareTo(playersSort[1].Hand);
            var winner = balance == 1 ? playersSort[0] : balance == -1 ? playersSort[1] : null; //0 = no winner
            foreach (var player in players)
            {
                Synopsis += $"{player.Name} had {player.Hand.ToString()}. {Environment.NewLine}";
            }
            if (winner == null) return Synopsis + $"It's a Tie between {playersSort[0].Name} and {playersSort[1].Name}! ";
            return Synopsis + $"{winner.Name} WINS with {winner.Hand.Name}!";
        }

        public void DealAll()
        {
            for (var i = 0; i < 5; i++)
            {
                players.ForEach(p=>p.Hand.AddCard(_deck.GetTopCard()));
            }
        }

        public void StackHand(string name, List<string> cards)
        {
            var player = players.FirstOrDefault(p => p.Name == name);
            if (player == null) return;
            player.Hand = new Hand();
            cards.ForEach(c => player.Hand.AddCard(new Card(c)));
        }
    }
}
