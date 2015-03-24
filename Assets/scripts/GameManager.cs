using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public MusicListController musicList;
	public KaraokeController karaoke;
	public WriteActivityController writeActivity;	
	public ResultsController results;
	public GameState gameState;

	private bool IsSelectingSong 
	{
		get{
			return gameState == GameState.SelectingSong;
		}
	}

	private bool IsPlayingSong
	{
		get{
			return gameState == GameState.PlayingSong;
		}
	}

	private bool IsPlayingWriteActivity {
		get{
			return gameState == GameState.WriteActivitySong;
		}
	}

	private bool IsShowingResults {
		get{
			return gameState == GameState.ShowingResults;
		}
	}
	
	void Start () {
		gameState = GameState.SelectingSong;
		musicList.SongStarted += HandleSongStarted;
		musicList.SongFinished += HandleSongFinished;
		karaoke.SongFinished += HandleFinishSongButtonSelected;
		karaoke.SongPaused += HandleSongPaused;
		writeActivity.BackActionExecuted += HandleBackActionExecuted;
		writeActivity.ActivityFinished += HandleActivityFinished;
		results.BackActionExecuted += HandleBackActionExecuted;
		results.RetryActionExecuted += HandleRetryActionExecuted;
	}
	
	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)) 
			Application.Quit(); 
		
		if (IsSelectingSong) {
			musicList.SetActive ();
			karaoke.SetInactive ();
			writeActivity.SetInactive ();
			results.SetInactive ();
		} else if (IsPlayingSong) {
			musicList.SetInactive ();
			karaoke.SetActive ();
			writeActivity.SetInactive ();
			results.SetInactive();
		} else if (IsPlayingWriteActivity) {
			musicList.SetInactive ();
			karaoke.SetInactive ();
			writeActivity.SetActive ();
			results.SetInactive ();
		} else if (IsShowingResults){
			musicList.SetInactive ();
			karaoke.SetInactive ();
			writeActivity.SetInactive ();
			results.SetActive();
		} else {
			karaoke.SetInactive ();
		}
	}
	
	private void HandleSongStarted (){
		gameState = GameState.PlayingSong;
		karaoke.BeginSubtitles (musicList.subtitleList, musicList.GetAudioSourceFromPlayer());
	}
	
	private void HandleSongFinished (){

		if (IsPlayingSong) {
			StartWriteActivity();
		} else if(IsSelectingSong){			
			musicList.RestartPlayer ();
		}
	}

	private void HandleFinishSongButtonSelected (){
		if (IsPlayingSong) {
			StartWriteActivity ();
		}
	}

	private void StartWriteActivity ()
	{
		gameState = GameState.WriteActivitySong;
		musicList.SetInactive ();
		writeActivity.Reset (musicList.selectedSong);
	}
	
	private void HandleSongPaused (){
		musicList.PauseSong ();
	}
	
	private void HandleBackActionExecuted (){
		gameState = GameState.SelectingSong;
		musicList.RestartPlayer ();
	}

	private void HandleActivityFinished() {
		gameState = GameState.ShowingResults;
	}
	private void HandleRetryActionExecuted() {
		gameState = GameState.WriteActivitySong;
		StartWriteActivity ();
	}
}
