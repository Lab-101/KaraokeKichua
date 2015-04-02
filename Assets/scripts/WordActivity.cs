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
	private Button resultsButton;
	[SerializeField]
	private Image firstImage;
	[SerializeField]
	private Image secondImage;
	[SerializeField]
	private Text firstTitle;
	[SerializeField]
	private Text secondTitle;

	void Awake(){
		ReadDataFromJson ();
		
		resultsButton.onClick.AddListener (delegate {
			if(ActivityFinished != null){
				ActivityFinished();
				resultsButton.gameObject.SetActive(false);
				score.SetTime (elapsedTimeOfActivity);
			}
		});
		
		randomWords.RandomWordSelected += HandleRandomWordSelected;		
		randomWords.SelectedCorrectWords += HandleSelectedCorrectWords;
		ActivityReseted += HandleActivityReseted;
		
		score = new Score ();
		score.SetTimeA (10);
		score.SetTimeB (20);
		SetActivityAsNotFinished ();
	}

	private void ReadDataFromJson (){
		JsonWordsParser parser = new JsonWordsParser();
		parser.SetLevelFilter (1);
		parser.JSONString = json.text;
		data = parser.Data;	
	}

	private void HandleActivityReseted(){
		DestroyWordList ();
		ClearElements ();
		randomWords.DrawButtonsByWord(data.wordsList);
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
			isActivityFinished = true;
		}
	}
	
	private void HandleSelectedCorrectWords () {
		resultsButton.gameObject.SetActive(true);
	}
	

}
