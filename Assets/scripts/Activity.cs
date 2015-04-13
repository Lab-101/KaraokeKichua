using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Activity : MonoBehaviour {
	protected int level;
	protected Score score;
	protected bool isDataFound;
	protected bool isActivityFinished;
	protected bool isCompleted;
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

	public bool IsCompleted {
		get {
			return isCompleted;
		}
	}

	public void ResetData(){
		isDataFound = false;
		isCompleted = false;
		if (ActivityDataReseted != null)
			ActivityDataReseted ();
	}

	public void StartActivity(){
		elapsedTimeOfActivity = 0;
		SetActivityAsNotFinished ();
		
		if (ActivityStarted != null)
			ActivityStarted ();
	}
		
	public bool IsDataFound(){
		return isDataFound;
	}

	protected void SetActivityAsFinished(){
		isActivityFinished = true;
		isCompleted = true;
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
