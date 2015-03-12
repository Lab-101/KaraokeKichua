using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Text.RegularExpressions;

public class PhraseSplitter {

	private string pattern = @"^(\s+|\d+|\w+|[^\d\s\w]+)+$";

	public string FullPhrase {
		set;
		private get;
	}

	public List<object> HiddenWords{
		set;
		private get;
	}

	public List<Word> WordsList{
		get {
			return SplitPhrase();
		}
	}

	private List<Word> SplitPhrase(){
		List<Word> wordsList = new List<Word> ();
		string[] words = SplitBySpaceAndPunctuations();
		foreach(string word in words)
		{
			wordsList.Add(CreateWord(word));
		}
		return wordsList;
	}

	private string[] SplitBySpaceAndPunctuations()
	{
		List<string> stringList = new List<string> ();
		Regex regex = new Regex(pattern);
		if (regex.IsMatch(FullPhrase))
		{
			Match match = regex.Match(FullPhrase);
			
			foreach (Capture capture in match.Groups[1].Captures)
			{
				if (!IsNullOrWhiteSpace(capture.Value))
					stringList.Add(capture.Value);
			}
		}
		return stringList.ToArray ();
	}

	private bool IsNullOrWhiteSpace(string aString)
	{
		return string.IsNullOrEmpty (aString) || aString.Trim ().Length == 0;
	}

	private Word CreateWord(string word)
	{
		Word wordObject = new Word();
		wordObject.text = word;
		wordObject.isHide = IsHiddenWord(word);
		return wordObject;
	}

	private bool IsHiddenWord (string word)
	{
		foreach(Dictionary<string, object> hiddenWord in HiddenWords)
			if (word == (string)hiddenWord["text"])
				return true;
		return false;
	}
}