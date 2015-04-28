using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic; 
using UnityEngine.UI;

public class KaraokeController : MonoBehaviour {
	public Button finishButton;
	public Image pausePanel;
	public Button pauseButton;
	public Button translationButton;
	public Button returnButton;
	public LyricSyncManager lyricSync;
	[SerializeField]
	private Text levelNameText;
	[SerializeField]
	private Text songNameText;
	[SerializeField]
	private GameObject translatedSongUI;


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

		translationButton.onClick.AddListener(delegate {
			translatedSongUI.gameObject.SetActive(true);
		});

		returnButton.onClick.AddListener(delegate {
			translatedSongUI.gameObject.SetActive(false);
		});
	}
	
	public void SetActive(){
		gameObject.SetActive (true);
	}
	
	public void SetInactive(){
		gameObject.SetActive (false);
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
		pausePanel.gameObject.SetActive (isVisibleFinishButton);
		translationButton.gameObject.SetActive(isVisibleFinishButton);
	}
	
	public void BeginSubtitles(List<string> songLyric, AudioSource clip){
		levelNameText.text = "<color=#E5C507FF>NIVEL " + GameSettings.Instance.nameLevel[0] + "</color><size=" + (Screen.width/16) + "><color=#FFFFFFFF><b> " + GameSettings.Instance.nameLevel[1] + "</b></color></size>";
		lyricSync.BeginDialogue(songLyric, clip);
	}

	public void SetSongName(string name){
		songNameText.text = name;
	}
}
