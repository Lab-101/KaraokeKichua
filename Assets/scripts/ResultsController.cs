using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class ResultsController : MonoBehaviour {

	public Button exitButton;
	public Button retryButton;
	public Text scoreDescription;
	public WriteActivityController writeActivity;
	public int scoreLevel;


	public Action BackActionExecuted {
		get;
		set;
	}
	public Action RetryActionExecuted {
		get;
		set;
	}

	void Start () {

		Debug.Log (writeActivity.elapsedTimeOfActivity);

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
		scoreDescription.text = messageLevel (scoreLevel);
	}
	public void SetActive(){
		gameObject.SetActive (true);
	}
	
	public void SetInactive(){
		gameObject.SetActive (false);
	}

	private string messageLevel(int level){
		switch (level)
		{
		case 1:
			return "Felicitaciones! Eres Muy Noob";
			break;
		case 2:
			return "Muy bien! Has practicado mucho y se ve en los resultados";
			break;
		case 3:
			return "Bien! Tienes que esforzarte más pero vas por buen camino";
			break;
		default:
			return "No se ha obtenido nivel";
			break;
		}
	}
}
