using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilORama4
{
	public enum DMXObjectType
	{
		Universe,
		Controller,
		Channel
	}

	public class ListItem
	{
		public ListItem()
		{ }
		public ListItem(string name, int id)
		{
			Name = name;
			ID = id;
		}
		public string Name { get; set; }

		public int ID { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}





	public interface IDMXThingy
	{
		int ID { get; set; }
		string Name { get; set; }
		string FullName { get; }
		string Comment { get; set; }
		string Location { get; set; }
		bool Active { get; set; }
		int UniverseNumber { get; }
		bool BadName { get; }
		bool BadNumber { get; }
		bool Editing { get; set; }
		bool Dirty { get; set; }
		object Tag { get; set; }
		int xLightsAddress { get; }
		bool ExactMatch { get; set; }
		int Number { get; set; }
		DMXObjectType ObjectType { get; }
	}
}
