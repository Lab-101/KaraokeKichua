using System;
using UnityEngine;
using System.Collections;

public class Activity : MonoBehaviour {

	protected Score score;
	protected bool isActivityFinished;
	protected float elapsedTimeOfActivity;

	public Action ActivityReseted {
		get;
		set;
	}

	public Action ActivityFinished {
		get;
		set;
	}

	void Update ()	{
		if(!isActivityFinished)
			elapsedTimeOfActivity += Time.deltaTime;
	}

	public void Reset(Song song){
		SetActive ();
		elapsedTimeOfActivity = 0;
		isActivityFinished = false;
		if (ActivityReseted != null) {
			ActivityReseted ();
		}
	}

	public void SetActive(){
		gameObject.SetActive (true);
	}
	
	public void SetInactive(){
		gameObject.SetActive (false);
	}
}
