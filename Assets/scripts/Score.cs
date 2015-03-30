using UnityEngine;
using System.Collections;

public class Score {
	private float elapseTime;
	private float timeA;
	private float timeB;

	public void SetTime (float time){
		this.elapseTime = time;
	}

	public void SetTimeA (float time){
		this.timeA = time;
	}

	public void SetTimeB (float time)	{
		this.timeB = time;
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
		float delta = (timeB - timeA)/3;
		return timeA + delta;
	}

	private bool IsBestScore(){
		return elapseTime <= GetMaxValueToBestScore ();
	}

	private bool IsNormalScore(){
		return elapseTime <= timeB;
	}

}
