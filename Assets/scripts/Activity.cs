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
	protected Button resultsButton;
	[SerializeField]
	protected ResultsController result;
	[SerializeField]
	protected GameStateBehaviour gameStateBehaviour;

	protected Action ActivityStarted {
		get;
		set;
	}
	protected Action ActivityDataReseted {
		get;
		set;
	}

	void Start (){		
		resultsButton.onClick.AddListener (delegate {
			CloseActivity ();
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

	public void SetActive(){
		gameObject.SetActive (true);
	}
	
	public void SetInactive(){
		gameObject.SetActive (false);
	}

	public void SetLevel(int level){
		this.level = level;
	}

	public void ResetData(){
		if (ActivityDataReseted != null)
			ActivityDataReseted ();
	}

	public void StartActivity(){
		elapsedTimeOfActivity = 0;
		SetActivityAsNotFinished ();
		
		if (ActivityStarted != null)
			ActivityStarted ();
	}

	protected void SetActivityAsFinished(){
		isActivityFinished = true;
	}

	protected void SetActivityAsNotFinished(){
		isActivityFinished = false;
	}

	private void CloseActivity ()	{
		resultsButton.gameObject.SetActive (false);
		score.SetTime (elapsedTimeOfActivity);
		result.scoreLevel = score.CalculateScore ();
		result.elapsedTime = elapsedTimeOfActivity;
		gameStateBehaviour.GameState = GameState.ShowingResults;
	}
}
