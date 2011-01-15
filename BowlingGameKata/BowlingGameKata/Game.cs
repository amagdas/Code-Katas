using System;
namespace BowlingGameKata
{
	public class Game
	{
		private int currentFrame = 1;
		private bool isFirstThrow = true;
		private ScoreCalculator scoreCalculator = new ScoreCalculator ();
		private const int LAST_FRAME_IN_GAME = 10;


		public int Score {
			get { return ScoreForFrame (currentFrame); }
		}

		public void Add (int pins)
		{
			scoreCalculator.AddThrow (pins);
			AdjustCurrentFrame (pins);
		}

		void AdjustCurrentFrame (int pins)
		{
			if (LastBallInFrame (pins)) {
				AdvanceFrame ();
			} else
				isFirstThrow = false;
		}

		private bool LastBallInFrame (int pins)
		{
			return Strike (pins) || (!isFirstThrow);
		}

		private bool Strike (int pins)
		{
			return (isFirstThrow && pins == 10);
		}		

		private void AdvanceFrame ()
		{
			currentFrame++;
			
			if (currentFrame > LAST_FRAME_IN_GAME)
				currentFrame = LAST_FRAME_IN_GAME;
		}

		public int ScoreForFrame (int frame)
		{
			return scoreCalculator.ScoreForFrame (frame);
		}
	}
}

