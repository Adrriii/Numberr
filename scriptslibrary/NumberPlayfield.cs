
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using OpenTK;

namespace StorybrewScripts.scriptslibrary
{
	public static class NumberPlayfield 
	{
		public static string Receptor = "sb/number/receptor.png";
		public static string Playfield = "sb/number/playfield.png";

		public static Numberr Instance;

		public static void Init(Numberr instance) {
			Instance = instance;
		}

		public static void InitBasePlayField() {
			NumberPlayfield.DisplayPlayFieldElement(NumberPlayfield.Playfield, 320, 240, OsbOrigin.Centre, false, 1);
			NumberPlayfield.DisplayPlayFieldElement(NumberPlayfield.Receptor, Instance.HitPosition, 240, OsbOrigin.CentreRight, false, Instance.getColumnWidth());
			NumberPlayfield.DisplayPlayFieldElement(NumberPlayfield.Receptor, Instance.ApearsPosition, 240, OsbOrigin.CentreLeft, true, Instance.getColumnWidth());
		}

		public static void DisplayPlayFieldElement(string sprite, int X, int Y, OsbOrigin origin, bool flip = false, double scale = 1) {
            var SendorInstance = Instance.GetLayer("Receptor").CreateSprite(sprite, origin, new Vector2(X, Y));
			if(flip) {
				SendorInstance.FlipH(StoryboardUtils.scriptStartTime, StoryboardUtils.scriptEndTime);
			}
            SendorInstance.Scale(0, StoryboardUtils.scriptStartTime, StoryboardUtils.scriptEndTime, scale, scale);
			SendorInstance.Fade(OsbEasing.In, StoryboardUtils.scriptStartFadeTime, StoryboardUtils.scriptEndFadeTime, 0, 1);
		}
	}
}