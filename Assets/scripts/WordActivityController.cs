using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class WordActivityController : MonoBehaviour {

	public RandomWordsController randomWords;	
	private List<string> correctWordsList = new List<string>();	
	private List<string> randomWordsList = new List<string>();
	public Button resultsButton;
	private bool isActivityFinished;
	public float elapsedTimeOfActivity;
	
	[SerializeField]

	public Action ActivityFinished {
		get;
		set;
	}
	
	void Start () {

		correctWordsList.Add ("cinco");
		correctWordsList.Add ("uno");
		randomWordsList.Add ("uno");
		randomWordsList.Add ("dos");
		randomWordsList.Add ("tres");
		randomWordsList.Add ("cuatro");
		randomWordsList.Add ("cinco");
		randomWords.DrawButtonsByWord(randomWordsList);
		randomWords.RandomWordSelected += HandleRandomWordSelected;
		
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