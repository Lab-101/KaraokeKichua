using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SongController : MonoBehaviour {
	private string song;
	private AudioClip songAudioClip;
	private float timeSelectedClip;
	private List<string> subtitleListRegular;
	private List<string> subtitleListAlternative;
	
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
			song = songName;
			songAudioClip = Resources.Load (songName, typeof(AudioClip)) as AudioClip;
			GetSubtitlesFormFile (songName);
		}
	}
	
	public void StartKaraoke(){
		karaoke.BeginSubtitles (subtitleListRegular, subtitleListAlternative, GetAudioSourceFromPlayer ());
		player.SetActive ();
		player.PlaySong(songAudioClip);
		gameStateBehaviour.GameState = GameState.PlayingSong;
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
		string pathLyricsRegular = GetDirectionBySystemOperative (songName, "Regular");		
		string pathLyricsAlternative = GetDirectionBySystemOperative (songName, "Alternative");

		SubtitleLoader loaderRegular = new SubtitleLoader ();
		loaderRegular.SubtitlesObtained += HandleSubtitlesObtainedRegular;
		loaderRegular.URL = pathLyricsRegular;
		StartCoroutine (loaderRegular.Start());

		SubtitleLoader loaderAlternative = new SubtitleLoader ();
		loaderAlternative.SubtitlesObtained += HandleSubtitlesObtainedAlternative;
		loaderAlternative.URL = pathLyricsAlternative;
		StartCoroutine (loaderAlternative.Start());
		
		karaoke.SetSongName (songName);
	}
	
	private string GetDirectionBySystemOperative (string name, string languageFolder){
		if (Application.platform == RuntimePlatform.Android) 
			return Application.streamingAssetsPath + "/" + languageFolder + "/" + name + ".ass";
		else if (Application.platform == RuntimePlatform.IPhonePlayer) 
			return "file://" + Application.streamingAssetsPath + "/" + languageFolder + "/" + WWW.EscapeURL (name).Replace ("+", "%20") + ".ass";
		else 
			return "file://" + Application.streamingAssetsPath + "/" + languageFolder + "/" + name + ".ass";
	}
		
	private void HandleSubtitlesObtainedRegular(string subtitle){
		subtitleListRegular = new List<string>();
		foreach(string line in subtitle.Split('\n')) 
			if(line.Contains("Dialogue: "))
				subtitleListRegular.Add(line);
	}

	private void HandleSubtitlesObtainedAlternative (string subtitle){
		subtitleListAlternative = new List<string>();
		foreach(string line in subtitle.Split('\n')) 
			if(line.Contains("Dialogue: "))
				subtitleListAlternative.Add(line);
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