using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;


using UnityEngine;


public class WordActivity : MonoBehaviour {

	public RandomWordsController randomWords;
	public Button resultsButton;
	public float timeA;
	public float timeB;
	public Score score;
	private WordActivityData data;
	public TextAsset json;

	private List<string> correctWordsList = new List<string>();	
	private List<string> randomWordsList = new List<string>();
	private bool isActivityFinished;
	public float elapsedTimeOfActivity;
	private int correctWords;

	private List<Image> imageList = new List<Image>();
	[SerializeField]
	private Image firstImage;
	[SerializeField]
	private Image secondImage;
	private List<Text> titleList = new List<Text>();
	[SerializeField]
	private Text firstTitle;
	[SerializeField]
	private Text secondTitle;

	public Action ActivityFinished {
		get;
		set;
	}

	void Start(){
		resultsButton.onClick.AddListener (delegate {
			if(ActivityFinished != null){
				ActivityFinished();
				resultsButton.gameObject.SetActive(false);
				score.SetTime (elapsedTimeOfActivity);
			}
		});

		randomWords.RandomWordSelected += HandleRandomWordSelected;		
		randomWords.SelectedCorrectWords += HandleSelectedCorrectWords;
	}

	void Update ()	{
		if(!isActivityFinished)
			elapsedTimeOfActivity += Time.deltaTime;
	}

	void Awake () {
		score = new Score ();
		score.SetTimeA (10);
		score.SetTimeB (20);
		isActivityFinished = true;
	}

	public void Reset(Song song){
		SetActive ();
		DestroyWordList ();
		SetDataToActivity();
		ClearElements ();
		randomWords.DrawButtonsByWord(randomWordsList);
		//phrase = GetRandomPhraseFromSong (song);
		elapsedTimeOfActivity = 0;
		isActivityFinished = false;
	}

	public void SetActive(){
		gameObject.SetActive (true);
	}
	
	public void SetInactive(){
		gameObject.SetActive (false);
	}

	private void ClearElements(){
		imageList = new List<Image>();
		imageList.Add (firstImage);
		imageList.Add (secondImage);
		foreach (Image picture in imageList)
			picture.sprite = null;
		titleList = new List<Text>();
		titleList.Add (firstTitle);
		titleList.Add (secondTitle);
		foreach (Text title in titleList)
			title.text = "";
	}

	private void SetDataToActivity(){
		data = new WordActivityData ();
		JsonWordsParser parser = new JsonWordsParser ();
		parser.SetLevelFilter (1);
		parser.JSONString = json.text;
		data = parser.Data;
		randomWordsList = data.wordsList;
		correctWordsList = data.wordsValidsList;
	}

	private void DestroyWordList(){
		foreach(Transform  child in randomWords.transform ) {
			Destroy (child.gameObject);
		}
	}

	void HandleRandomWordSelected (Button wordButton) {
		string nameButton = wordButton.transform.GetChild(0).GetComponent<Text>().text;
		foreach (string correctWord in correctWordsList) {
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

	void HandleSelectedCorrectWords () {
		resultsButton.gameObject.SetActive(true);
	}

	
	private Sprite GetImageFrom(string word){
		return Resources.Load ("Images/"+word, typeof(Sprite)) as Sprite;
	}
}