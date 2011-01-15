using System;
namespace BowlingGameKata
{
	public class ScoreCalculator
	{
		private int[] throws = new int[21];
		private int ball = 0;
		private int currentThrow;
		private const int NO_OF_PINS_IN_FRAME = 10;
		private const int BONUS_POINTS = 10;

		public void AddThrow (int pins)
		{
			throws[currentThrow++] = pins;
		}		

		public int ScoreForFrame (int theFrame)
		{
			ball = 0;
			int score = 0;
			
			for (int currentFrame = 0; currentFrame < theFrame; currentFrame++) {
				
				if (Strike ()) {
					score += BONUS_POINTS + NextTwoBallsForStrike;
					ball += 1;
				} else if (Spare ()) {
					score += 10 + NextBallForSpare;
					ball += 2;
				} else {
					score += TwoBallsInAFrame;
					ball += 2;
				}
			}
			
			return score;
		}

		private bool Strike ()
		{
			return throws[ball] == NO_OF_PINS_IN_FRAME;
		}

		private bool Spare ()
		{
			return throws[ball] + throws[ball + 1] == NO_OF_PINS_IN_FRAME;
		}

		private int NextTwoBallsForStrike {
			get { return throws[ball + 1] + throws[ball + 2]; }
		}

		private int NextBallForSpare {
			get { return throws[ball + 2]; }
		}

		private int TwoBallsInAFrame {
			get { return throws[ball] + throws[ball + 1]; }
		}
	}
}

