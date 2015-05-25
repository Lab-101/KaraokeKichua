using UnityEngine;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic; 
using UnityEngine.UI;

public class KaraokeController : MonoBehaviour {
	public Button finishButton;
	public Image pausePanel;
	public Button pauseButton;
	public Button translationButton;
	public Button returnButton;
	public Text translationSongText;
	public LyricSyncManager lyricSync;
	[SerializeField]
	private Text levelNameText;
	[SerializeField]
	private Text songNameText;
	private string songNameString;
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
			StartCoroutine(ReadTraslate());
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
	
	public void BeginSubtitles(List<string> songLyricRegular, List<string> songLyricAlternative,  AudioSource clip){
		levelNameText.text = "<color=#E5C507FF>NIVEL " + GameSettings.Instance.nameLevel[0] + "</color><size=" + (Screen.width/16) + "><color=#FFFFFFFF><b> " + GameSettings.Instance.nameLevel[1] + "</b></color></size>";
		lyricSync.BeginDialogue(songLyricRegular, songLyricAlternative, clip);
	}
	
	public void SetSongName(string name){
		songNameText.text = name;
		songNameString = name;
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

	private IEnumerator ReadTraslate(){
		string text = "";
		WWW data = new WWW(GetDirectionBySystemOperative("Translations", songNameString));
		yield return data;

		if (string.IsNullOrEmpty (data.error)) {			
			byte [] sourceBytes = data.bytes;
			string fileText = Encoding.Default.GetString (sourceBytes);
			translationSongText.text = fileText.ToString();
		} else {			
			translationSongText.text = "Archivo de traduccion no encontrado";
		}

		translatedSongUI.gameObject.SetActive(true);
	}

	private string GetDirectionBySystemOperative (string pathFile, string fileName){
		if (Application.platform == RuntimePlatform.Android) 
			return Application.streamingAssetsPath + "/" + pathFile + "/" + fileName + ".txt";
		else if (Application.platform == RuntimePlatform.IPhonePlayer) 
			return "file://" + Application.streamingAssetsPath + "/" + pathFile + "/" + WWW.EscapeURL (fileName).Replace ("+", "%20") + ".txt";
		else 
			return "file://" + Application.streamingAssetsPath + "/" + pathFile + "/" + fileName + ".txt";
	}
}

