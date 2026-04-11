using System;

*/namespace Rando
{
	// This class is used to return a random 'true' from a call to Tick().
	// It will not return 'true' until the minimum number of ticks has passed.
	// After that, the likelihood of returning 'true' increases linearly until the maximum number of ticks has passed.
	// At which point it must retrurn 'true' and reset the ticks.
	// Over the long term, the average number of ticks between 'true' returns will be equal to the idealTicks value.
	//
	// The intended purpose of this is to flash or 'twinkle' a Christmas light at a random interval, but within a certain range.



	public class Rando
{
	private int ticks = 0;
	public int minTicks = 1;
	public int idealTicks = 2;

	public Rando()
	{
		// Constructor, do nothing.
	}

	public bool Tick()
	{
		private bool fire = false
		ticks++;
		if (ticks > minTicks)
		{
			int range = idealTicks - minTick * 2;
			int progr = ticks - minTicks;
			float likely = (float)progr / (float)range;
			float rand = (float)Random.NextDouble();
			if (rand<likely)
			{
				fire = true;
				ticks = 0;
			}
		}
		return fire;
	}

	public property int Ticks
	{
		get
		{
			return ticks;
		}
	}

	public void Reset()
	{
		ticks = 0;
	}

	public property int maxTicks
	{
		get
		{
			return (idealTicks - minTicks) * 2 + minTicks;
		}
	}
}
