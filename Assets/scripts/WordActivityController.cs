using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;


using UnityEngine;


public class WordActivityController : MonoBehaviour {
	
	protected Score score;
	private WordActivityData data;
	public TextAsset json;

	public RandomWordsController randomWords;	
	private List<string> correctWordsList = new List<string>();	
	private List<string> randomWordsList = new List<string>();
	public Button resultsButton;
	private bool isActivityFinished;
	public float elapsedTimeOfActivity;
	
	void Start(){
		data = new WordActivityData ();
		JsonWordsParser parser = new JsonWordsParser ();
		parser.SetLevelFilter (1);
		parser.JSONString = json.text;
		data = parser.Data;
		randomWordsList = data.wordsList;
		correctWordsList = data.wordsValidsList;
		randomWords.DrawButtonsByWord(randomWordsList);
		randomWords.RandomWordSelected += HandleRandomWordSelected;
		
		resultsButton.onClick.AddListener (delegate {
			if(ActivityFinished != null){
				ActivityFinished();
			}
		});
	}
	
	[SerializeField]

	public Action ActivityFinished {
		get;
		set;
	}

	public void SetActive(){
		gameObject.SetActive (true);
	}
	
	public void SetInactive(){
		gameObject.SetActive (false);
	}

	void HandleRandomWordSelected (Button wordButton) {
		Debug.Log ("oprimio!");
		string nameButton = wordButton.transform.GetChild(0).GetComponent<Text>().text;
		foreach (string correctWord in correctWordsList) {
			if (nameButton == correctWord){
				wordButton.image.color = Color.green;
				break;
			}
			else 
				wordButton.image.color = Color.red;
		}
		wordButton.interactable = false;
	}
}