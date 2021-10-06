
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using OpenTK;
using System;

namespace StorybrewScripts.scriptslibrary
{
	public class NumberPlayfield 
	{
		public static string Receptor = "sb/number/receptor.png";
		public static string Playfield = "sb/number/playfield.png";
		public static double FadeInPercentageStop = 0.5;
		public static double HiddenPercentageBegin = 0.5;

		public Numberr instance;
		public StoryboardUtils storyboardUtils;

		public NumberPlayfield(Numberr instanceNumberr, StoryboardUtils storyboard) {
			instance = instanceNumberr;
			storyboardUtils = storyboard;
		}

		public void InitBasePlayField() {
			DisplayPlayFieldElement(Playfield, 320, 240, OsbOrigin.Centre, false, 1);
			DisplayPlayFieldElement(Receptor, instance.HitPosition, 240, OsbOrigin.CentreRight, false, instance.getColumnWidth());
			DisplayPlayFieldElement(Receptor, instance.ApearsPosition, 240, OsbOrigin.CentreLeft, true, instance.getColumnWidth());
		}

		public void DisplayPlayFieldElement(string sprite, int X, int Y, OsbOrigin origin, bool flip = false, double scale = 1) {
            OsbSprite fieldSprite = instance.GetLayer("Receptor").CreateSprite(sprite, origin, new Vector2(X, Y));
			if(flip) {
				fieldSprite.FlipH(storyboardUtils.scriptStartTime, storyboardUtils.scriptEndTimeQuit);
			}
            fieldSprite.Scale(0, storyboardUtils.scriptStartTime, storyboardUtils.scriptEndTime, scale, scale);
			fieldSprite.Fade(OsbEasing.In, storyboardUtils.scriptStartFadeTime, storyboardUtils.scriptEndFadeTime, 0, 1);
			fieldSprite.Fade(OsbEasing.In, storyboardUtils.scriptEndTime - storyboardUtils.scriptEndFadeTime, storyboardUtils.scriptEndTime - storyboardUtils.scriptStartFadeTime, 1, 0);
		}

		public void ApplyFadeIn(OsbSprite noteSprite, OsuHitObject hitobject, double fadeIn, int msScrollSpeed) {
			if(fadeIn != 0) {
				double fadeStop = FadeInPercentageStop * fadeIn;
				int fadeLength = (int) Math.Floor(msScrollSpeed * fadeStop);
				int fadeStartDistance = msScrollSpeed - fadeLength;

				noteSprite.Fade(OsbEasing.In, hitobject.StartTime - fadeStartDistance, hitobject.StartTime - msScrollSpeed * FadeInPercentageStop, 0, 1);
			}
		}

		public void ApplyHidden(OsbSprite noteSprite, OsuHitObject hitobject, double fadeOut, int msScrollSpeed) {
			if(fadeOut != 0) {
				int fadeStartDistance = (int) Math.Ceiling(msScrollSpeed * HiddenPercentageBegin);
				int fadeLength = (int) Math.Ceiling((msScrollSpeed * HiddenPercentageBegin) * fadeOut);

				noteSprite.Fade(OsbEasing.In, hitobject.StartTime - fadeStartDistance, hitobject.StartTime - fadeLength, 1, 0);
			}
		}
	}
}