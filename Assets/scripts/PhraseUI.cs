using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class PhraseUI : MonoBehaviour {
	public Button letterVisiblePrefab;
	public Button letterHidePrefab;
	private int numberWords;
	private int currentLetter=0;

	[SerializeField]
	private Hashtable hiddenWords;
	
	void Start () {
		hiddenWords = new Hashtable ();
	}

	public void DrawPhrase(Phrase phrase){
		int hiddenWordsCounter = 0;
		foreach (Word word in phrase.words){
			List<Button> letters = new List<Button>();
			foreach(char letter in word.text){
				letters.Add( createLetter(letter+"", word.isHide));
			}
			if (word.isHide)
			{
				hiddenWords.Add(hiddenWordsCounter, letters);
				hiddenWordsCounter++;
			}
			numberWords++;
			if (numberWords < phrase.words.Count)
				createLetter("", false);
		}
	}

	private Button GetPrefab(bool isWordHide){
		if(isWordHide)
			return Instantiate(letterHidePrefab) as Button;

		return Instantiate(letterVisiblePrefab) as Button;
	}

	private Button createLetter(string letter, bool isLetterHide){
		Button newItem;
		newItem = GetPrefab (isLetterHide);
		newItem.image.color = isLetterHide ? Color.cyan : Color.white;
		newItem.name = letter;
		newItem.transform.SetParent(gameObject.transform, false);	
		newItem.transform.GetChild(0).GetComponent<Text>().text = letter;
		return newItem;
	}

	public bool CheckCorrectLetter(string letter){
		List<Button> letters = hiddenWords[0] as List<Button>;
		Text buttonText = GetTextFrom (letters [currentLetter]);
		if (buttonText.text == letter){
			buttonText.gameObject.SetActive(true);
			currentLetter++;
			return true;
		}
		return false;
	}

	private Text GetTextFrom(Button button)
	{
		return button.transform.GetChild(0).GetComponent<Text>() as Text;
	}
}
