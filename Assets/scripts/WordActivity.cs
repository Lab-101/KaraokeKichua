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
	public KaraokeController karaoke;

	void Awake(){
		ReadDataFromJson ();
				
		randomWords.RandomWordSelected += HandleRandomWordSelected;		
		randomWords.SelectedCorrectWords += HandleSelectedCorrectWords;
		ActivityReseted += HandleActivityReseted;		
	}

	private void ReadDataFromJson (){
		JsonWordsParser parser = new JsonWordsParser();
		parser.SetLevelFilter (level);
		parser.JSONString = json.text;
		data = parser.Data;	
	}

	private void DestroyWordList(){
		foreach(Transform  child in randomWords.transform ) {
			Destroy (child.gameObject);
		}
	}

	private void ClearElements(){
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
	
	private void HandleActivityReseted(){
		ReadDataFromJson ();
		DestroyWordList ();
		ClearElements ();
		randomWords.DrawButtonsByWord(data.wordsList);
		gameStateBehaviour.GameState = GameState.WordActivity;
		result.RetryActionExecuted += HandleActivityReseted;
	}
}
