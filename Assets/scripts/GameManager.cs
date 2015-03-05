using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public MusicListController musicList;
	public KaraokeController karaoke;
	public WriteActivityController writeActivity;

	public GameState gameState;

	void Start () {
		gameState = GameState.SelectingSong;
		musicList.songStarted += HandleSongStarted;
		karaoke.songWrite += HandleSongWrite;
		karaoke.songPause += HandleSongPause;
		writeActivity.songPreview += HandleSongPreview;
	}

	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)) 
			Application.Quit(); 
		
		if (gameState == GameState.SelectingSong) {
			musicList.SetActive();
			karaoke.SetInactive();
			writeActivity.SetInactive();
		} else if (gameState == GameState.PlayingSong){
			musicList.SetInactive();
			karaoke.SetActive();
			writeActivity.SetInactive();
		} else {
			musicList.SetInactive();
			karaoke.SetInactive();
			writeActivity.SetActive();
		}

	}

	private void HandleSongStarted (){
		musicList.player.SetActive ();
		gameState = GameState.PlayingSong;
		karaoke.BeginSubtitles (musicList.songLyricsAsset.text, musicList.player.audioSource);
		musicList.PlayCurrentSong ();
		Invoke ("HandleSongWrite", musicList.player.GetSongLength () + 1);
	}

	private void HandleSongWrite (){
		gameState = GameState.WriteActivitySong;
		musicList.player.SetInactive ();
	}

	private void HandleSongPreview (){
		gameState = GameState.SelectingSong;
		musicList.player.SetActive();
	}

	private void HandleSongPause (){
		musicList.PauseSong ();
	}

}
