﻿using System.Collections.ObjectModel;
//using SimMetrics.Net.API;

namespace FuzzyString
{
	//public static readonly TokeniserWhitespace _tokeniser = new TokeniserWhitespace();

	//public static class TokeniserWhitespace
	public static class _tokeniser
  {
    //private TokeniserUtilities<string> _tokenUtilities = new TokeniserUtilities<string>();

    public static Collection<string> Tokenize(string word)
    {
      Collection<string> collection = new Collection<string>();
      if (word != null)
      {
        int length;
        for (int i = 0; i < word.Length; i = length)
        {
          char c = word[i];
          if (char.IsWhiteSpace(c))
          {
            i++;
          }
          length = word.Length;
          for (int j = 0; j < Delimiters.Length; j++)
          {
            int index = word.IndexOf(Delimiters[j], i);
            if (index < length && index != -1)
            {
              length = index;
            }
          }
          string termToTest = word.Substring(i, length - i);
          if (!StopWordHandler.IsWord(termToTest))
          {
            collection.Add(termToTest);
          }
        }
      }
      return collection;
    }

    public static Collection<string> TokenizeToSet(string word)
    {
			TokeniserUtilities<string> _tokenUtilities = new TokeniserUtilities<string>();
			if (word != null)
      {
        return _tokenUtilities.CreateSet(Tokenize(word));
      }
      return null;
    }

    public static string Delimiters { get; } = "\r\n\t \x00a0";

    public static ITermHandler StopWordHandler { get; set; } = new DummyStopTermHandler();
  }
}