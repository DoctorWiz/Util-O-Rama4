using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using FileHelper;

namespace LORUtils4
{
	public interface iLORMember4 : IComparable<iLORMember4>
	{
		string Name
		{	get; }
		void ChangeName(string newName);
		int Centiseconds
		{	get; set;	}
		int Index
		{	get; }
		void SetIndex(int theIndex);
		int SavedIndex
		{	get; }
		void SetSavedIndex(int theSavedIndex);
		int AltSavedIndex
		{	get; set;	}
		iLORMember4 Parent
		{	get; }
		void SetParent(iLORMember4 newParent);
		// For Channels, RGBChannels, ChannelGroups, Tracks, Timings, etc.  This will be the parent Sequence
		// For VizChannels and VizDrawObjects this will be the parent Visualization
		bool Selected
		{	get; set;	}
		bool Dirty
		{	get; }
		void MakeDirty(bool dirtyState = true);
		LORMemberType4 MemberType
		{	get; }
		int CompareTo(iLORMember4 otherObj);
		string LineOut();
		string ToString();
		void Parse(string lineIn);
		bool Written
		{	get; }
		iLORMember4 Clone();
		iLORMember4 Clone(string newName);
		object Tag
		{	get; set; }
		object Nodes
		{ get; set; }
		iLORMember4 MapTo
		{	get; set;	}
		bool ExactMatch
		{	get; set;	}
		int UniverseNumber
		{	get; }
		int DMXAddress
		{	get; }
		string Comment
		{ get; set; }
		int RuleID
		{ get; set; }

	}
}