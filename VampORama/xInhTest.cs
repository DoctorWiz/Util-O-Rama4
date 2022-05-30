using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilORama4
{
	// Interface
	public interface ixThing
	{
		string Name
		{ get; set; }

		int Chips
		{ get; set; }
	}

	// Base Class	
	public class xThingBase : ixThing
	{
		protected string _name;
		protected int _chips;

		// Constructors
		public xThingBase()
		{ }

		public xThingBase(string name, int chips)
		{
			_name = name;
			_chips = chips;
		}

		// Properties, Methods, Functions
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		public int Chips
		{
			get { return _chips; }
			set { _chips = value; }
		}
	}

	// Derivative class
	public class xFancyThing : xThingBase, ixThing
	{
		private int _dips;

		// Constructor
		public xFancyThing(string name, int chips)
		{
			_name = name;
			_chips = chips;
		}

		// Properties, Methods, Functions (new, additional)
		public int Dips
		{
			get { return _dips; }
			set { _dips = value; }
		}
	}

}
