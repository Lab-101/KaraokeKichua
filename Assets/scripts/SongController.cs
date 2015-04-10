using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SongController : MonoBehaviour {
	private AudioClip songAudioClip;
	private float timeSelectedClip;
	private List<string> subtitleList;
	
	[SerializeField]
	private Player player;
	[SerializeField]
	private KaraokeController karaoke;
	[SerializeField]
	protected GameStateBehaviour gameStateBehaviour;
	
	public Action SongStarted {
		get;
		set;
	}
	
	public Action SongFinished {
		get;
		set;
	}
	void Start () {
		player.PlayFinished += HandlePlayFinished;
		karaoke.SongFinished += HandleSongFinished;
		karaoke.SongPaused += HandleSongPaused;
	}
	
	public void SetSong (string songName){
		if (songName != null) {
			songAudioClip = Resources.Load (songName, typeof(AudioClip)) as AudioClip;
			GetSubtitlesFormFile (songName);
		}
	}
	
	public void StartKaraoke(){
		gameStateBehaviour.GameState = GameState.PlayingSong;
		karaoke.BeginSubtitles (subtitleList, GetAudioSourceFromPlayer ());
		player.SetActive ();
		player.PlaySong(songAudioClip);
	}
	
	public void PlayPreview (string selectedSongUrl)	{
		player.PlayPreview (songAudioClip);
	}

	public void StopSong(){
		player.Stop ();
	}

	private AudioSource GetAudioSourceFromPlayer(){
		return player.audioSource;
	}	
	
	private void PauseSong(){		
		if (player.IsPlaying()) {
			player.Pause ();
			player.SetInactive();
		} else {
			player.SetActive();
			player.Resume();
		}
	}
	
	private void RestartPlayer(){
		player.SetActive();
		player.SetSongLengthInSeconds (0.01f);
	}
	
	private void GetSubtitlesFormFile (string songName){
		karaoke.SetSongName (songName);
		string songNamePath = GetDirectionBySystemOperative (songName);
		SubtitleLoader loader = new SubtitleLoader ();
		loader.SubtitlesObtained += HandleSubtitlesObtained;
		loader.URL = songNamePath;
		StartCoroutine (loader.Start());
	}
	
	private string GetDirectionBySystemOperative (string name){
		Debug.Log ("Este es el streaming!!! " + Application.streamingAssetsPath + " en " + Application.platform);
		if (Application.platform == RuntimePlatform.Android) 
			return Application.streamingAssetsPath + "/" + name + ".ass";
		else if (Application.platform == RuntimePlatform.IPhonePlayer) 
			return "file://" + Application.streamingAssetsPath + "/" + WWW.EscapeURL (name).Replace("+","%20") + ".ass";
		else 
			return "file://" + Application.streamingAssetsPath + "/" + name + ".ass";
	}
		
	private void HandleSubtitlesObtained(string subtitle){
		subtitleList = new List<string>();
		foreach(string line in subtitle.Split('\n')) 
			if(line.Contains("Dialogue: "))
				subtitleList.Add(line);
	}
	
	private void HandlePlayFinished (){
		if (gameStateBehaviour.GameState == GameState.PlayingSong)
			OpenWordActivity ();
		else
			RestartPlayer ();
	}
	
	private void HandleSongFinished () {
		OpenWordActivity ();
		RestartPlayer ();
	}
	
	private void HandleSongPaused () {
		PauseSong ();
	}
	
	private void OpenWordActivity(){		
		gameStateBehaviour.GameState = GameState.WordActivity;
	}
	
}