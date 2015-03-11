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
		karaoke.SongFinished += HandleSongFinished;
		karaoke.SongPaused += HandleSongPaused;
		writeActivity.BackActionExecuted += HandleBackActionExecuted;
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
		gameState = GameState.PlayingSong;
		karaoke.BeginSubtitles (musicList.songLyricsAsset.text, musicList.player.audioSource);
		Invoke ("HandleSongFinished", musicList.player.GetSongLength () + 1);
	}

	private void HandleSongFinished (){
		gameState = GameState.WriteActivitySong;
		musicList.player.SetInactive ();
		writeActivity.Reset (musicList.selectedSong);
		CancelInvoke ();
	}

	private void HandleBackActionExecuted (){
		gameState = GameState.SelectingSong;
		RestartPlayer ();
	}

	private void RestartPlayer(){		
		musicList.player.SetActive();
		musicList.player.SetSongLengthInSeconds (0.01f);
	}

	private void HandleSongPaused (){
		musicList.PauseSong ();
	}

}
