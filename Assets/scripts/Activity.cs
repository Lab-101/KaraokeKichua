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
	protected int bestObteinedScore;
	protected int bestScoreObtained;

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

	public int BestObteinedScore {
		get {
			return bestObteinedScore;
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
		score.SetTime (elapsedTimeOfActivity);
		
		int scoreCalculate = score.CalculateScore ();
		
		if (scoreCalculate > bestScoreObtained) {
			bestScoreObtained = scoreCalculate;
			SaveBestScoreActivity ();
		}
		resultsButton.gameObject.SetActive (false);
		result.scoreLevel = scoreCalculate;
		result.elapsedTime = elapsedTimeOfActivity;
		gameStateBehaviour.GameState = GameState.ShowingResults;
	}

	private void SaveBestScoreActivity () {
		ActivityScoreData activityScoreData = new ActivityScoreData ();
		activityScoreData.level = level;
		activityScoreData.activity = GetActivityName ();
		activityScoreData.score = bestScoreObtained;
		ActivityScorePersistent.SaveActivityLevelScore (activityScoreData);
	}
	
	private void ReadScoreActivity () {
		bestScoreObtained = ActivityScorePersistent.GetScoreByActivityAndLevel (level, GetActivityName());
	}
	
	private string GetActivityName ()
	{
		return this.GetType ().ToString ();
	}
}
