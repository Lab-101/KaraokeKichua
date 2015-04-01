using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class ResultsController : MonoBehaviour {

	public Button exitButton;
	public Button retryButton;
	public Text score;
	public Text scoreDescription;
	public WriteActivityController writeActivity;
	public WordActivity wordActivity;
	public int scoreLevel;
	public Text time;
	public ProgressBarController progressBar;
	public Image Image1;
	public Image Image2;
	public Image Image3;

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
		//scoreLevel = writeActivity.score.CalculateScore ();
		progressBar.SetFillerSize (scoreLevel);
		scoreLevel = wordActivity.score.CalculateScore ();
		score.text = "Puntuacion de: "+ scoreLevel;
		scoreDescription.text = PutMessageAndRankByLevel (scoreLevel);
		//time.text = "Tiempo de la actividad: "+ writeActivity.elapsedTimeOfActivity;
		time.text = "Tiempo de la actividad: "+ wordActivity.elapsedTimeOfActivity;

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
		Image1.color = color1;
		Image2.color = color2;
		Image3.color = color3;
	}
}


