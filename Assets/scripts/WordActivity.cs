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
	private List<int> indexCorrectWords = new List<int>();

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
	[SerializeField]
	protected Button translatedSongButton;

	void Awake(){
		ReadDataFromJson ();
		randomWords.RandomWordSelected += HandleRandomWordSelected;		
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
		indexCorrectWords.Clear ();
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
		for (int index = 0; index < data.wordsValidsList.Count; index++) {
			string correctWord = data.wordsValidsList [index];
			if (nameButton == correctWord) {
				if(!indexCorrectWords.Contains(index)){
					titleList [correctWords].text = correctWord;
					imageList [correctWords].sprite = GetImageFrom (correctWord);
					indexCorrectWords.Add(index);
					correctWords++;
				}
				ChangeColorByState (wordButton, new Color32 (0, 255, 1, 255), true);
				wordAudio.SetWordToPlay (correctWord);
				wordAudio.PlayWord ();
				break;
			}
			else 
				ChangeColorByState (wordButton, new Color32 (254, 0, 0, 255), false);
		}
		if (indexCorrectWords.Count >= 2) {
			DisableWords();
			correctWords = 0;
			SetActivityAsFinished();
		}
	}

	private void DisableWords(){
		for (int indexWord = 0; indexWord < data.wordsList.Count; indexWord++) {
			string word = data.wordsList [indexWord];
			if (!data.wordsValidsList.Contains(word) )
				randomWords.DisableButtonInIndex (indexWord);
		}
		FinishPhraseActivity();
	}

	private void ChangeColorByState (Button stateButton, Color32 stateColor, bool buttonState){
		stateButton.transform.FindChild("StateImage").GetComponent<Image>().color = stateColor;
		stateButton.transform.FindChild("Text").GetComponent<Text>().color = stateColor;
		stateButton.interactable = buttonState;
	}

	private void FinishPhraseActivity () {
		resultsButton.gameObject.SetActive(true);
		//translatedSongButton.gameObject.SetActive(true);
		
		LevelData data = new LevelData();
		data.level = level;
		data.isUnlocked = true;
		data.isIntroOpened = true;
		LevelDataPersistent.SaveLevelData(data);
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
