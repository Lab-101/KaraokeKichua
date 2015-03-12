using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class MusicListController : MonoBehaviour {
	public MusicListUI ui;
	public Button playButton;
	public TextAsset songLyricsAsset;
	public Song selectedSong;
	public TextAsset json;

	private AudioClip selectedClip;
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
		player.Pause();
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
		
	private void HandleSongSelected (string selectedSongUrl)	{
		selectedClip = Resources.Load (selectedSongUrl, typeof(AudioClip)) as AudioClip;
		songLyricsAsset = Resources.Load (selectedSongUrl, typeof(TextAsset)) as TextAsset;
		selectedSong = GetSongFrom(selectedSongUrl);
		player.PlayPreview (selectedClip);
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