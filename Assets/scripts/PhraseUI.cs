using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using System;

public class PhraseUI : MonoBehaviour {
	public Button letterPrefab;
	private int currentLetter;
	private int currentHiddenWord;
	private int hiddenWordsCounter;
	private int numberWords;
	public bool isFirstHiddenWord = true;

	[SerializeField]
	private Hashtable hiddenWords;

	public Action<int> WordFinished {
		get;
		set;
	}

	public Action PhraseFinished {
		get;
		set;
	}
	
	void Start () {
		hiddenWords = new Hashtable ();
	}

	public void DrawPhrase(Phrase phrase){
		ResetPhraseCounters ();
		foreach (Word word in phrase.words){
			List<Button> letters = new List<Button>();
			foreach(char letter in word.text){
				letters.Add( createLetter(letter+"", word.isHidden));
			}
			if (word.isHidden)
				AddHiddenWordInList(letters);
			AddSpacesAfterWord(phrase.words);
		}
	}

	public bool CheckCorrectLetter(string letter){
		List<Button> letters = hiddenWords[currentHiddenWord] as List<Button>;
		Text buttonText = GetTextFrom (letters [currentLetter]);
		if (buttonText.text == letter){
			buttonText.gameObject.SetActive(true);
			currentLetter++;
			if (currentLetter >= letters.Count)
				SetNextHiddenWord(letters);
			return true;
		}
		return false;
	}

	public void ClearHiddenWords(){
		hiddenWords.Clear ();
	}

	private void ResetPhraseCounters(){		
		hiddenWordsCounter = 0;
		currentHiddenWord = 0;
		currentLetter = 0;
		numberWords = 0;
	}

	private void AddHiddenWordInList(List<Button> hiddenWordButtons){
		if(isFirstHiddenWord){
			ChangeColorListLetters(hiddenWordButtons, new Color32(147, 185, 249, 255));
			isFirstHiddenWord = false;
		}
		hiddenWords.Add(hiddenWordsCounter, hiddenWordButtons);
		hiddenWordsCounter++;
	}

	private void AddSpacesAfterWord(List<Word> wordList){
		numberWords++;
		if (numberWords < wordList.Count)
			createLetter("", false);
	}

	private Button createLetter(string letter, bool isLetterHide){
		Button newItem;
		newItem = Instantiate(letterPrefab) as Button;;
		newItem.image.color = isLetterHide ? Color.cyan : Color.white;
		newItem.name = letter;
		newItem.transform.SetParent(gameObject.transform, false);	
		newItem.transform.GetChild(0).gameObject.SetActive(!isLetterHide);
		newItem.transform.GetChild(0).GetComponent<Text>().text = letter;
		return newItem;
	}

	private void SetNextHiddenWord(List<Button> letterButton){
		currentLetter = 0;
		currentHiddenWord++;
		PaintWordFinished(letterButton);
		if (currentHiddenWord >= hiddenWords.Count)
			FinishGame();
		else
			PaintNextWordInPhrase(letterButton);
	}

	private void PaintWordFinished(List<Button> finishedWordButtons){
		if (WordFinished != null){		
			ChangeColorListLetters(finishedWordButtons, Color.green);
			WordFinished(currentHiddenWord);
		}
	}

	private void PaintNextWordInPhrase(List<Button> netxWordButtons){
		netxWordButtons = hiddenWords[currentHiddenWord] as List<Button>;
		ChangeColorListLetters(netxWordButtons, new Color32(147, 185, 249, 255));
	}

	private void FinishGame(){
		if (PhraseFinished!=null)
			PhraseFinished();
		isFirstHiddenWord = true;
	}
		
	private void ChangeColorListLetters(List<Button> letters, Color color){
		foreach (Button letter in letters)
			letter.image.color = color;
	}

	private Text GetTextFrom(Button button){
		return button.transform.GetChild(0).GetComponent<Text>() as Text;
	}
}