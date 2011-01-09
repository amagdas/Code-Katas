using System;
namespace BowlingGameKata
{
	public class Game
	{
		private int score;
		private int[] throws = new int[21];
		private int currentThrow;
		private int currentFrame = 1;
		private bool isFirstThrow = true;

		public int Score {
			get { return ScoreForFrame (CurrentFrame - 1); }
		}

		public int CurrentFrame {
			get { return currentFrame; }
		}

		public void Add (int pins)
		{
			throws[currentThrow++] = pins;
			score += pins;
			
			AdjustCurrentFrame (pins);
		}

		void AdjustCurrentFrame (int pins)
		{
			if (isFirstThrow) {
				if (pins == 10)
					//strike
					currentFrame++;
				else
					isFirstThrow = false;
			} else {
				isFirstThrow = true;
				currentFrame++;
			}
			
			if(currentFrame > 11)
				currentFrame = 11;
		}

		public int ScoreForFrame (int theFrame)
		{
			int score = 0;
			int ball = 0;
			for (int currentFrame = 0; currentFrame < theFrame; currentFrame++) {
				int firstThrow = throws[ball++];
				
				if (firstThrow == 10) {
					score += 10 + throws[ball] + throws[ball+1];
				} else {
					int secondThrow = throws[ball++];
					
					int frameScore = firstThrow + secondThrow;
					
					if (frameScore == 10) {
						score += frameScore + throws[ball];
					} else
						score += frameScore;
				}
			}
			
			return score;
		}
	}
}

