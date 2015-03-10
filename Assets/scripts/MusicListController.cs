using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class MusicListController : MonoBehaviour {
	public Player player;
	public MusicListUI ui;
	public Button playButton;
	public Action songStarted;
	public TextAsset songLyricsAsset;
	public Song selectedSong;
	public TextAsset json;
	private List<Song> songsList;

	void Start () {
		ParseJsonData ();
		playButton.onClick.AddListener(delegate {
			if(songStarted != null)
				songStarted();
		});

		ui.songSelected += HandleSongSelected;
		ui.SetSongs (songsList);
	}

	void ParseJsonData ()
	{
		JsonSongsParser parser = new JsonSongsParser ();
		parser.JSONString = json.text;
		songsList = parser.SongsList;
	}

	private void HandleSongSelected (string selectedSongUrl)	{
		AudioClip song = Resources.Load (selectedSongUrl, typeof(AudioClip)) as AudioClip;
		songLyricsAsset = Resources.Load (selectedSongUrl, typeof(TextAsset)) as TextAsset;
		PlaySong (song);
		selectedSong = GetSongFrom(selectedSongUrl);
	}

	private Song GetSongFrom(string selectedSongUrl){
		foreach (Song song in songsList){
			if(song.urlSong == selectedSongUrl)
				return song;
		}
		
		throw new Exception ("Palabra no encontrada");
	}

	private void PlaySong (AudioClip song){
		player.SetAudioClipToAudioSource (song);
		player.Play(player.songStartTime, player.songPlayTime);
	}

	public void PlayCurrentSong(){		
		player.Play(0, player.GetSongLength()+2);
	}

	public void StopSong(){
		player.Stop();
	}

	public void PauseSong(){
		player.Pause();
	}

	public void SetActive(){
		gameObject.SetActive (true);
	}

	public void SetInactive(){
		gameObject.SetActive (false);
	}
}