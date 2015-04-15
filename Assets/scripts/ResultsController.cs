using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class ResultsController : MonoBehaviour {
	//Data
	public float elapsedTime;
	public int scoreLevel;

	//GUI Objects
	public Button exitButton;
	public Button retryButton;
	public Text score;
	public Text scoreDescription;
	public Text time;
	public Image imageRegularScore;
	public Image imageNormalScore;
	public Image imageBestScore;
	public ProgressBarController progressBar;
	[SerializeField]
	private Text levelName;
	[SerializeField]
	protected GameStateBehaviour gameStateBehaviour;
	private float sizeNumberLevel;
	private float sizeNameLevel;

	public Action BackActionExecuted {
		get;
		set;
	}
	public Action RetryActionExecuted {
		get;
		set;
	}

	void Start () {
		exitButton.onClick.AddListener(delegate {
			gameStateBehaviour.GameState = GameState.SelectingLevel;
		});

		retryButton.onClick.AddListener(delegate {
			if(RetryActionExecuted != null){
				RetryActionExecuted();
			}
		});

		sizeNumberLevel = Screen.width / 32;
		sizeNameLevel = 5 * Screen.width / 128;
	}
	public void SetActive(){
		gameObject.SetActive (true);
		score.text = PutMessageAndRankByLevel (scoreLevel, 1);
		scoreDescription.text = PutMessageAndRankByLevel (scoreLevel, 0);
		levelName.text = "<size=" + sizeNumberLevel + "><color=#E5C507FF>NIVEL " + GameSettings.Instance.nameLevel[0] + "</color></size><size=" + sizeNameLevel + "><color=#FFFFFFFF><b> " + GameSettings.Instance.nameLevel[1] + "</b></color></size>";
		time.text = "Tiempo de la actividad: "+ elapsedTime;
		progressBar.SetFillerSize (GameSettings.Instance.scoreByCurrentLevel);
	}
	
	public void SetInactive(){
		gameObject.SetActive (false);
	}

	private string PutMessageAndRankByLevel(int level, int phraseItem){
		switch (level) {
		case 1:
			SetImagesByScore("yes", "no", "no");
			return GameSettings.Instance.regularScoreMessage[phraseItem];
		case 2:
			SetImagesByScore("yes", "yes", "no");
			return GameSettings.Instance.normalScoreMessage[phraseItem];
		case 3:
			SetImagesByScore("yes", "yes", "yes");
			return GameSettings.Instance.bestScoreMessage[phraseItem];
		default:
			return "No se ha obtenido nivel";
		}
	}

	private void SetImagesByScore(string firstImage, string secondImage, string thirdImage){
		imageRegularScore.sprite = GetImageFromScore(firstImage);
		imageNormalScore.sprite = GetImageFromScore(secondImage);
		imageBestScore.sprite = GetImageFromScore(thirdImage);
	}

	private Sprite GetImageFromScore(string word){
		return Resources.Load ("Images/Texturas/corn_" + word, typeof(Sprite)) as Sprite;
	}
}