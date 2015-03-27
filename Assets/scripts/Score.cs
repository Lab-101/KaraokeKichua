using UnityEngine;
using System.Collections;

public class Score {
	private float time;
	private float min;
	private float max;

	public void SetTime (float time){
		this.time = time;
	}

	public void SetMin (float min){
		this.min = min;
	}

	public void SetMax (float max)	{
		this.max = max;
	}

	public int CalculateScore ()	{
		int points;
		if (IsBestScore())
			points = 1;
		else if (IsNormalScore())
			points = 2;
		else
			points = 3;

		return points;
	}

	private float GetMaxValueToBestScore(){
		float delta = (max - min)/3;
		return min + delta;
	}

	private bool IsBestScore(){
		return time <= GetMaxValueToBestScore ();
	}

	private bool IsNormalScore(){
		return time <= max;
	}

}
