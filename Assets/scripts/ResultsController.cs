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
	public int scoreLevel;
	public Text time;
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
		scoreLevel = writeActivity.score.CalculateScore ();
		score.text = "Puntuacion de: "+ scoreLevel;
		scoreDescription.text = messageLevel (scoreLevel);
		time.text = "Tiempo de la actividad: "+ writeActivity.elapsedTimeOfActivity;
		progressBar.SetFillerSize (scoreLevel);
	}
	
	public void SetInactive(){
		gameObject.SetActive (false);
	}

	private string messageLevel(int level){
		switch (level) {
			case 1:
				return "Bien! Tienes que esforzarte más pero vas por buen camino";
			case 2:
				return "Muy bien! Has practicado mucho y se ve en los resultados";
			case 3:
				return "Felicitaciones! Eres Muy Noob-Pro";
			default:
				return "No se ha obtenido nivel";
		}
	}
}
