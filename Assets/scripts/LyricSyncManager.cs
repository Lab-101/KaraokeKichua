using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using System.Text.RegularExpressions; 
using UnityEngine.UI;

public class LyricSyncManager : MonoBehaviour {

	//Subtitle variables
	private string[] fileLines;
	private List<string> subtitleLines = new List<string>();	
	private List<string> subtitleTimingStrings = new List<string>();
	public List<float> subtitleTimings = new List<float>();	
	public List<string> subtitleText = new List<string>();	
	private int nextSubtitle = 0;	
	private string displaySubtitle;	
	private AudioSource audio;
	public Text lyricText;

	public static LyricSyncManager Instance { get; private set; }
	
	void Awake(){
		if(Instance != null && Instance != this)
			Destroy(gameObject);
		
		Instance = this;		
		gameObject.AddComponent<AudioSource>();
	}
	
	public void BeginDialogue (string songLyricSync, AudioSource clip) {
		lyricText.text = "";
		audio = clip;
		nextSubtitle = 0;
		fileLines = songLyricSync.Split('\n');
		ResetSubtitlesList ();
		SplitSubtitles();
		SplitOutSubtitles ();

		//Set initial subtitle text
		if(subtitleText[0] != null)
			displaySubtitle = subtitleText[0];
	}

	private void ResetSubtitlesList(){
		//Reset all lists
		subtitleLines = new List<string>();
		subtitleTimingStrings = new List<string>();
		subtitleTimings = new List<float>();
		subtitleText = new List<string>();		
	}

	private void SplitSubtitles(){
		//Split subtitle and related lines into different lists
		foreach(string line in fileLines)
			subtitleLines.Add(line);
	}

	private void SplitOutSubtitles (){
		//Split out our subtitle elements splitTemp[0]= timeNumber splitTemp[1]= Text
		for(int cnt = 0; cnt < subtitleLines.Count; cnt++){
			string[] splitTemp = subtitleLines[cnt].Split('|');
			subtitleTimingStrings.Add(splitTemp[0]);
			subtitleTimings.Add(float.Parse(CleanTimeString(subtitleTimingStrings[cnt])));
			subtitleText.Add(splitTemp[1]);						
		}
	}

	//Remove all characters that are not part of the timing float: (<time/>num)
	private string CleanTimeString(string timeString)	{
		Regex digitsOnly = new Regex(@"[^\d+(\.\d+)*$]");
		return digitsOnly.Replace(timeString, "");
	}
	
	void Update () {
		//Increment nextSubtitle when we hit the associated time point
		if(nextSubtitle < subtitleText.Count){
			if(audio.time > subtitleTimings[nextSubtitle]){
				displaySubtitle = subtitleText[nextSubtitle];
				lyricText.text = displaySubtitle;
				nextSubtitle++;
			}
		}
	}
}