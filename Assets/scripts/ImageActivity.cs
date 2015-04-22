using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ImageActivity : Activity {

	public TextAsset json;
	private ImageWordActivityData data = new ImageWordActivityData();
	private int correctWords;

	// GUI Objects
	[SerializeField]
	private RandomWordsList randomWords;
	[SerializeField]
	private Image firstImage;
	[SerializeField]
	private Text firstTitle;

	void Awake(){

		resultsButton.gameObject.SetActive(false);
		ReadDataFromJson ();
		randomWords.RandomWordSelected += HandleRandomWordSelected;		
		//randomWords.SelectedCorrectWords += HandleSelectedCorrectWords;
		ActivityStarted += HandleActivityStarted;	
		ActivityDataReseted += ReadDataFromJson;
	}

	private void ReadDataFromJson (){
		JsonImageWordParser parser = new JsonImageWordParser();
		data = new ImageWordActivityData ();
		parser.SetLevelFilter (level);
		parser.JSONString = json.text;
		data = parser.Data;	
		CheckIsDataFound ();
	}

	private void CheckIsDataFound() {

		if (data.wordsList.Count == 0 || data.wordValid == null || data.wordTranslated == null) {
			isDataFound = false;
			isCompleted = true;
		} else {
			isDataFound = true;
		}
	}
	
	private void HandleActivityStarted () {
		ClearActivity ();
		CreateActivity ();
		BeginActivity ();
	}
	
	private void BeginActivity ()	{
		gameStateBehaviour.GameState = GameState.ImageActivity;
	}

	private void ClearActivity () {
		DestroyWordList ();
		ClearImagesAndTittles ();
	}
	
	private void CreateActivity ()	{
		firstImage.sprite = GetImageFrom (data.wordValid);
		randomWords.DrawButtonsByWord (data.wordsList);
		result.RetryActionExecuted = RetryActivity;
	}

	private void DestroyWordList(){
		foreach(Transform  child in randomWords.transform ) {
			Destroy (child.gameObject);
		}
	}

	private void RetryActivity(){
		elapsedTimeOfActivity = 0;
		SetActivityAsNotFinished();
		ClearActivity ();
		CreateActivity ();
		gameStateBehaviour.GameState = GameState.ImageActivity;
	}

	private void HandleRandomWordSelected (Button wordButton) {
	/*	string nameButton = wordButton.transform.GetChild(0).GetComponent<Text>().text;

		if (nameButton == data.wordValid) {
			ChangeColorByState (wordButton, new Color32 (0, 255, 1, 255));
			firstTitle.text = data.wordTranslated;
			correctWords = 2;
		} else { 
			ChangeColorByState (wordButton, new Color32 (254, 0, 0, 255));
		}

		wordButton.interactable = false;
		if (correctWords >= 2) {
			//randomWords.DisableAllButtons();
			correctWords = 0;
			SetActivityAsFinished();
		}*/
	}

	private void HandleSelectedCorrectWords () {
		resultsButton.gameObject.SetActive(true);
	}

	private void ChangeColorByState (Button stateButton, Color32 stateColor){
		stateButton.transform.FindChild("StateImage").GetComponent<Image>().color = stateColor;
		stateButton.transform.FindChild("Text").GetComponent<Text>().color = stateColor;
	}

	private Sprite GetImageFrom(string word){
		return Resources.Load ("Images/"+word, typeof(Sprite)) as Sprite;
	}

	private void ClearImagesAndTittles(){
		firstImage.sprite = null;
		firstTitle.text = "";
	}
}

