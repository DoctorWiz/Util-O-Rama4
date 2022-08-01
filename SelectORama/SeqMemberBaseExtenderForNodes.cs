#define NODESUPPORT

using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using Syncfusion.Windows.Forms.Tools;


namespace LOR4
{
	/// <summary>
	/// Extension to the Member Base Class for use with ListViews (for Timing Grids)
	/// and for Syncfusion's TreeViewAdv for tracks, channels, groups, etc.
	/// </summary>

	// Extension to the interface
	public partial interface iLOR4Member : IComparable<iLOR4Member>
	{
		List<TreeNodeAdv> Nodes
		{ get; set; }

		ListViewItem ListViewItem
		{ get; set; }
	}

	public delegate void SelectedChanged();


	// Extension to the base class
	public abstract partial class LOR4MemberBase : iLOR4Member, IComparable<iLOR4Member>
	{
		// Holds a List<TreeNodeAdv> for SyncFusion's TreeViewAdv in projects that use it.
		protected List<TreeNodeAdv> myNodes = new List<TreeNodeAdv>();
		// Holds a ListViewItem for Timing Grids in projects that use it.
		protected ListViewItem myListViewItem = new ListViewItem("");

		public virtual List<TreeNodeAdv> Nodes
		{
			get { return myNodes; }
			set
			{
				if ((MemberType == LOR4MemberType.Sequence) ||
						(MemberType == LOR4MemberType.Visualization) ||
						(MemberType == LOR4MemberType.Timings))
				{
					throw new NotSupportedException("Nodes should not be assigned to " + LOR4SeqEnums.MemberTypeName(MemberType) + " for member " + Name + ".");
				}
				else
				{
					myNodes = value;
				}
			}
		}

		public virtual ListViewItem ListViewItem
		{
			get { return myListViewItem; }
			set
			{
				if (MemberType == LOR4MemberType.Timings)
				{
					myListViewItem = value;
				}
				else
				{
					throw new NotSupportedException("ListViewItems should only be assigned to Timing Grids.");
				}
			}
		}

		public event SelectedChanged SelectedChanged;

#if NODESUPPORT
		public virtual CheckState Selected
		{
			get { return myCheckState; }
			set
			{
				myCheckState = value;
				if (Nodes != null)
				{
					for (int n = 0; n < Nodes.Count; n++)
					{
						Nodes[n].CheckState = value;
					}
				}
			}
		}
#endif
		File
	}
}