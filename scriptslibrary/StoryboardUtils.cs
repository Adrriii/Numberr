
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System;
using System.Linq;

namespace StorybrewScripts.scriptslibrary
{
	public class StoryboardUtils 
	{
		public StoryboardObjectGenerator storyboardWorker;

		public int scriptStartTime = -4000;

		public int scriptEndTime;
		public int scriptEndTimeQuit;

		public int scriptStartFadeTime = -3500;

		public int scriptEndFadeTime = -3000;

		public static int kizunaColumnCount;

		public StoryboardUtils(StoryboardObjectGenerator storyboard, Beatmap beatmap) {
            scriptEndTime = (int) beatmap.HitObjects.Last().EndTime;
			scriptEndTimeQuit = scriptEndTime - scriptStartTime;
			kizunaColumnCount = (int) Math.Floor(beatmap.CircleSize);
			storyboardWorker = storyboard;
		}
		public void InitBlackBG() {
            StoryboardLayer layer = storyboardWorker.GetLayer("Overlay");
			OsbSprite sprite = layer.CreateSprite("sb/utils/px.png", OsbOrigin.Centre);

            sprite.ScaleVec(scriptStartTime, 854, 480);
            sprite.Fade(scriptStartTime, scriptEndTimeQuit, 1, 1);
		}

		public void SetBackgroundAsMap(Beatmap beatmap, double opacity) {
            StoryboardLayer layer = storyboardWorker.GetLayer("Overlay");
			OsbSprite sprite = layer.CreateSprite(beatmap.BackgroundPath, OsbOrigin.Centre);

            sprite.Scale(scriptStartTime, 480.0f / storyboardWorker.GetMapsetBitmap(beatmap.BackgroundPath).Height);
            sprite.Fade(scriptStartTime - 500, scriptStartTime, 0, opacity);
            sprite.Fade(scriptEndTimeQuit, scriptEndTimeQuit, opacity, 0);
		}

		public static int GetColumnFromHitObjectX(OsuHitObject hitabject) {
        	double hitobjectX = hitabject.PositionAtTime(hitabject.StartTime).X;
            double HitObjectLaneX = MathUtils.Clamp((hitobjectX / (512d / kizunaColumnCount)), 0, kizunaColumnCount);

			return (int) Math.Floor(HitObjectLaneX);
		}

		public static string GetHitObjectImage(OsuHitObject hitabject) {
			return "sb/number/"+GetColumnFromHitObjectX(hitabject)+".png";
		}
	}
}