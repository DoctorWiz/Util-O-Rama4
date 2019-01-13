		

		private int XChangeColors(RGBchannel rgbCh)
		{
			int errState = 0;
			int redIndex;  // The Array Index of the Red Channel of the current RGB Channel
			int grnIndex;  // Note: Array Index, NOT 'SavedIndex'
			int bluIndex;
			int redFirstEffectIndex = -1;   // The Array Index of the first Effect in this Red Channel
			int redFinalEffectIndex = -1;     // The Array Index of the first Effect PAST this Red Channel
			int grnFirstEffectIndex = -1;
			int grnFinalEffectIndex = -1;
			int bluFirstEffectIndex = -1;
			int bluFinalEffectIndex = -1;
			int redIntensity; // The Red Intensity, right this very Centisecond
			int grnIntensity;
			int bluIntensity;
			int redEffectIndex = -1;    // the Array Index of the Effect for the Red Channeol for the current Centisecond
			int grnEffectIndex = -1;
			int bluEffectIndex = -1;
			Effect blankEffect = new Effect();  // Used if there is NO Effects for a channel, and also when past the last Effect for the channel
																					//blankEffect.startIntensity = 0;
																					//blankEffect.endIntensity = 0;
			blankEffect.Intensity = 0;
			Effect redEffect = blankEffect;   // Current Red Effect structure for the current Centisecond
			Effect grnEffect = blankEffect;
			Effect bluEffect = blankEffect;
			ColorChange[] changeList = presetSets[cboPresets.SelectedIndex].colorChanges; // Temp, for convenience, a copy of the array of changes to be made
			EffectType redEffectType = EffectType.Intensity;
			EffectType grnEffectType = EffectType.Intensity;
			EffectType bluEffectType = EffectType.Intensity;
			int channelIndex = 0;
			int rgbChannelIndex = 0;
			int effectIndex = 0;
			int redEndIntensity = -1;
			int grnEndIntensity = -1;
			int bluEndIntensity = -1;
			int redEndCentisecond = -1;
			int grnEndCentisecond = -1;
			int bluEndCentisecond = -1;
			int replaceCount = 0;
			int centisecond = 0;
			//int lastEndCenti = -1;
			pixel p = new pixel();
			// Create temp new pixel array which will hold RGB values for each and every centisecond
			pixel[] Keywdels = new pixel[seq.Centiseconds + 2];
			string sMsg = "";
			frmKeywdels fp = new frmKeywdels();
			bool showChanges = false;
			bool copyEffects = false;

			Channel redCh = rgbCh.redChannel;
			Channel grnCh = rgbCh.grnChannel;
			Channel bluCh = rgbCh.bluChannel;




			copyEffects = true;
			if (rgbChannelIndex <= seq.RGBchannels.Count)
			{
				// Is this channel an RGB channel, or a regular one?
				if (seq.RGBchannels[rgbChannelIndex].redChannel.SavedIndex == seq.Channels[channelIndex].SavedIndex ||
						seq.RGBchannels[rgbChannelIndex].grnChannel.SavedIndex == seq.Channels[channelIndex].SavedIndex ||
						seq.RGBchannels[rgbChannelIndex].bluChannel.SavedIndex == seq.Channels[channelIndex].SavedIndex)
				{
					copyEffects = false;
				}
			}
			if (copyEffects)
			{
				// NOT an RGB, this is a regular
				// Are there any Effects for this channel?
				effectIndex = seq.Channels[channelIndex].firstEffectIndex;
				if (effectIndex >= 0)

				{
					while (seq.Effects[effectIndex].channelIndex == channelIndex)
					{
						// Copy them to the NEW array
						Array.Resize(ref NEWeffects, newEffectCount + 1);
						NEWeffects[newEffectCount] = seq.Effects[effectIndex];
						newEffectCount++;
						effectIndex++;
					} // end While Effect
				}
			}
			else
			{
				// This is an RGB Channel
				// Get the Array Index of the Red, Green, and Blue Channels in this RGB Channel
				redIndex = seq.RGBchannels[rgbChannelIndex].redChannel.Index;
				grnIndex = seq.RGBchannels[rgbChannelIndex].grnChannel.Index;
				bluIndex = seq.RGBchannels[rgbChannelIndex].bluChannel.Index;
				// Reset
				redFirstEffectIndex = -1;
				redFinalEffectIndex = -1;
				grnFirstEffectIndex = -1;
				grnFinalEffectIndex = -1;
				bluFirstEffectIndex = -1;
				bluFinalEffectIndex = -1;

				// Loop thru all Effects, looking for the ones for these red, green and blue Channels

				redFirstEffectIndex = seq.Channels[redIndex].firstEffectIndex;
				redEffectIndex = redFirstEffectIndex;
				if (redEffectIndex >= 0)
				{
					redEffect = seq.Effects[redEffectIndex];
					redEffectType = redEffect.EffectType;
					redEndIntensity = redEffect.endIntensity;
					redEndCentisecond = redEffect.endCentisecond;
				}

				grnFirstEffectIndex = seq.Channels[grnIndex].firstEffectIndex;
				grnEffectIndex = grnFirstEffectIndex;
				if (grnEffectIndex >= 0)
				{
					grnEffect = seq.Effects[grnEffectIndex];
					grnEffectType = grnEffect.EffectType;
					grnEndIntensity = grnEffect.endIntensity;
					grnEndCentisecond = grnEffect.endCentisecond;
				}

				bluFirstEffectIndex = seq.Channels[bluIndex].firstEffectIndex;
				bluEffectIndex = bluFirstEffectIndex;
				if (bluEffectIndex >= 0)
				{
					bluEffect = seq.Effects[bluEffectIndex];
					bluEffectType = bluEffect.EffectType;
					bluEndIntensity = bluEffect.endIntensity;
					bluEndCentisecond = bluEffect.endCentisecond;
				}

				// Reset to initial values of BLACK
				redIntensity = 0;
				grnIntensity = 0;
				bluIntensity = 0;

				// Count thru all centiseconds to the end of the sequence
				for (int centiSec = 0; centiSec < seq.Centiseconds; centiSec++)
				{
					// Create a new pixel for this centisecond
					p = new pixel();

					if (redEffectIndex >= 0)
					{
						if (redEffect.channelIndex == redChannel.Index)
						{
							// Check current centisecond against the end of the current Red Effect
							if (redEffect.endCentisecond == centiSec)
							{
								// got it! potential change coming up!

								// check end Intensity (Ramps)
								if (redEffect.endIntensity > -1)
								{
									//redIntensity = redEffect.endIntensity;
									redEndIntensity = redEffect.endIntensity;
								}
								else
								{
									// not a ramp, set Intensity to zero
									redIntensity = 0;
								}
								redEndIntensity = -2;
								redEndCentisecond = -2;
								redEffectType = EffectType.None;

								redEffectIndex++;
								redEffect = seq.Effects[redEffectIndex];
							} // end if endCentisecond

							if (redEffect.startCentisecond == centiSec)
							{
								// current centisecond matches the start centisecond of the current Effect
								if (redEffect.startIntensity > -1)
								{
									redIntensity = redEffect.startIntensity;
								}
								if (redEffect.Intensity > -1)
								{
									redIntensity = redEffect.Intensity;
								}
								redEndIntensity = redEffect.endIntensity;
								redEffectType = redEffect.type;
								redEndIntensity = redEffect.endIntensity;
								redEndCentisecond = redEffect.endCentisecond;
							} // end  if startCentisecond, endcentiSecond
						} // end if channel indexes match
					} // end if we have any red Effects

					//sMsg = "Keywdel[" + centiSec.ToString() + "].redIntensity = " + redIntensity.ToString();
					//Debug.Print(sMsg);

					// Now lets go thru the same thing for the Green
					// See comments above for Red, same thing happening here

					if (grnEffectIndex >= 0)
					{
						if (grnEffect.channelIndex == grnChannel.Index)
						{
							// Check current centisecond against the end of the current grn Effect
							if (grnEffect.endCentisecond == centiSec)
							{
								// got it! potential change coming up!

								// check end Intensity (Ramps)
								if (grnEffect.endIntensity > -1)
								{
									//grnIntensity = grnEffect.endIntensity;
									grnEndIntensity = grnEffect.endIntensity;
								}
								else
								{
									// not a ramp, set Intensity to zero
									grnIntensity = 0;
								}
								grnEndIntensity = -2;
								grnEndCentisecond = -2;
								grnEffectType = EffectType.None;

								grnEffectIndex++;
								grnEffect = seq.Effects[grnEffectIndex];
							}
							if (grnEffect.startCentisecond == centiSec)
							{
								// current centisecond matches the start centisecond of the current Effect
								if (grnEffect.startIntensity > -1)
								{
									grnIntensity = grnEffect.startIntensity;
								}
								if (grnEffect.Intensity > -1)
								{
									grnIntensity = grnEffect.Intensity;
								}
								grnEndIntensity = grnEffect.endIntensity;
								grnEffectType = grnEffect.type;
								grnEndIntensity = grnEffect.endIntensity;
								grnEndCentisecond = grnEffect.endCentisecond;
							} // end startCentisecond, endcentiSecond
						} // end if channel Indexes match
					} // end if we have any green Effects

					// And now finally lets go thru the same thing for the Blue
					// See comments above for grn, same thing happening here

					if (bluEffectIndex >= 0)
					{
						if (bluEffect.channelIndex == bluChannel.Index)
						{
							// Check current centisecond against the end of the current Blue Effect
							if (bluEffect.endCentisecond == centiSec)
							{
								// got it! potential change coming up!

								// check end Intensity (Ramps)
								if (bluEffect.endIntensity > -1)
								{
									//bluIntensity = bluEffect.endIntensity;
									bluEndIntensity = bluEffect.endIntensity;
								}
								else
								{
									// not a ramp, set Intensity to zero
									bluIntensity = 0;
								}
								bluEndIntensity = -2;
								bluEndCentisecond = -2;
								bluEffectType = EffectType.None;

								bluEffectIndex++;
								bluEffect = seq.Effects[bluEffectIndex];
							}
							if (bluEffect.startCentisecond == centiSec)
							{
								// current centisecond matches the start centisecond of the current Effect
								if (bluEffect.startIntensity > -1)
								{
									bluIntensity = bluEffect.startIntensity;
								}
								if (bluEffect.Intensity > -1)
								{
									bluIntensity = bluEffect.Intensity;
								}
								bluEndIntensity = bluEffect.endIntensity;
								bluEffectType = bluEffect.EffectType;
								bluEndIntensity = bluEffect.endIntensity;
								bluEndCentisecond = bluEffect.endCentisecond;
							} // end startCentisecond, endcentiSecond
						} // end if channel indexes match
					} // end if we have any blue Effects

					// Set the red, green, and blue intensities on the current temp pixel
					p.redIntensity = redIntensity;
					p.grnIntensity = grnIntensity;
					p.bluIntensity = bluIntensity;
					p.redEndIntensity = redEndIntensity;
					p.grnEndIntensity = grnEndIntensity;
					p.bluEndIntensity = bluEndIntensity;
					p.redEndCentisecond = redEndCentisecond;
					p.grnEndCentisecond = grnEndCentisecond;
					p.bluEndCentisecond = bluEndCentisecond;
					p.redEffectType = redEffectType;
					p.grnEffectType = grnEffectType;
					p.bluEffectType = bluEffectType;

					// save this pixel for this centisecond in the array
					Keywdels[centiSec] = p;

					//sMsg = "Channel[" + redChannel.Index.ToString() + "] ";
					//sMsg += "Keywdel[" + centiSec.ToString() + "].redIntensity = " + p.redIntensity.ToString();
					//Debug.Print(sMsg);
				}
				Keywdels[seq.Centiseconds] = new pixel();
				Keywdels[seq.Centiseconds + 1] = new pixel();
				// Done building pixel array!!!
				// We now have an array with the RGB values of this rgbChannel for each and every centisecond of the sequence

				if (showChanges)
				{
					fp = new frmKeywdels();
					for (int i = 0; i < seq.Centiseconds; i++)
					{
						fp.setKeywdel(i, 0, Intensity(Keywdels[i].redIntensity), Intensity(Keywdels[i].grnIntensity), Intensity(Keywdels[i].bluIntensity));
					}
				}

				// Now, lets go thru that array, and look for the colors we are supposed to be changing FROM
				for (int centiSec = 0; centiSec < seq.Centiseconds; centiSec++)
				{
					//updateProgress(2, grnChannel.Index, centiSec);
					// Loop thru list of changes
					for (int cg = 0; cg < presetSets[cboPresets.SelectedIndex].changeCount; cg++)
					{
						// compare red
						if (Keywdels[centiSec].redIntensity == changeList[cg].fromR)
						{
							// AND compare green
							if (Keywdels[centiSec].grnIntensity == changeList[cg].fromG)
							{
								// AND compare blue
								if (Keywdels[centiSec].bluIntensity == changeList[cg].fromB)
								{
									// if all 3 colors match the current FROM color
									// change it to the current TO color
									if (cg == 4)
									{ sMsg = "B to O"; }
									Keywdels[centiSec].redIntensity = changeList[cg].toR;
									Keywdels[centiSec].grnIntensity = changeList[cg].toG;
									Keywdels[centiSec].bluIntensity = changeList[cg].toB;
									// force exit of loop thru change list
									// (Since some colors may be SWAPPED and we don't want to change it,
									//   then change it back again!)
									cg = 99;
								} // End blue match
							} // End green match
						} // End Red match
					} // end ChangeList loop
				} // end centisecond loop

				if (showChanges)
				{
					for (int i = 0; i < seq.Centiseconds; i++)
					{
						fp.setKeywdel(i, 1, Intensity(Keywdels[i].redIntensity), Intensity(Keywdels[i].grnIntensity), Intensity(Keywdels[i].bluIntensity));
					}
					fp.ShowDialog();
				}

				// ** BUILD NEW EFFECTS **

				// ** RED STARTS HERE **

				// now create NEW Effects lists for the Red channel
				Effect[] replaceEffs = new Effect[1];
				Effect newE = new Effect();
				replaceCount = 0;
				// Reset to initial values of BLACK
				redIntensity = 0;
				centisecond = 0;
				// Is the Red turned on at the very first centisecond (0)?
				p = Keywdels[0];
				if (p.redIntensity > 0)
				{
					// If New Intensity then Create New Effect
					redIntensity = p.redIntensity;
					//newE.channelIndex = redChannel.Index;
					newE.parent = redChannel;
					//newE.SavedIndex = seq.Channels[redChannel.Index].SavedIndex;
					newE.EffectType = GetBestEffectType(p.redEffectType, p.grnEffectType, p.bluEffectType);
					newE.startCentisecond = 0;
					if (p.redEndIntensity > -1)
					{
						// for fades
						newE.endIntensity = p.redEndIntensity;
						newE.startIntensity = p.redIntensity;
					}
					else
					{
						// for steady
						newE.Intensity = p.redIntensity;
					}

					centisecond = 1;
					// Now continue looking thru pixels, looking for it to change again, thus indicating the end centisecond
					while (centisecond < seq.Centiseconds + 1)
					{
						// get pixel for current centisecond
						p = Keywdels[centisecond];
						// did the anything change?
						if (!p.redEquals(Keywdels[centisecond - 1]))
						{
							// save end centisecond
							newE.endCentisecond = centisecond;
							// add this to the array of replacement Effects, incr counter
							Array.Resize(ref replaceEffs, replaceCount + 1);
							replaceEffs[replaceCount] = newE;
							replaceCount++;
							// get ready to continue for loop above  (4 levels <-) by:
							//    creating a new Effect,
							//       and backing the centisecond up by 1 (in case the next Effect needs to start immediately after this one)
							newE = new Effect();
							centisecond--;
							break; // from while centisecond loop
						}  // end if (p.redIntensity != redIntensity) indicating end centisecond
							 // If we didn't find the change, and thus didn't break out of this while loop
						centisecond++;
					} // end while (centiSec < seq.Centiseconds) loop looking for end centisecond
				} // end if new Intensity > 0
				else
				{
					centisecond = 1;
				}
				// set new Current Intensity
				redIntensity = p.redIntensity;

				// Now, start checking from end of the last Effect, comparing to the previous centisecond
				while (centisecond <= seq.Centiseconds)
				{
					//updateProgress(2, redChannel.Index, centisecond);
					// get pixel for current centisec
					p = Keywdels[centisecond];
					// Did the Intensity change?
					if (!p.redEquals(Keywdels[centisecond - 1]))
					{
						if (p.redIntensity > 0)
						{
							// If New Intensity then Create New Effect
							redIntensity = p.redIntensity;
							newE.channelIndex = redChannel.Index;
							newE.SavedIndex = seq.Channels[redChannel.Index].SavedIndex;
							newE.type = GetBestEffectType(p.redEffectType, p.grnEffectType, p.bluEffectType);
							newE.startCentisecond = centisecond;
							if (p.redEndIntensity > -1)
							{
								// for fades
								newE.endIntensity = p.redEndIntensity;
								newE.startIntensity = p.redIntensity;
							}
							else
							{
								// for steady
								newE.Intensity = p.redIntensity;
							}

							centisecond++;
							// Now continue looking thru pixels, looking for it to change again, thus indicating the end centisecond
							while (centisecond < seq.Centiseconds + 1)
							{
								// get pixel for current centisecond
								p = Keywdels[centisecond];
								// did the Intensity change?
								if (p.redIntensity != redIntensity)
								{
									// save end centisecond
									newE.endCentisecond = centisecond;
									// add this to the array of replacement Effects, incr counter
									Array.Resize(ref replaceEffs, replaceCount + 1);
									replaceEffs[replaceCount] = newE;
									replaceCount++;
									// get ready to continue for loop above  (4 levels <-) by:
									//    creating a new Effect, and backing the centisecond up by 1
									newE = new Effect();
									centisecond--;
									break;
								}  // end if (p.redIntensity != redIntensity) indicating end centisecond
									 // If we didn't find the change, and thus didn't break out of this while loop
								centisecond++;
							} // end while (centiSec < seq.Centiseconds) loop looking for end centisecond
						} // end if new Intensity > 0
							// set new Current Intensity
						redIntensity = p.redIntensity;
					} // end if Intensity changed
					centisecond++;
				} // end for loop thru all centiseconds

				// now REPLACE the Effects list for this Red channel
				for (int fx = 0; fx < replaceCount; fx++)
				{
					// Loop thru Replacement Effects (for this Red channel) and add them to the master list of all new Effects
					//    (including those we copied from the non-rgb Channels, near the start of this procedure)
					Array.Resize(ref NEWeffects, newEffectCount + 1);
					NEWeffects[newEffectCount] = replaceEffs[fx];
					newEffectCount++;
					effectIndex++;
				}
				// END Red

				// ** GREEN STARTS HERE **

				// now create NEW Effects lists for the Greenchannel
				replaceEffs = new Effect[1];
				newE = new Effect();
				replaceCount = 0;
				// Reset to initial values of BLACK
				grnIntensity = 0;
				centisecond = 0;

				// Is the Greenturned on at the very first centisecond (0)?
				p = Keywdels[0];
				if (p.grnIntensity > 0)
				{
					// If New Intensity then Create New Effect
					grnIntensity = p.grnIntensity;
					newE.channelIndex = grnChannel.Index;
					newE.SavedIndex = seq.Channels[grnChannel.Index].SavedIndex;
					newE.type = GetBestEffectType(p.grnEffectType, p.bluEffectType, p.redEffectType);
					newE.startCentisecond = 0;
					if (p.grnEndIntensity > -1)
					{
						// for fades
						newE.endIntensity = p.grnEndIntensity;
						newE.startIntensity = p.grnIntensity;
					}
					else
					{
						// for steady
						newE.Intensity = p.grnIntensity;
					}

					centisecond = 1;
					// Now continue looking thru pixels, looking for it to change again, thus indicating the end centisecond
					while (centisecond < seq.Centiseconds + 1)
					{
						// get pixel for current centisecond
						p = Keywdels[centisecond];
						// did the anything change?
						if (!p.grnEquals(Keywdels[centisecond - 1]))
						{
							// save end centisecond
							newE.endCentisecond = centisecond;
							// add this to the array of replacement Effects, incr counter
							Array.Resize(ref replaceEffs, replaceCount + 1);
							replaceEffs[replaceCount] = newE;
							replaceCount++;
							// get ready to continue for loop above  (4 levels <-) by:
							//    creating a new Effect, and backing the centisecond up by 1
							newE = new Effect();
							centisecond--;
							break;
						}  // end if (p.grnIntensity != grnIntensity) indicating end centisecond
							 // If we didn't find the change, and thus didn't break out of this while loop
						centisecond++;
					} // end while (centiSec < seq.Centiseconds) loop looking for end centisecond
				} // end if new Intensity > 0
				else
				{
					centisecond = 1;
				}
				// set new Current Intensity
				grnIntensity = p.grnIntensity;

				// Now, start checking from end of the last Effect, comparing to the previous centisecond
				while (centisecond <= seq.Centiseconds)
				{
					//updateProgress(2, grnChannel.Index, centisecond);
					// get pixel for current centisec
					p = Keywdels[centisecond];
					// Did the Intensity change?
					if (!p.grnEquals(Keywdels[centisecond - 1]))
					{
						if (p.grnIntensity > 0)
						{
							// If New Intensity then Create New Effect
							grnIntensity = p.grnIntensity;
							newE.channelIndex = grnChannel.Index;
							newE.SavedIndex = seq.Channels[grnChannel.Index].SavedIndex;
							newE.type = GetBestEffectType(p.grnEffectType, p.bluEffectType, p.redEffectType);
							newE.startCentisecond = centisecond;
							if (p.grnEndIntensity > -1)
							{
								// for fades
								newE.endIntensity = p.grnEndIntensity;
								newE.startIntensity = p.grnIntensity;
							}
							else
							{
								// for steady
								newE.Intensity = p.grnIntensity;
							}

							centisecond++;
							// Now continue looking thru pixels, looking for it to change again, thus indicating the end centisecond
							while (centisecond < seq.Centiseconds + 1)
							{
								// get pixel for current centisecond
								p = Keywdels[centisecond];
								// did the Intensity change?
								if (p.grnIntensity != grnIntensity)
								{
									// save end centisecond
									newE.endCentisecond = centisecond;
									// add this to the array of replacement Effects, incr counter
									Array.Resize(ref replaceEffs, replaceCount + 1);
									replaceEffs[replaceCount] = newE;
									replaceCount++;
									// get ready to continue for loop above  (4 levels <-) by:
									//    creating a new Effect, and backing the centisecond up by 1
									newE = new Effect();
									centisecond--;
									break;
								}  // end if (p.grnIntensity != grnIntensity) indicating end centisecond
									 // If we didn't find the change, and thus didn't break out of this while loop
								centisecond++;
							} // end while (centiSec < seq.Centiseconds) loop looking for end centisecond
						} // end if new Intensity > 0
							// set new Current Intensity
						grnIntensity = p.grnIntensity;
					} // end if Intensity changed
					centisecond++;
				} // end for loop thru all centiseconds

				// now REPLACE the Effects list for this Greenchannel
				for (int fx = 0; fx < replaceCount; fx++)
				{
					// Loop thru Replacement Effects (for this Greenchannel) and add them to the master list of all new Effects
					//    (including those we copied from the non-rgb Channels, near the start of this procedure)
					Array.Resize(ref NEWeffects, newEffectCount + 1);
					NEWeffects[newEffectCount] = replaceEffs[fx];
					newEffectCount++;
					effectIndex++;
				}
				// END GREEN

				// ** BLUE STARTS HERE **
				replaceEffs = new Effect[1];
				newE = new Effect();
				replaceCount = 0;
				// Reset to initial values of BLACK
				bluIntensity = 0;
				centisecond = 0;
				// Is the Blue turned on at the very first centisecond (0)?
				p = Keywdels[0];
				if (p.bluIntensity > 0)
				{
					// If New Intensity then Create New Effect
					bluIntensity = p.bluIntensity;
					newE.channelIndex = bluChannel.Index;
					newE.SavedIndex = seq.Channels[bluChannel.Index].SavedIndex;
					newE.type = GetBestEffectType(p.bluEffectType, p.grnEffectType, p.bluEffectType);
					newE.startCentisecond = 0;
					if (p.bluEndIntensity > -1)
					{
						// for fades
						newE.endIntensity = p.bluEndIntensity;
						newE.startIntensity = p.bluIntensity;
					}
					else
					{
						// for steady
						newE.Intensity = p.bluIntensity;
					}

					centisecond = 1;
					// Now continue looking thru pixels, looking for it to change again, thus indicating the end centisecond
					while (centisecond < seq.Centiseconds + 1)
					{
						// get pixel for current centisecond
						p = Keywdels[centisecond];
						// did the anything change?
						if (!p.bluEquals(Keywdels[centisecond - 1]))
						{
							// save end centisecond
							newE.endCentisecond = centisecond;
							// add this to the array of replacement Effects, incr counter
							Array.Resize(ref replaceEffs, replaceCount + 1);
							replaceEffs[replaceCount] = newE;
							replaceCount++;
							// get ready to continue for loop above  (4 levels <-) by:
							//    creating a new Effect,
							//       and backing the centisecond up by 1 (in case the next Effect needs to start immediately after this one)
							newE = new Effect();
							centisecond--;
							break; // from while centisecond loop
						}  // end if (p.bluIntensity != bluIntensity) indicating end centisecond
							 // If we didn't find the change, and thus didn't break out of this while loop
						centisecond++;
					} // end while (centiSec < seq.Centiseconds) loop looking for end centisecond
				} // end if new Intensity > 0
				else
				{
					centisecond = 1;
				}
				// set new Current Intensity
				bluIntensity = p.bluIntensity;

				// Now, start checking from end of the last Effect, comparing to the previous centisecond
				while (centisecond <= seq.Centiseconds)
				{
					//updateProgress(2, bluChannel.Index, centisecond);
					// get pixel for current centisec
					p = Keywdels[centisecond];
					// Did the Intensity change?
					if (!p.bluEquals(Keywdels[centisecond - 1]))
					{
						if (p.bluIntensity > 0)
						{
							// If New Intensity then Create New Effect
							bluIntensity = p.bluIntensity;
							newE.channelIndex = bluChannel.Index;
							newE.SavedIndex = seq.Channels[bluChannel.Index].SavedIndex;
							newE.type = GetBestEffectType(p.bluEffectType, p.grnEffectType, p.bluEffectType);
							newE.startCentisecond = centisecond;
							if (p.bluEndIntensity > -1)
							{
								// for fades
								newE.endIntensity = p.bluEndIntensity;
								newE.startIntensity = p.bluIntensity;
							}
							else
							{
								// for steady
								newE.Intensity = p.bluIntensity;
							}

							centisecond++;
							// Now continue looking thru pixels, looking for it to change again, thus indicating the end centisecond
							while (centisecond < seq.Centiseconds + 1)
							{
								// get pixel for current centisecond
								p = Keywdels[centisecond];
								// did the Intensity change?
								if (p.bluIntensity != bluIntensity)
								{
									// save end centisecond
									newE.endCentisecond = centisecond;
									// add this to the array of replacement Effects, incr counter
									Array.Resize(ref replaceEffs, replaceCount + 1);
									replaceEffs[replaceCount] = newE;
									replaceCount++;
									// get ready to continue for loop above  (4 levels <-) by:
									//    creating a new Effect, and backing the centisecond up by 1
									newE = new Effect();
									centisecond--;
									break;
								}  // end if (p.bluIntensity != bluIntensity) indicating end centisecond
									 // If we didn't find the change, and thus didn't break out of this while loop
								centisecond++;
							} // end while (centiSec < seq.Centiseconds) loop looking for end centisecond
						} // end if new Intensity > 0
							// set new Current Intensity
						bluIntensity = p.bluIntensity;
					} // end if Intensity changed
					centisecond++;
				} // end for loop thru all centiseconds

				// now REPLACE the Effects list for this Blue channel
				for (int fx = 0; fx < replaceCount; fx++)
				{
					// Loop thru Replacement Effects (for this Blue channel) and add them to the master list of all new Effects
					//    (including those we copied from the non-rgb Channels, near the start of this procedure)
					Array.Resize(ref NEWeffects, newEffectCount + 1);
					NEWeffects[newEffectCount] = replaceEffs[fx];
					newEffectCount++;
					effectIndex++;
				}
				// END BLUE

				channelIndex++;  // Skip next channel which will be the green one of this rgbChannel, we've already done it!
				channelIndex++;  // Likewise, skip again, which whill be the blue one, it's done also

				rgbChannelIndex++;
				effectIndex++;
			} // End rgbChannel loop

			if (bluFinalEffectIndex > 0)
			{
				// pick up where we left off
				effectIndex = bluFinalEffectIndex;
			}
			else if (grnFinalEffectIndex > 0)
			{
				effectIndex = grnFinalEffectIndex;
			}
			else if (redFinalEffectIndex > 0)
			{
				effectIndex = redFinalEffectIndex;
			}
			updateProgress(channelIndex, 0);
			channelIndex++;
			effectIndex++;

			Array.Resize(ref NEWeffects, newEffectCount + 1);
			NEWeffects[newEffectCount] = new Effect();

			seq.Effects = NEWeffects;
			seq.effectCount = newEffectCount;
			changesMade = true;

			return errState;
		}  // end ChangeColors

		private void btnOK_Click(object sender, EventArgs e)
		{
			btnReColor.Enabled = false;
			grpFile.Enabled = false;
			grpColors.Enabled = false;

			string theFile = dlgFileOpen.InitialDirectory + "\\" + txtInputFilename.Text;

			seq = new Sequence4();
			seq.ReadSequenceFile(lastFile);

			string sMsg;
			sMsg = "File Parse Complete!\r\n\r\n";
			sMsg += seq.lineCount.ToString() + " lines\r\n";
			sMsg += seq.Channels.Count.ToString() + " Channels\r\n";
			sMsg += seq.RGBchannels.Count.ToString() + " RGB Channels\r\n";
			//sMsg += seq.effectCount.ToString() + " Effects\r\n";
			sMsg += seq.ChannelGroups.Count.ToString() + " groups\r\n";
			//sMsg += seq.groupItemCount.ToString() + " group items";

			DialogResult mReturn;
			//mReturn = MessageBox.Show(sMsg, "File Parse Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

			ChangeColors();

			string basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Light-O-Rama\\Sequences\\";
			//string newFile = basePath + "!CHANGED " + txtFilename.Text;
			string newFile = Path.GetDirectoryName(lastFile) + "\\" + "!CHANGED " + Path.GetFileName(lastFile);

			//seq.WriteFile(newFile);
			seq.WriteSequenceFile_DisplayOrder(newFile);

			grpColors.Enabled = true;
			grpFile.Enabled = true;
			btnReColor.Enabled = true;

			System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"c:\windows\Media\chimes.wav");
			player.Play();
		} // end parseFile


		#endregion
		private void ReadInputFile(string filename)
		{
			Sequence4 seq = new Sequence4();
			seq.ReadClipboardFile(filename);
		}

		private void WriteOutputFile(string filename)
		{
			seq.WriteClipboardFile(filename);
		}

		private int SavePresets()
		{
			int errStatus = 0;

			string basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Light-O-Rama\\Sequences\\";
			string fileName = basePath + "ColorChanger Presets.xml";

			StreamWriter writer = new StreamWriter(fileName);
			string lineOut; // line read in (does not get modified)

			lineOut = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?>";
			writer.WriteLine(lineOut);

			lineOut = "<" + TABLEpresetSet + "s>";
			writer.WriteLine(lineOut);

			for (int i = 0; i < presetSetCount; i++)
			{
				if (presetSets[i].changeCount > 0)
				{
					lineOut = "  <" + TABLEpresetSet;
					lineOut += SPC + FIELDname + FIELDEQ + presetSets[i].name + ENDQT + "/>";
					writer.WriteLine(lineOut);
					lineOut = "    <" + TABLEcolorChange + "s>";
					writer.WriteLine(lineOut);

					for (int j = 0; j < presetSets[i].changeCount; j++)
					{
						lineOut = "      <" + TABLEcolorChange;
						lineOut += SPC + FIELDfromName + FIELDEQ + presetSets[i].colorChanges[j].fromName + ENDQT;
						lineOut += SPC + FIELDfromR + FIELDEQ + presetSets[i].colorChanges[j].fromR.ToString() + ENDQT;
						lineOut += SPC + FIELDfromG + FIELDEQ + presetSets[i].colorChanges[j].fromG.ToString() + ENDQT;
						lineOut += SPC + FIELDfromB + FIELDEQ + presetSets[i].colorChanges[j].fromB.ToString() + ENDQT;
						lineOut += SPC + FIELDtoName + FIELDEQ + presetSets[i].colorChanges[j].toName + ENDQT;
						lineOut += SPC + FIELDtoR + FIELDEQ + presetSets[i].colorChanges[j].toR.ToString() + ENDQT;
						lineOut += SPC + FIELDtoG + FIELDEQ + presetSets[i].colorChanges[j].toG.ToString() + ENDQT;
						lineOut += SPC + FIELDtoB + FIELDEQ + presetSets[i].colorChanges[j].toB.ToString() + ENDQT;
						lineOut += "/>";
						writer.WriteLine(lineOut);
					}

					lineOut = "    </" + TABLEcolorChange + "s>";
					writer.WriteLine(lineOut);
					lineOut = "  </" + TABLEpresetSet + ">";
					writer.WriteLine(lineOut);
				}
			}

			lineOut = "</" + TABLEpresetSet + "s>";
			writer.WriteLine(lineOut);

			return errStatus;
		}

		private int LoadPresets()
		{
			int errStatus = 0;

			string basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Light-O-Rama 2015\\Sequences\\";
			string fileName = basePath + "ColorChanger Presets.xml";

			StreamReader reader = new StreamReader(fileName);
			string lineIn; // line read in (does not get modified)
			int pos1 = -1; // positions of certain key text in the line
			int lineCount = 0;

			// Zero these out from any previous run
			presetSetCount = 0;
			colorChangeCount = 0;

			int curPreset = -1;
			int curChange = -1;

			ColorChange[] colorChanges = new ColorChange[1];

			// * PASS 1 - COUNT OBJECTS
			while ((lineIn = reader.ReadLine()) != null)
			{
				lineCount++;
				// does this line mark the start of a preset set?
				pos1 = lineIn.IndexOf("<presetSet name=");
				if (pos1 > 0)
				{
					presetSetCount++;
				}
				pos1 = lineIn.IndexOf("<colorChange fromName=");
				if (pos1 > 0)
				{
					colorChangeCount++;
				}
			}
			reader.Close();

			// CREATE ARRAYS TO HOLD OBJECTS
			presetSets = new PresetSet[presetSetCount];

			// * PASS 2 - COLLECT OBJECTS
			reader = new StreamReader(fileName);
			while ((lineIn = reader.ReadLine()) != null)
			{
				lineCount++;
				// does this line mark the start of a preset set?
				pos1 = lineIn.IndexOf(TABLEpresetSet + " " + FIELDname);
				if (pos1 > 0)
				{
					curPreset++;
					PresetSet ps = new PresetSet();
					ps.name = utils.getKeyWord(lineIn, FIELDname);
					presetSets[curPreset] = ps;

					lineIn = reader.ReadLine();
					lineIn = reader.ReadLine();
					curChange = -1;
					pos1 = lineIn.IndexOf(TABLEcolorChange + " " + FIELDfromName);
					while (pos1 > 0)
					{
						curChange++;
						Array.Resize(ref colorChanges, curChange + 1);
						ColorChange cc = new ColorChange();
						cc.fromName = utils.getKeyWord(lineIn, FIELDfromName);
						cc.fromR = utils.getKeyValue(lineIn, FIELDfromR);
						cc.fromG = utils.getKeyValue(lineIn, FIELDfromG);
						cc.fromB = utils.getKeyValue(lineIn, FIELDfromB);
						cc.toName = utils.getKeyWord(lineIn, FIELDtoName);
						cc.toR = utils.getKeyValue(lineIn, FIELDtoR);
						cc.toG = utils.getKeyValue(lineIn, FIELDtoG);
						cc.toB = utils.getKeyValue(lineIn, FIELDtoB);
						colorChanges[curChange] = cc;

						lineIn = reader.ReadLine();
						pos1 = lineIn.IndexOf(TABLEcolorChange + " " + FIELDfromName);
					}
					if (curChange > -1)
					{
						ps.colorChanges = colorChanges;
						ps.changeCount = curChange + 1;
						presetSets[curPreset] = ps;
					}
				}
			}

			reader.Close();

			
	string sMsg;
	sMsg = "File Parse Complete!\r\n\r\n";
	sMsg += lineCount.ToString() + " lines\r\n";
	sMsg += seq.Channels.Count.ToString() + " Channels\r\n";
	sMsg += RGBchannels.Count.ToString() + " RGB Channels\r\n";
	sMsg += seq.effectCount.ToString() + " Effects\r\n";
	sMsg += groupCount.ToString() + " groups\r\n";
	sMsg += groupItemCount.ToString() + " group items";

	DialogResult mReturn;
	mReturn = MessageBox.Show(sMsg, "File Parse Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
	

			return errStatus;
		}
