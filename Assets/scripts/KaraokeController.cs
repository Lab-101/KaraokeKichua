using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic; 
using UnityEngine.UI;

public class KaraokeController : MonoBehaviour {
	public Button finishButton;
	public Button pauseButton;
	public LyricSyncManager lyricSync;
	[SerializeField]
	private Text levelNameText;
	[SerializeField]
	private Text songNameText;
	
	public Action SongFinished {
		get;
		set;
	}
	
	public Action SongPaused {
		get;
		set;
	}
	
	void Start () {
		finishButton.onClick.AddListener(delegate {
			if(SongFinished != null){
				SongFinished();
				ChangePauseElements("Pausa", false);
			}
		});
		pauseButton.onClick.AddListener(delegate {
			if(SongPaused != null){
				SongPaused();
				ChangePauseState();
			}
		});
	}
	
	public void SetActive(){
		gameObject.SetActive (true);
	}
	
	public void SetInactive(){
		gameObject.SetActive (false);
	}

	public void SetHeaderInfo (int numberLevel, string nameLevel)
	{
		levelNameText.text = "<color=#E5C507FF>NIVEL " + numberLevel + "</color><size=80><color=#FFFFFFFF><b> " + nameLevel + "</b></color></size>";//47 57
	}
	
	private void ChangePauseState(){
		if(pauseButton.GetComponentInChildren<Text> ().text == "Pausa")
			ChangePauseElements("Continuar", true);
		else
			ChangePauseElements("Pausa", false);
	}
	
	private void ChangePauseElements(string pauseState, bool isVisibleFinishButton){
		pauseButton.GetComponentInChildren<Text> ().text = pauseState;
		finishButton.gameObject.SetActive(isVisibleFinishButton);
	}
	
	public void BeginSubtitles(List<string> songLyric, AudioSource clip){
		lyricSync.BeginDialogue(songLyric, clip);
	}

	public void SetSongName(string name){
		songNameText.text = name;
	}
}
