using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class MusicListController : MonoBehaviour {
	public MusicListUI ui;
	public Button playButton;
	public TextAsset songLyricsAsset;
	public Song selectedSong;
	public TextAsset json;
	public List<string> subtitleList;
	
	private AudioClip selectedClip;
	private float timeSelectedClip;
	private List<Song> songsList;
	
	[SerializeField]
	private Player player;
	
	public Action SongStarted {
		get;
		set;
	}
	
	public Action SongFinished {
		get;
		set;
	}
	
	void Start () {
		ParseJsonData ();
		playButton.onClick.AddListener(HandlePlayActionExecuted);
		ui.songSelected += HandleSongSelected;
		player.PlayFinished += HandlePlayFinished;
		ui.SetSongs (songsList);
	}
	
	void ParseJsonData (){
		JsonSongsParser parser = new JsonSongsParser ();
		parser.JSONString = json.text;
		songsList = parser.SongsList;
	}
	
	public AudioSource GetAudioSourceFromPlayer(){
		return player.audioSource;
	}
	
	
	public void PauseSong(){		
		if (player.IsPlaying()) {
			Debug.Log("From Resume to Pause");
			player.Pause ();
			player.SetInactive();
		} else {
			player.SetActive();
			player.Resume();
			Debug.Log("From Pause to Resume");
		}
	}
	
	public void RestartPlayer(){		
		player.SetActive();
		player.SetSongLengthInSeconds (0.01f);
	}
	
	public void PlayPreview(){		
		player.PlayPreview (selectedClip);
	}
	
	public void SetActive(){
		gameObject.SetActive (true);
	}
	
	public void SetInactive(){
		gameObject.SetActive (false);
	}
	
	public void SetInactivePlayer(){
		player.SetInactive ();
	}

	public void SetActivePlayer(){
		player.SetActive ();
	}
	
	private void HandleSongSelected (string selectedSongUrl)	{
		selectedClip = Resources.Load (selectedSongUrl, typeof(AudioClip)) as AudioClip;
		GetSubtitlesFormFile (selectedSongUrl);
		selectedSong = GetSongFrom(selectedSongUrl);
		player.PlayPreview (selectedClip);
	}
	
	private void GetSubtitlesFormFile (string songName){
		string songNamePath = GetDirectionBySystemOperative (songName);
		SubtitleLoader loader = new SubtitleLoader ();
		loader.SubtitlesObtained += HandleSubtitlesObtained;
		loader.URL = songNamePath;
		StartCoroutine (loader.Start());
	}

	private void HandleSubtitlesObtained(string subtitle)
	{
		subtitleList = new List<string>();
		foreach(string line in subtitle.Split('\n')) 
		{
			if(line.Contains("Dialogue: "))
			{
				Debug.Log(line);
				subtitleList.Add(line);
			}
		}
	}
	
	private string GetDirectionBySystemOperative (string name){
		Debug.Log ("Este es el streaming!!! " + Application.streamingAssetsPath + " en " + Application.platform);
		if (Application.platform == RuntimePlatform.Android) 
			return Application.streamingAssetsPath + "/" + name + ".ass";
		else if (Application.platform == RuntimePlatform.IPhonePlayer) 
			return Application.streamingAssetsPath + "/" + name + ".ass";
		else 
			return "file://" + Application.streamingAssetsPath + "/" + name + ".ass";
	}
	
	private void HandlePlayActionExecuted(){
		if(SongStarted != null){
			SongStarted();
			player.SetActive ();
			player.PlaySong(selectedClip);
		}
	}
	
	private void HandlePlayFinished (){
		if (SongFinished != null)
			SongFinished ();
	}
	
	private Song GetSongFrom(string selectedSongUrl){
		foreach (Song song in songsList){
			if(song.urlSong == selectedSongUrl)
				return song;
		}
		
		throw new Exception ("Palabra no encontrada");
	}
}

class SubtitleLoader {
	public string URL;
	public Action<string> SubtitlesObtained;

	public IEnumerator Start() {
		Debug.Log (URL);
		WWW www = new WWW(URL);
		yield return www;
		if (SubtitlesObtained != null)
			SubtitlesObtained(www.text);
	}
}