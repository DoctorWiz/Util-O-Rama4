using System.Collections.ObjectModel;

namespace FuzzyString
{
  public interface ITokeniser
  {
    Collection<string> Tokenize(string word);
    Collection<string> TokenizeToSet(string word);

    string Delimiters { get; }


    ITermHandler StopWordHandler { get; set; }
   }
}

