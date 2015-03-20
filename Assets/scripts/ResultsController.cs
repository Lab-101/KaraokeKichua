using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class ResultsController : MonoBehaviour {

	public Button exitButton;
	public Button retryButton;

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
	}
	
	public void SetInactive(){
		gameObject.SetActive (false);
	}

	void Update () {
	
	}
}
