using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelRemapper
{
	public enum xRefMode { Normal, FastAdd };
	public enum xRefNoFindAction { IgnoreNoErrorRetNeg1, RaiseError };
	public enum xRefUpdateNoFindAction { IgnoreNoErrorNoAdd, AddNew, AddWithError, RaiseErrorNoAdd }
	public enum xRefRemoveNoFindAction { IgnoreNoError, RaiseError };
	public enum xRefAddDuplicateAction { IgnoreNoChange, UpdateWithNoError, RaiseError };
	public enum xRefUpdateDuplicateAction { IgnoreNoChange, RaiseError }

	class xRef
	{
		private int count;
		private int[] AList;
		private int[] BList;
		private int[] AxRef;
		private int[] BxRef;

		private xRefMode mode = xRefMode.Normal;
		private xRefNoFindAction nofindAction = xRefNoFindAction.IgnoreNoErrorRetNeg1;
		private xRefUpdateNoFindAction updateNoFindAction = xRefUpdateNoFindAction.IgnoreNoErrorNoAdd;
		private xRefRemoveNoFindAction removeNoFindAction = xRefRemoveNoFindAction.IgnoreNoError;
		private xRefAddDuplicateAction addDuplicateAction = xRefAddDuplicateAction.IgnoreNoChange;
		private xRefUpdateDuplicateAction updateDuplicateAction = xRefUpdateDuplicateAction.IgnoreNoChange;

		public int Count
		{
			get
			{
				return count;
			}
		}

		public xRefMode Mode
		{
			get
			{
				return mode;
			}
			set
			{
				if ((mode == xRefMode.FastAdd) && (value == xRefMode.Normal))
				{
					//Switching back to Normal mode after FastAdd
					reindex();
				}
				mode = value;
			}
		}

		public xRefNoFindAction NofindAction
		{
			get
			{
				return NofindAction;
			}
			set
			{
				NofindAction = value;
			}
		}

		public xRefUpdateNoFindAction UpdateNoFindAction
		{
			get
			{
				return updateNoFindAction;
			}
			set
			{
				updateNoFindAction = value;
			}
		}

		public xRefRemoveNoFindAction RemoveNoFindAction
		{
			get
			{
				return removeNoFindAction;
			}
			set
			{
				removeNoFindAction = value;
			}
		}

		public xRefAddDuplicateAction AddDuplicateAction
		{
			get
			{
				return addDuplicateAction;
			}
			set
			{
				addDuplicateAction = value;
			}
		}
			
		public xRefUpdateDuplicateAction UpdateDuplicateAction
		{
			get
			{
				return updateDuplicateAction;
			}
			set
			{
				updateDuplicateAction = value;
			}
		}

		public void Clear()
		{
			count = 0;
			//TODO: What else?
		}

		public void Add(int A, int B)
		{

		}

		public int xRefA(int A)
		{
			int B = -1;

			return B;
		}

		public int xRefB(int B)
		{
			int A = -1;

			return A;
		}

		public int UpdateA(int A, int B)
		{
			int oldB = -1;

			return oldB;
		}

		public int UpdateB(int A, int B)
		{
			int oldA = -1;

			return oldA;
		}

		public void RemoveA(int A)
		{

		}

		public void RemoveB(int B)
		{

		}

		public bool AExists(int A)
		{
			bool found = false;

			return found;
		}

		public bool BExists(int B)
		{
			bool found = false;

			return found;
		}

		private int findAIndex(int A)
		{
			int index = -1;

			return index;
		}

		private int findBIndex(int B)
		{
			int index = -1;

			return index;
		}

		private void reindex()
		{
			// QuickSort

		}
	}
}
