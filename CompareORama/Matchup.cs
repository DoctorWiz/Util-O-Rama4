using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilORama4
{
	public class Matchup : IComparable
	{
		public int IndexDat = -1;
		public string NameDat = "";
		public string OutputDat = "";
		public string ColorDat = "";
		public int IndexLOR = -1;
		public int SavedIndex = -1;
		public string NameLOR = "";
		public bool ExactLOR = false;
		public string OutputLOR = "";
		public string ColorLOR = "";
		public int IndexViz = -1;
		public string NameViz = "";
		public bool ExactViz = false;
		public int TypeViz = -1;
		public string OutputViz = "";
		public string ColorViz = "";
		public int IndexxLights = -1;
		public string NamexLights = "";
		public bool ExactxLights = false;
		public int TypexLights = -1;
		public string OutputxLights = "";
		public string ColorxLights = "";
		public override string ToString()
		{
			return NameDat;
		}

		public int CompareTo(Object otherThing)
		{
			return NameDat.CompareTo(otherThing.ToString());
		}
		public int CompareTo(string otherName)
		{
			return NameDat.CompareTo(otherName);
		}

	}

}
