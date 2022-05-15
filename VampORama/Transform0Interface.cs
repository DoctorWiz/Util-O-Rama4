using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LOR4Utils;
using FileHelper;

namespace UtilORama4
{
	public interface ITransform //: IComparable<iLOR4Member>
	{
		string[] AvailablePluginNames
		{
			get;
		}

		vamps.AlignmentType[] AllowableAlignments
		{
			get;
		}

		vamps.LabelType[] AllowableLabels
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

		//int xTimingsToxLights(xTimings timings, string baseFileName);

		//int xTimingsToLORtimings(xTimings timings, LOR4Sequence sequence);

		//int xTimingsToLORChannels(xTimings timings, LOR4Sequence sequence);

		int ResultsToxTimings(string resultsFile, vamps.AlignmentType alignmentType, vamps.LabelType labelType);

		int xTimingsToxLights(string baseFileName);

		int xTimingsToLORtimings();

		int xTimingsToLORChannels();

	}
}
