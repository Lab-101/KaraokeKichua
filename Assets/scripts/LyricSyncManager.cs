using UnityEngine;
using System.Collections;
using System.Collections.Generic; //Needed for Lists
using System.Text.RegularExpressions; //Needed for some string parsing
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
	//Trigger variables
	private List<string> triggerLines = new List<string>();	
	private List<string> triggerTimingStrings = new List<string>();
	public List<float> triggerTimings = new List<float>();	
	private List<string> triggers = new List<string>();
	public List<string> triggerObjectNames = new List<string>();
	public List<string> triggerMethodNames = new List<string>();
	private AudioSource audio;
	private int nextTrigger = 0;
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
		nextTrigger = 0;
		fileLines = songLyricSync.Split('\n');
		ResetSubtitlesList ();
		SplitSubtitlesAndTriggers ();
		SplitOutSubtitles ();
		SplitOutTriggers ();

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
		
		triggerLines = new List<string>();
		triggerTimingStrings = new List<string>();
		triggerTimings = new List<float>();
		triggers = new List<string>();
		triggerObjectNames = new List<string>();
		triggerMethodNames = new List<string>();			
	}

	private void SplitSubtitlesAndTriggers(){
		//Split subtitle and trigger related lines into different lists
		foreach(string line in fileLines){
			if(line.Contains("<trigger/>"))
				triggerLines.Add(line);
			else
				subtitleLines.Add(line);
		}
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

	void SplitOutTriggers (){
		//Split out our trigger elements
		for(int cnt = 0; cnt < triggerLines.Count; cnt++){
			string[] splitTemp1 = triggerLines[cnt].Split('|');
			triggerTimingStrings.Add(splitTemp1[0]);
			triggerTimings.Add(float.Parse(CleanTimeString(triggerTimingStrings[cnt])));
			
			triggers.Add(splitTemp1[1]);
			string[] splitTemp2 = triggers[cnt].Split('-');
			splitTemp2[0] = splitTemp2[0].Replace("<trigger/>", "");
			triggerObjectNames.Add(splitTemp2[0]);
			triggerMethodNames.Add(splitTemp2[1]);
		}
	}

	//Remove all characters that are not part of the timing float: (<time/>num)
	private string CleanTimeString(string timeString)	{
		Regex digitsOnly = new Regex(@"[^\d+(\.\d+)*$]");
		return digitsOnly.Replace(timeString, "");
	}
	
	void OnGUI () {
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