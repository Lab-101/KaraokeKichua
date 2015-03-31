using UnityEngine;
using System.Collections;

public class Activity : MonoBehaviour {

	protected Score score;
	protected ActivityData data;
	protected bool isActivityFinished;
	protected float elapsedTimeOfActivity;

	void Update ()	{
		if(!isActivityFinished)
			elapsedTimeOfActivity += Time.deltaTime;
	}
}
