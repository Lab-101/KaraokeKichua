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
			if(BackActionExecuted != null){
				BackActionExecuted();
			}
		});

		retryButton.onClick.AddListener(delegate {
			if(RetryActionExecuted != null){
				RetryActionExecuted();
			}
		});


	}
	public void SetActive(){
		gameObject.SetActive (true);
		score.text = "Puntuacion de: "+ scoreLevel;
		scoreDescription.text = PutMessageAndRankByLevel (scoreLevel);
		time.text = "Tiempo de la actividad: "+ elapsedTime;
		progressBar.SetFillerSize (scoreLevel);

	}
	
	public void SetInactive(){
		gameObject.SetActive (false);
	}

	private string PutMessageAndRankByLevel(int level){
		switch (level) {
			case 1:
				SetColor(Color.yellow, Color.gray, Color.gray);
				return "Bien! Tienes que esforzarte más pero vas por buen camino";
			case 2:
				SetColor(Color.yellow, Color.yellow, Color.gray);
				return "Muy bien! Has practicado mucho y se ve en los resultados";
			case 3:
				SetColor(Color.yellow, Color.yellow, Color.yellow);
				return "Felicitaciones! Eres Muy Noob-Pro";
			default:
				return "No se ha obtenido nivel";
		}
	}

	private void SetColor(Color color1, Color color2, Color color3){
		imageRegularScore.color = color1;
		imageNormalScore.color = color2;
		imageBestScore.color = color3;
	}
}


