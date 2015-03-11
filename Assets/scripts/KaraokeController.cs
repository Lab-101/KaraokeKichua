using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class KaraokeController : MonoBehaviour {	
	public Button finishButton;
	public Button pauseButton;
	public LyricSyncManager lyricSync;
	public string songLyricsText;

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
				pauseButton.GetComponentInChildren<Text> ().text = "Pause";
			}
		});
		pauseButton.onClick.AddListener(delegate {
			if(SongPaused != null){
				SongPaused();
				ChangeTextPauseButton();
			}
		});
	}

	public void SetActive(){
		gameObject.SetActive (true);
	}
	
	public void SetInactive(){
		gameObject.SetActive (false);
	}
	
	private void ChangeTextPauseButton(){
		if(pauseButton.GetComponentInChildren<Text> ().text == "Pause")
			pauseButton.GetComponentInChildren<Text> ().text = "Continuar";
		else
			pauseButton.GetComponentInChildren<Text> ().text = "Pause";
	}

	public void BeginSubtitles(string songLyric, AudioSource clip){
		lyricSync.BeginDialogue(songLyric, clip);
	}
}
