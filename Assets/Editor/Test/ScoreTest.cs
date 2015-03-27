using NUnit.Framework;

[TestFixture]
public class ScoreTest {

	private Score score = new Score();

	private void SetScoreProperties (float time, float min, float max)	{
		score.SetTime (time);
		score.SetMin (min);
		score.SetMax (max);
	}

	[Test]
	public void CanISetTimeToAScore()	{
		score.SetTime (0f);
	}

	[Test]
	public void CanISetMinValueToAScore()	{
		score.SetMin (0f);
	}

	[Test]
	public void CanISetMaxValueToAScore()	{
		score.SetMax (0f);
	}

	[Test]
	public void GetFirstScore()	{
		SetScoreProperties (0, 1, 2);
		Assert.AreEqual (1, score.CalculateScore());
		
		SetScoreProperties (1, 1, 2);
		Assert.AreEqual (1, score.CalculateScore());
		
		SetScoreProperties (1.33f, 1, 2);	
		Assert.AreEqual (1, score.CalculateScore());
	}

	[Test]
	public void GetSecondScore()	{
		SetScoreProperties (1.34f, 1, 2);
		Assert.AreEqual (2, score.CalculateScore());
		
		SetScoreProperties (1.66f, 1, 2);
		Assert.AreEqual (2, score.CalculateScore());
		
		SetScoreProperties (2, 1, 2);	
		Assert.AreEqual (2, score.CalculateScore());
	}

	
	[Test]
	public void GetThirdScore()	{
		SetScoreProperties (2.01f, 1, 2);
		Assert.AreEqual (3, score.CalculateScore());
		
		
		SetScoreProperties (3, 1, 2);
		Assert.AreEqual (3, score.CalculateScore());		
		
		SetScoreProperties (10000, 1, 2);	
		Assert.AreEqual (3, score.CalculateScore());
	}

}
