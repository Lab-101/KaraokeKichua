using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class MusicListController : MonoBehaviour {
	public MusicListUI ui;
	public Button playButton;
	public Action songStarted;
	public TextAsset songLyricsAsset;
	public Song selectedSong;
	public TextAsset json;
	private AudioClip selectedClip;
	private List<Song> songsList;

	[SerializeField]
	public Player player;

	void Start () {
		ParseJsonData ();

		playButton.onClick.AddListener(HandlePlayActionExecuted);

		ui.songSelected += HandleSongSelected;
		ui.SetSongs (songsList);
	}

	void ParseJsonData (){
		JsonSongsParser parser = new JsonSongsParser ();
		parser.JSONString = json.text;
		songsList = parser.SongsList;
	}

	public void StopSong(){
		player.Stop();
	}

	public void PauseSong(){
		player.Pause();
	}

	public void RestartPlayer(){		
		player.SetActive();
		player.SetSongLengthInSeconds (0.01f);
	}

	public void SetActive(){
		gameObject.SetActive (true);
	}

	public void SetInactive(){
		gameObject.SetActive (false);
	}
		
	private void HandleSongSelected (string selectedSongUrl)	{
		selectedClip = Resources.Load (selectedSongUrl, typeof(AudioClip)) as AudioClip;
		songLyricsAsset = Resources.Load (selectedSongUrl, typeof(TextAsset)) as TextAsset;
		selectedSong = GetSongFrom(selectedSongUrl);
		player.PlayPreview (selectedClip);
	}
	
	private void HandlePlayActionExecuted(){
		if(songStarted != null){
			songStarted();
			player.SetActive ();
			player.PlaySong(selectedClip);
		}
	}
	
	private Song GetSongFrom(string selectedSongUrl){
		foreach (Song song in songsList){
			if(song.urlSong == selectedSongUrl)
				return song;
		}
		
		throw new Exception ("Palabra no encontrada");
	}
}