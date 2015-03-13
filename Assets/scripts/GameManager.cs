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
		musicList.SongStarted += HandleSongStarted;
		musicList.SongFinished += HandleSongFinished;
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
		karaoke.BeginSubtitles (musicList.songLyricsAsset.text, musicList.GetAudioSourceFromPlayer());
	}

	private void HandleSongFinished (){
		if (gameState == GameState.PlayingSong) {
			gameState = GameState.WriteActivitySong;
			musicList.SetInactivePlayer ();
			writeActivity.Reset (musicList.selectedSong);
		} else if(gameState == GameState.SelectingSong){			
			musicList.RestartPlayer ();
			musicList.PlayPreview();
		}
	}

	private void HandleBackActionExecuted (){
		gameState = GameState.SelectingSong;
		musicList.RestartPlayer ();
	}

	private void HandleSongPaused (){
		musicList.PauseSong ();
	}
}