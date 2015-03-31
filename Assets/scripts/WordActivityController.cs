using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class WordActivityController : MonoBehaviour {
	
	public Button resultsButton;
	private bool isActivityFinished;
	public float elapsedTimeOfActivity;
	
	[SerializeField]

	public Action ActivityFinished {
		get;
		set;
	}
	
	void Start () {
		
		resultsButton.onClick.AddListener (delegate {
			if(ActivityFinished != null){
				ActivityFinished();
				}
		});
		
		}
	
	public void SetActive(){
		gameObject.SetActive (true);
	}
	
	public void SetInactive(){
		gameObject.SetActive (false);
	}

}