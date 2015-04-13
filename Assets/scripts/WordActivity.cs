using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class WordActivity : Activity {
	// Activity Data
	public TextAsset json;
	private WordActivityData data = new WordActivityData();
	
	private int correctWords;
	private List<Text> titleList = new List<Text>();
	private List<Image> imageList = new List<Image>();

	// GUI Objects
	[SerializeField]
	private RandomWordsList randomWords;
	[SerializeField]
	private Image firstImage;
	[SerializeField]
	private Image secondImage;
	[SerializeField]
	private Text firstTitle;
	[SerializeField]
	private Text secondTitle;
	[SerializeField]
	protected SongController songController;

	void Awake(){
		ReadDataFromJson ();
		randomWords.RandomWordSelected += HandleRandomWordSelected;		
		randomWords.SelectedCorrectWords += HandleSelectedCorrectWords;
		ActivityStarted += HandleActivityStarted;
		ActivityDataReseted += ReadDataFromJson;
	}

	public void PlayPreview ()	{
		songController.PlayPreview(data.songName);
	}

	public void StopSong(){
		songController.StopSong ();
	}

	private void ReadDataFromJson (){
		JsonWordsParser parser = new JsonWordsParser();
		data = new WordActivityData ();
		parser.SetLevelFilter (level);
		parser.JSONString = json.text;
		data = parser.Data;	
		CheckIsDataFound ();
		SetDataSong (data.songName);
	}
	
	private void CheckIsDataFound() {
		if (data.wordsList.Count == 0 || data.wordsValidsList.Count == 0 || data.songName == null) {
			isDataFound = false;
			isCompleted = true;
		} else {
			isDataFound = true;
		}
	}

	private void DestroyWordList(){
		foreach(Transform  child in randomWords.transform ) {
			Destroy (child.gameObject);
		}
	}

	private void ClearImagesAndTittles(){
		imageList = new List<Image>();
		imageList.Add (firstImage);
		imageList.Add (secondImage);
		titleList = new List<Text>();
		titleList.Add (firstTitle);
		titleList.Add (secondTitle);

		foreach (Image picture in imageList)
			picture.sprite = null;

		foreach (Text title in titleList)
			title.text = "";
	}
	
	private Sprite GetImageFrom(string word){
		return Resources.Load ("Images/"+word, typeof(Sprite)) as Sprite;
	}

	private void SetDataSong(string song){
		songController.SetSong(song);
	} 
	
	private void ClearActivity () {
		DestroyWordList ();
		ClearImagesAndTittles ();
	}
	
	private void CreateActivity ()	{
		randomWords.DrawButtonsByWord (data.wordsList);
		result.RetryActionExecuted = RetryActivity;
	}

	private void HandleRandomWordSelected (Button wordButton) {
		string nameButton = wordButton.transform.GetChild(0).GetComponent<Text>().text;
		foreach (string correctWord in data.wordsValidsList) {
			if (nameButton == correctWord){
				wordButton.image.color = Color.green;
				titleList[correctWords].text = correctWord;
				imageList[correctWords].sprite = GetImageFrom(correctWord);
				correctWords++;
				break;
			}
			else 
				wordButton.image.color = Color.red;
		}
		wordButton.interactable = false;
		if (correctWords >= 2) {
			randomWords.DisableAllButtons();
			correctWords = 0;
			SetActivityAsFinished();
		}
	}

	private void HandleSelectedCorrectWords () {
		resultsButton.gameObject.SetActive(true);
	}
	
	private void HandleActivityStarted(){
		ClearActivity ();
		CreateActivity ();
		songController.StartKaraoke ();
	}

	private void RetryActivity(){
		elapsedTimeOfActivity = 0;
		SetActivityAsNotFinished();
		ClearActivity ();
		CreateActivity ();
		gameStateBehaviour.GameState = GameState.WordActivity;
	}
}
