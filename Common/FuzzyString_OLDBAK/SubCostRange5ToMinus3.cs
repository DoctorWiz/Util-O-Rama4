using System.Collections.ObjectModel;
//using SimMetrics.Net.API;

namespace FuzzyString
{
  public partial class FuzzyFunctions // SubCostRange5ToMinus3 : AbstractSubstitutionCost
  {
    //private readonly Collection<string>[] _approx = new Collection<string>[7];
    private const double CharApproximateMatchScore = 3.0;
    private const double Char53ExactMatchScore = 5.0;
    private const double Char53MismatchScore = -3.0;

    private static void SubCostRange5ToMinus3(ref Collection<string>[] _approx)
    {
      _approx[0] = new Collection<string>();
      _approx[0].Add("d");
      _approx[0].Add("t");
      _approx[1] = new Collection<string>();
      _approx[1].Add("g");
      _approx[1].Add("j");
      _approx[2] = new Collection<string>();
      _approx[2].Add("l");
      _approx[2].Add("r");
      _approx[3] = new Collection<string>();
      _approx[3].Add("m");
      _approx[3].Add("n");
      _approx[4] = new Collection<string>();
      _approx[4].Add("b");
      _approx[4].Add("p");
      _approx[4].Add("v");
      _approx[5] = new Collection<string>();
      _approx[5].Add("a");
      _approx[5].Add("e");
      _approx[5].Add("i");
      _approx[5].Add("o");
      _approx[5].Add("u");
      _approx[6] = new Collection<string>();
      _approx[6].Add(",");
      _approx[6].Add(".");
    }

    public static double Get53Cost(this string source, int firstWordIndex, string target, int secondWordIndex)
    {

			Collection<string>[] _approx = new Collection<string>[7];
			SubCostRange5ToMinus3(ref _approx);

			if (source != null && target != null)
      {
        if (source.Length <= firstWordIndex || firstWordIndex < 0)
        {
          return Char53MismatchScore;
        }

        if (target.Length <= secondWordIndex || secondWordIndex < 0)
        {
          return Char53MismatchScore;
        }

        if (source[firstWordIndex] == target[secondWordIndex])
        {
          return Char53ExactMatchScore;
        }

        char ch = source[firstWordIndex];
        string item = ch.ToString().ToLowerInvariant();
        char ch2 = target[secondWordIndex];
        string str2 = ch2.ToString().ToLowerInvariant();
        for (int i = 0; i < _approx.Length; i++)
        {
          if (_approx[i].Contains(item) && _approx[i].Contains(str2))
          {
            return CharApproximateMatchScore;
          }
        }
      }
      return Char53MismatchScore;
    }

    public static double Max53Cost => Char53ExactMatchScore;

    public static double Min53Cost => Char53MismatchScore;

  }
}