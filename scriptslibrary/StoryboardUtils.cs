
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System;
using System.Linq;

namespace StorybrewScripts.scriptslibrary
{
	public static class StoryboardUtils 
	{
		public static StoryboardObjectGenerator storyboardWorker;

		public static int scriptStartTime = -3000;

		public static int scriptEndTime;

		public static int scriptStartFadeTime = -2000;
		public static int scriptEndFadeTime = -1500;

		public static int kizunaColumnCount;

		public static void Init(StoryboardObjectGenerator storyboardWorker, Beatmap beatmap) {
            scriptEndTime = (int) beatmap.HitObjects.Last().EndTime + 5000;
			kizunaColumnCount = (int) Math.Floor(beatmap.CircleSize);
			StoryboardUtils.storyboardWorker = storyboardWorker;
		}
		public static void InitBlackBG(int audioDurationSeconds) {
            StoryboardLayer layer = storyboardWorker.GetLayer("Overlay");
			OsbSprite sprite = layer.CreateSprite("sb/utils/px.png", OsbOrigin.Centre);

            sprite.ScaleVec(scriptStartTime, 854, 480);
            sprite.Fade(scriptStartTime, (int)audioDurationSeconds, 1, 1);
		}

		public static void SetBackgroundAsMap(Beatmap beatmap, double opacity) {
            StoryboardLayer layer = storyboardWorker.GetLayer("Overlay");
			OsbSprite sprite = layer.CreateSprite(beatmap.BackgroundPath, OsbOrigin.Centre);

            sprite.Scale(scriptStartTime, 480.0f / storyboardWorker.GetMapsetBitmap(beatmap.BackgroundPath).Height);
            sprite.Fade(scriptStartTime - 500, scriptStartTime, 0, opacity);
            sprite.Fade(scriptEndTime, scriptEndTime + 500, opacity, 0);
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