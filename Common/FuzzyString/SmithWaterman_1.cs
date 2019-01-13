using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SmithWaterman
{
	class SmithWaterman
	{
		private static int[,] matrix = {
	{ 4, -1, -2, -2,  0, -1, -1,  0, -2, -1, -1, -1, -1, -2, -1,  1,  0, -3, -2,  0},
	{-1,  5,  0, -2, -3,  1,  0, -2,  0, -3, -2,  2, -1, -3, -2, -1, -1, -3, -2, -3},
	{-2,  0,  6,  1, -3,  0,  0,  0,  1, -3, -3,  0, -2, -3, -2,  1,  0, -4, -2, -3},
	{-2, -2,  1,  6, -3,  0,  2, -1, -1, -3, -4, -1, -3, -3, -1,  0, -1, -4, -3, -3},
	{ 0, -3, -3, -3,  9, -3, -4, -3, -3, -1, -1, -3, -1, -2, -3, -1, -1, -2, -2, -1},
	{-1,  1,  0,  0, -3,  5,  2, -2,  0, -3, -2,  1,  0, -3, -1,  0, -1, -2, -1, -2},
	{-1,  0,  0,  2, -4,  2,  5, -2,  0, -3, -3,  1, -2, -3, -1,  0, -1, -3, -2, -2},
	{ 0, -2,  0, -1, -3, -2, -2,  6, -2, -4, -4, -2, -3, -3, -2,  0, -2, -2, -3, -3},
	{-2,  0,  1, -1, -3,  0,  0, -2,  8, -3, -3, -1, -2, -1, -2, -1, -2, -2,  2, -3},
	{-1, -3, -3, -3, -1, -3, -3, -4, -3,  4,  2, -3,  1,  0, -3, -2, -1, -3, -1,  3},
	{-1, -2, -3, -4, -1, -2, -3, -4, -3,  2,  4, -2,  2,  0, -3, -2, -1, -2, -1,  1},
	{-1,  2,  0, -1, -3,  1,  1, -2, -1, -3, -2,  5, -1, -3, -1,  0, -1, -3, -2, -2},
	{-1, -1, -2, -3, -1,  0, -2, -3, -2,  1,  2, -1,  5,  0, -2, -1, -1, -1, -1,  1},
	{-2, -3, -3, -3, -2, -3, -3, -3, -1,  0,  0, -3,  0,  6, -4, -2, -2,  1,  3, -1},
	{-1, -2, -2, -1, -3, -1, -1, -2, -2, -3, -3, -1, -2, -4,  7, -1, -1, -4, -3, -2},
	{ 1, -1,  1,  0, -1,  0,  0,  0, -1, -2, -2,  0, -1, -2, -1,  4,  1, -3, -2, -2},
	{ 0, -1,  0, -1, -1, -1, -1, -2, -2, -1, -1, -1, -1, -2, -1,  1,  5, -2, -2,  0},
	{-3, -3, -4, -4, -2, -2, -3, -2, -2, -3, -2, -3, -1,  1, -4, -3, -2, 11,  2, -3},
	{-2, -2, -2, -3, -2, -1, -2, -3,  2, -1, -1, -2, -1,  3, -3, -2, -2,  2,  7, -1},
	{ 0, -3, -3, -3, -1, -2, -2, -3, -3,  3,  1, -2,  1, -1, -2, -2,  0, -3, -1,  4}};

		// quick and dirty equivalent of typesafe enum pattern, can also use HashMap
		// or even better, EnumMap in Java 5. 
		// This code is for Java 1.4.2, so we will stick to the simple implementation
		private static int getIndex(char a)
		{
			// check for upper and lowercase characters
			switch (char.ToUpper(a))
			{
				case 'A': return 0;
				case 'R': return 1;
				case 'N': return 2;
				case 'D': return 3;
				case 'C': return 4;
				case 'Q': return 5;
				case 'E': return 6;
				case 'G': return 7;
				case 'H': return 8;
				case 'I': return 9;
				case 'L': return 10;
				case 'K': return 11;
				case 'M': return 12;
				case 'F': return 13;
				case 'P': return 14;
				case 'S': return 15;
				case 'T': return 16;
				case 'W': return 17;
				case 'Y': return 18;
				case 'V': return 19;
				default:  return 0;
			}
		}

		private const char NON_ALPHABETIC_CHARACTER1 = '§';
		private const char NON_ALPHABETIC_CHARACTER2 = '$';

		private enum Herkunft
		{
			KeineInformation,
			Oben,
			ObenLinks,
			Links
		}
		private struct Alignment
		{
			public string Seq1 { get; set; }
			public string Seq2 { get; set; }
		}

		private string SeqU = string.Empty, SeqV = string.Empty;
		private int[,] Matrix;
		private Herkunft[,] Herkunftsmatrix;
		private List OptimaleAlignments;

		public SmithWaterman(string sequenceU, string sequenceV)
		{
			SeqU = NON_ALPHABETIC_CHARACTER1 + sequenceU;
			SeqV = NON_ALPHABETIC_CHARACTER2 + sequenceV;

			this.Initialisieren();
			this.MatrixBerechnen();
			this.Backtrace();
		}

		private void Initialisieren()
		{
			Matrix = new int[SeqU.Length, SeqV.Length]; //Setzt praktischerweise gleichzeitig alles auf 0
			Herkunftsmatrix = new Herkunft[SeqU.Length, SeqV.Length];
			OptimaleAlignments = new List();
		}

		private void MatrixBerechnen()
		{
			int a, b, c;

			for (int SeqUCounter = 1; SeqUCounter & lt; SeqU.Length; SeqUCounter++)
            {
				for (int SeqVCounter = 1; SeqVCounter & lt; SeqV.Length; SeqVCounter++)
                {
					a = 0; b = 0; c = 0;

					a = Matrix[SeqUCounter - 1, SeqVCounter - 1] + Score(SeqU[SeqUCounter], SeqV[SeqVCounter]);
					b = Matrix[SeqUCounter - 1, SeqVCounter] + Score(SeqU[SeqUCounter], '-');
					c = Matrix[SeqUCounter, SeqVCounter - 1] + Score('-', SeqV[SeqVCounter]);
					int max = this.Max(a, b, c);

					if (max & lt; 0)
                    {
						max = 0;
					}

					if (max != 0)
					{
						if (max == a)
						{ Herkunftsmatrix[SeqUCounter, SeqVCounter] = Herkunft.ObenLinks; }
						if (max == b)
						{ Herkunftsmatrix[SeqUCounter, SeqVCounter] = Herkunft.Links; }
						if (max == c)
						{ Herkunftsmatrix[SeqUCounter, SeqVCounter] = Herkunft.Oben; }
					}

					Matrix[SeqUCounter, SeqVCounter] = max;

				}
			}

		}

		private int Score(char uj, char vj)
		{
			if (uj != '-' & amp; &amp; vj != '-')
            {
				//if (uj == vj)
				//{ 
				//    return 1; 
				//}

				//if (uj != vj)
				//{
				return matrix[getIndex(uj), getIndex(vj)];

				//}
			}
            else
            {
				return -5;
			}

			throw new Exception("Unreachable code reached...what?");
		}

		private void Backtrace()
		{
			List HöchsteZahl = new List();

			int tempHöchsteZahl = 0;

			//Höchste Zahl ermitteln
			for (int SeqUCounter = 1; SeqUCounter & lt; SeqU.Length; SeqUCounter++)
            {
				for (int SeqVCounter = 1; SeqVCounter & lt; SeqV.Length; SeqVCounter++)                 {
					if (Matrix[SeqUCounter, SeqVCounter] & gt; tempHöchsteZahl)
                    { tempHöchsteZahl = Matrix[SeqUCounter, SeqVCounter]; }
				}
			}

			//Alle raussuchen
			for (int SeqUCounter = 1; SeqUCounter & lt; SeqU.Length; SeqUCounter++)
            {
				for (int SeqVCounter = 1; SeqVCounter & lt; SeqV.Length; SeqVCounter++)
                {
					if (Matrix[SeqUCounter, SeqVCounter] == tempHöchsteZahl)
					{ HöchsteZahl.Add(new Point(SeqUCounter, SeqVCounter)); }
				}
			}

			for (int i = 0; i & lt; HöchsteZahl.Count; i++)
            {
				Alignment tempAlignment = new Alignment();
				int u = HöchsteZahl[i].X, v = HöchsteZahl[i].Y;

				while (Matrix[u, v] != 0)
				{

					switch (Herkunftsmatrix[u, v])
					{
						case Herkunft.KeineInformation:
							System.Windows.Forms.MessageBox.Show("Hummm");
							break;
						case Herkunft.Oben:
							tempAlignment.Seq1 = '-' + tempAlignment.Seq1;
							tempAlignment.Seq2 = SeqU[v] + tempAlignment.Seq2;
							v--;
							break;
						case Herkunft.ObenLinks:
							tempAlignment.Seq1 = SeqU[u] + tempAlignment.Seq1;
							tempAlignment.Seq2 = SeqV[v] + tempAlignment.Seq2;
							u--; v--;
							break;
						case Herkunft.Links:
							tempAlignment.Seq1 = SeqU[u] + tempAlignment.Seq1;
							tempAlignment.Seq2 = '-' + tempAlignment.Seq2;
							u--;
							break;
						default:
							break;
					}

				}

				OptimaleAlignments.Add(tempAlignment);
			}
		}
		private int Max(int a, int b, int c)
		{
			return Math.Max(a, Math.Max(b, c));

		}

		public string Print()
		{
			string tempString = string.Empty;
			SeqU = SeqU.Replace(NON_ALPHABETIC_CHARACTER1, '§');
			SeqV = SeqV.Replace(NON_ALPHABETIC_CHARACTER2, '§');

			//Matrix
			tempString += "Smith-Waterman Matrix\r\n";
			tempString += "§\t";
			for (int SeqUCounter = 0; SeqUCounter & lt; SeqU.Length; SeqUCounter++)
            {
				tempString += SeqU[SeqUCounter] + "\t";
			}
			tempString += "\r\n";

			for (int SeqVCounter = 0; SeqVCounter & lt; SeqV.Length; SeqVCounter++)
            {
				tempString += SeqV[SeqVCounter] + "\t";
				for (int SeqUCounter = 0; SeqUCounter & lt; SeqU.Length; SeqUCounter++)
                {

					tempString += Matrix[SeqUCounter, SeqVCounter] + "\t";

				}
				tempString += "\r\n";
			}

			tempString += "\r\n";

			//Optimale Alignments
			tempString += "Optimale Alignments\r\n";
			foreach (Alignment a in OptimaleAlignments)
			{
				foreach (char c in a.Seq1)
				{ tempString += c + "\t"; }

				tempString += "\r\n";

				foreach (char c in a.Seq2)
				{ tempString += c + "\t"; }

				tempString += "\r\n";
				tempString += "\r\n";
			}

			//Herkunftsmatrix
			tempString += "Herkunftsmatrix\r\n";
			for (int SeqUCounter = 0; SeqUCounter & lt; SeqU.Length; SeqUCounter++)
            {
				for (int SeqVCounter = 0; SeqVCounter & lt; SeqV.Length; SeqVCounter++)
                {
					if (Herkunftsmatrix[SeqUCounter, SeqVCounter] != Herkunft.KeineInformation)
					{
						tempString += string.Format("[{0}][{1}] = [{2}]\r\n", SeqUCounter, SeqVCounter, Herkunftsmatrix[SeqUCounter, SeqVCounter]);
					}
				}
				tempString += "\r\n";
			}

			return tempString;

		}
	}
}