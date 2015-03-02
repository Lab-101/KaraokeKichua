using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class KaraokeController : MonoBehaviour {	
	public Button finishButton;
	public Button pauseButton;
	public Action songPreview;
	public Action songPause;
	public LyricSyncManager lyricSync;
	public string songLyricsText;
	
	void Start () {
		finishButton.onClick.AddListener(delegate {
			if(songPreview != null){
				songPreview();
				pauseButton.GetComponentInChildren<Text> ().text = "Pause";
			}
		});
		pauseButton.onClick.AddListener(delegate {
			if(songPause != null){
				songPause();
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
