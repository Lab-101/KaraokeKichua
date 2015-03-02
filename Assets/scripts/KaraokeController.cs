using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class KaraokeController : MonoBehaviour {	
	public Button finishButton;
	public Button pauseButton;
	public Action songPreview;
	public Action songPause;
	public Text lyricText;
	
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
}
