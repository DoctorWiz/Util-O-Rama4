using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using FileHelper;

namespace LOR4Utils
{
	public interface iLOR4Member : IComparable<iLOR4Member>
	{
		string Name
		{
			get;
		}
		void ChangeName(string newName);
		int Centiseconds
		{
			get;
			set;
		}
		int Index
		{
			get;
		}
		void SetIndex(int theIndex);
		int SavedIndex
		{
			get;
		}
		void SetSavedIndex(int theSavedIndex);
		int AltSavedIndex
		{
			get;
			set;
		}
		iLOR4Member Parent
		{
			get;
		}
		void SetParent(iLOR4Member newParent);
		// For Channels, RGBChannels, ChannelGroups, Tracks, Timings, etc.  This will be the parent Sequence
		// For VizChannels and VizDrawObjects this will be the parent Visualization
		bool Selected
		{
			get;
			set;
		}
		bool Dirty
		{
			get;
		}
		void MakeDirty(bool dirtyState);
		LOR4MemberType MemberType
		{
			get;
		}

		new int CompareTo(iLOR4Member otherObj);
		string LineOut();
		string ToString();
		void Parse(string lineIn);
		bool Written
		{
			get;
		}
		iLOR4Member Clone();
		iLOR4Member Clone(string newName);
		object Tag
		{
			get;
			set;
		}
		iLOR4Member MapTo
		{
			get;
			set;
		}
		bool ExactMatch
		{
			get;
			set;
		}
		int UniverseNumber
		{
			get;
		}
		int DMXAddress
		{
			get;
		}

	}
}