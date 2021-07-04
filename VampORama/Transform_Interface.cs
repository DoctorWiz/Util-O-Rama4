using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LORUtils;

namespace VampORama
{
	public interface ITransform //: IComparable<IMember>
	{
		string[] AvailablePluginNames
		{
			get;
		}

		vamps.AlignmentType[] AllowableAlignments
		{
			get;
		}

		vamps.LabelTypes[] AllowableLabels
		{
			get;
		}

		xTimings Timings
		{
			get;
		}

		int TransformationType
		{
			get;
		}

		string TransformationName
		{
			get;
		}

		//string AnnotateSong(string songFile, int pluginIndex);

		int xTimingsToxLights(xTimings timings, string baseFileName);

		int xTimingsToLORtimings(xTimings timings, Sequence4 sequence);

		int xTimingsToLORChannels(xTimings timings, Sequence4 sequence);

	}
}
