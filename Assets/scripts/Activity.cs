using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Activity : MonoBehaviour {
	protected int level;
	protected Score score;
	protected bool isActivityFinished;
	protected float elapsedTimeOfActivity;

	[SerializeField]
	protected float timeA;
	[SerializeField]
	protected float timeB;
	[SerializeField]
	protected ResultsController result;
	[SerializeField]
	protected Button resultsButton;

	public Action ActivityReseted {
		get;
		set;
	}

	public Action ActivityFinished {
		get;
		set;
	}

	void Start (){		
		resultsButton.onClick.AddListener (delegate {
			if(ActivityFinished != null){
				ActivityFinished();
				resultsButton.gameObject.SetActive(false);
				score.SetTime (elapsedTimeOfActivity);
			}
		});

		score = new Score ();
		score.SetTimeA (timeA);
		score.SetTimeB (timeB);
		SetActivityAsNotFinished ();
	}

	void Update ()	{
		if(!isActivityFinished)
			elapsedTimeOfActivity += Time.deltaTime;
	}

	public void Reset(){
		elapsedTimeOfActivity = 0;
		SetActive ();
		SetActivityAsNotFinished ();
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

	public void SetLevel(int level){
		this.level = level;
	}

	public int GetScorePoints(){
		return score.CalculateScore();
	}

	public float GetElapsedTime(){
		return elapsedTimeOfActivity;
	}

	protected void SetActivityAsFinished(){
		isActivityFinished = true;
	}

	protected void SetActivityAsNotFinished(){
		isActivityFinished = false;
	}
}
