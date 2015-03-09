using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using System;

public class PhraseUI : MonoBehaviour {
	public Button letterPrefab;
	private int currentLetter = 0;
	private int currentHiddenWord = 0;
	public Action<int> FinishedWord;

	[SerializeField]
	private Hashtable hiddenWords;
	
	void Start () {
		hiddenWords = new Hashtable ();
	}

	public void DrawPhrase(Phrase phrase){
		int hiddenWordsCounter = 0;
		currentHiddenWord = 0;
		currentLetter = 0;
		int numberWords = 0;
		foreach (Word word in phrase.words){
			List<Button> letters = new List<Button>();
			foreach(char letter in word.text){
				letters.Add( createLetter(letter+"", word.isHide));
			}
			if (word.isHide){
				hiddenWords.Add(hiddenWordsCounter, letters);
				hiddenWordsCounter++;
			}
			numberWords++;
			if (numberWords < phrase.words.Count)
				createLetter("", false);
		}
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

	public bool CheckCorrectLetter(string letter){
		List<Button> letters = hiddenWords[currentHiddenWord] as List<Button>;
		Text buttonText = GetTextFrom (letters [currentLetter]);
		if (buttonText.text == letter){
			buttonText.gameObject.SetActive(true);
			currentLetter++;
			if (currentLetter>=letters.Count){
				currentLetter = 0;
				currentHiddenWord++;
				if (FinishedWord != null){
					FinishedWord(currentHiddenWord);
				}
			}
			return true;
		}
		return false;
	}

	public void ClearHiddenWords(){
		hiddenWords.Clear ();
	}

	private Text GetTextFrom(Button button){
		return button.transform.GetChild(0).GetComponent<Text>() as Text;
	}
}
