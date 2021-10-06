using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StorybrewScripts.scriptslibrary;

namespace StorybrewScripts
{
    public class Numberr : StoryboardObjectGenerator
    {
        [Configurable]
        public int ScrollSpeed = 14;

        [Configurable]
        public int HitPosition = 0;

        [Configurable]
        public int ApearsPosition = 640;

        [Configurable]
        public int ColumnWidth = 30;

        [Configurable]
        public int HitObjectWidth = 25;

        [Configurable]
        public double FadeIn = 0.2;

        [Configurable]
        public double Hidden = 0.2;

		public static double OsuPixelsPerSecondPerScrollSpeed = 28583.3;

		public static int ReceptorMargin = -10;

        public override void Generate() {
			StoryboardUtils storyboard = new StoryboardUtils(this, Beatmap);
			storyboard.InitBlackBG();
			storyboard.SetBackgroundAsMap(Beatmap, 1);

			NumberPlayfield numberrPlayfield = new NumberPlayfield(this, storyboard);
			numberrPlayfield.InitBasePlayField();

			int msScrollSpeed = (int) Math.Ceiling(OsuPixelsPerSecondPerScrollSpeed / ScrollSpeed);

            StoryboardLayer notesLayer = GetLayer("HitObject");
            foreach (OsuHitObject hitobject in Beatmap.HitObjects)
            {
				OsbSprite noteSprite = notesLayer.CreateSprite(StoryboardUtils.GetHitObjectImage(hitobject), OsbOrigin.Centre);
                noteSprite.Scale(0, hitobject.StartTime - msScrollSpeed, hitobject.StartTime - msScrollSpeed, getHitObjectWidth(), getHitObjectWidth());
                noteSprite.MoveX(0, hitobject.StartTime - msScrollSpeed, hitobject.StartTime, ApearsPosition - ReceptorMargin, HitPosition + ReceptorMargin);

				numberrPlayfield.ApplyFadeIn(noteSprite, hitobject, FadeIn, msScrollSpeed);
				numberrPlayfield.ApplyHidden(noteSprite, hitobject, Hidden, msScrollSpeed);
			}
		}

		private double _columnWidth = -1;
		public double getColumnWidth() {
			if(_columnWidth == -1) {
				_columnWidth = ColumnWidth / 100.0;
			}
			return _columnWidth;
		}

		private double _hitObjectWidth = -1;
		public double getHitObjectWidth() {
			if(_hitObjectWidth == -1) {
				_hitObjectWidth = HitObjectWidth / 100.0;
			}
			return _hitObjectWidth;
		}
    }	
}
