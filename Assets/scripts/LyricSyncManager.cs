using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using System.Text.RegularExpressions; 
using UnityEngine.UI;

public class LyricSyncManager : MonoBehaviour {
	
	//Subtitle variables
	private string[] fileLines;
	private List<string> subtitleLines = new List<string>();
	public List<float> subtitleTimings = new List<float>();	
	public List<string> subtitleText = new List<string>();
	public List<object> subtitleTimesByWord = new List<object>();
	private int nextSubtitle = 0;	
	private string displaySubtitle;
	private AudioSource audioSource;
	private string subtitleFromLine;
	public Text lyricText;
	public Regex regex = new Regex(@" ?\{.*?\}");
	private int nextWord = 0;
	private List<float> timeSubtitle = new List<float>();
	private string color = "<color=#ffffffff>";
	private string endColor = "</color>";
	private string coloredWords = "";
	private string uncoloredWords = "";
	private bool startSubtitles = false;	
	public string[] timeArray;

	public static LyricSyncManager Instance { get; private set; }
	
	void Awake(){
		if(Instance != null && Instance != this)
			Destroy(gameObject);
		
		Instance = this;		
		gameObject.AddComponent<AudioSource>();
	}
	
	void Update () {
		//Increment nextSubtitle when we hit the associated time point
		if(nextSubtitle < subtitleText.Count){
			if(audioSource.time > subtitleTimings[nextSubtitle]){
				displaySubtitle = subtitleText[nextSubtitle];
				timeSubtitle = (List<float>)subtitleTimesByWord[nextSubtitle] ;				
				uncoloredWords = displaySubtitle.Replace ("*", " ");
				coloredWords = displaySubtitle.Split('*')[0] + " ";
				startSubtitles=true;
				nextSubtitle++;
				nextWord = 0;
			}
		}
		if (startSubtitles)
			GetDataBySubtitle(displaySubtitle, timeSubtitle);
	}
	
	private void GetDataBySubtitle(string subtitle, List<float> time){
		string[] subtitleArray = subtitle.Split('*');
		try {
			if (audioSource.time > time[nextWord] && nextWord < (time.Count-1)) {
				nextWord ++;
				if(time.Count - 1 == nextWord ){
					coloredWords += subtitleArray[nextWord];
				}
				else
					coloredWords += subtitleArray[nextWord]+" ";
			}
			Regex uncoloredWordsRegex = new Regex(Regex.Escape(coloredWords));
			string newUncoloredWords = uncoloredWordsRegex.Replace(uncoloredWords, "", 1);
			lyricText.text = color + coloredWords + endColor + newUncoloredWords;
		}
		catch {
			lyricText.text = color + coloredWords + endColor;
		}
	}

	public void BeginDialogue (List<string> songLyricSync, AudioSource clip) {
		lyricText.text = "";
		audioSource = clip;
		nextSubtitle = 0;
		ResetSubtitlesList ();
		subtitleLines = songLyricSync;
		SplitOutSubtitles ();
		//Set initial subtitle text
		if(subtitleText[0] != null)
			displaySubtitle = subtitleText[0];
	}
	
	private void ResetSubtitlesList(){
		//Reset all lists
		subtitleLines = new List<string>();
		subtitleTimings = new List<float>();
		subtitleText = new List<string>();
		subtitleTimesByWord = new List<object>();
		startSubtitles = false;
	}

	private void SplitOutSubtitles (){
		//Split out our subtitle elements splitTemp[0]= timeNumber splitTemp[1]= Text
		for(int cnt = 0; cnt < subtitleLines.Count; cnt++){
			string[] splitTemp = subtitleLines[cnt].Split(',');
			if (!IsSubtitleEmpty(splitTemp[9])) {
				subtitleTimings.Add(ParseTimeToSeconds(splitTemp[1]));
				for (int i = 9; i < splitTemp.Length; i++)
				{
					if (i>9)
						subtitleFromLine += ",";
					subtitleFromLine += splitTemp[i];
				}				
				string cleanedSubtitle = CleanSubtitleString(subtitleFromLine);
				List<float> wordTimeByPhrase = GetTimeForWord(subtitleFromLine, subtitleTimings[cnt]);
				subtitleText.Add(cleanedSubtitle);
				subtitleTimesByWord.Add(wordTimeByPhrase);
				subtitleFromLine = "";
			}
		}
	}

	//Set time to seconds
	private float ParseTimeToSeconds(string time){
		string[] timeArray = time.Split(':');
		float seconds = float.Parse(timeArray [0]) * 3600 + float.Parse(timeArray [1]) * 60 + float.Parse(timeArray [2]);
		return seconds;
	}
	
	//Remove all characters in brackets
	private string CleanSubtitleString(string subtitle)	{
		if(subtitle.Contains("}")){
			subtitle = regex.Replace (subtitle, "*");
			return subtitle.Substring(1);
		}
		return subtitle;
	}

	private List<float> GetTimeForWord(string subtitle, float startTime) {
		int wordCounter = 1;
		string wordTime = "";
		string[] subtitleArray = subtitle.Split('{');
		foreach(string numberWord in subtitleArray){
			string numberWordText = "{" + numberWord.ToString();
			if (regex.IsMatch(numberWordText)) {
				string time = regex.Match(numberWordText).Groups[0].Value.Replace (@"{\k", "").Replace("}", "");
				wordTime += (time + SetSpacesBetweenNumbers(wordCounter++, (subtitleArray.Length - 1)));
			}
		}
		return ParseRealTimesByPhrase (wordTime, startTime);
	}

	private List<float> ParseRealTimesByPhrase (string timeByWord, float start){
		List<float> timeByWordArray = new List<float>();
		if (!IsSubtitleEmpty(timeByWord)){
			string [] timeByWordString = timeByWord.Split(' ');
			for (int i = 0; i < timeByWordString.Length; i++){
				start += (float.Parse (timeByWordString [i])/100);
				timeByWordArray.Add(start);
			}
		}
		return timeByWordArray;
	}

	private string SetSpacesBetweenNumbers(int counter, int arraySize){
		if (counter < arraySize)
			return " ";
		return "";
	}

	private bool IsSubtitleEmpty(string subtitle){
		return subtitle.Length <= 1 || subtitle == "" || subtitle == null;
	}
}