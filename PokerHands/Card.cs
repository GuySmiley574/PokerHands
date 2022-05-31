namespace PokerHands
{
    public class Card : IEquatable<Card>, IComparable<Card>
    {
        private string[] faces = {"2","3","4","5","6","7","8","9","10","J","Q","K","A"};
        private string[] suites = { "♠", "♥", "♣", "♦"};
        private string[] suitesAlpha = { "S", "H", "C", "D"};
        private int _suiteIndex;
        private int _faceIndex;

        public Card()
        {

        }

        public Card(string cardTxt)
        {
            FaceIndex = Array.IndexOf(faces, cardTxt.Substring(0,cardTxt.Length - 1)); //find index of matching face
            SuiteIndex = Array.IndexOf(suitesAlpha, cardTxt.Substring(cardTxt.Length - 1,1)); //find index of matching suite
        }


        public int FaceIndex
        {
            get => _faceIndex;
            set
            {
                if (value is < 0 or > 12) throw new ArgumentOutOfRangeException();
                _faceIndex = value;
            }
        }

        public string FaceName => faces[FaceIndex];

        public int SuiteIndex
        {
            get => _suiteIndex;
            set
            {
                if (value is < 0 or > 3) throw new ArgumentOutOfRangeException();
                _suiteIndex = value;
            }
        }

        public string SuiteName => suites[SuiteIndex];
        public bool Equals(Card? other)
        {
            if (other == null) return false;
            return other.FaceIndex == FaceIndex;
        }

        public int CompareTo(Card? other)
        {
            // A null value means that this object is greater.
            return other == null ? 1 : this.FaceIndex.CompareTo(other.FaceIndex);
        }

        public override string ToString()
        {
            return FaceName + SuiteName;
        }
    }
}
