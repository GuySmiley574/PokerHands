namespace PokerHands
{
    public static class Extensions
    {
        private static Random randomizer = new Random();
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = randomizer.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
    }
}
