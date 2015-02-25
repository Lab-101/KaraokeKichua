using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject viewCurrent;
	public MusicListController musicList;
	public KaraokeController karaoke;

	public GameState gameState;

	void Start () {
		gameState = GameState.SelectingSong;
		viewCurrent.SetActive (true);
		musicList.songStarted += HandleSongStarted;
		karaoke.songPreview += HandleSongPreview;
	}

	void Update(){
		if (gameState == GameState.SelectingSong) {
			musicList.SetActive();
			karaoke.SetInactive();
		} else {
			musicList.SetInactive();
			karaoke.SetActive();
		}
	}

	private void HandleSongStarted (){
		gameState = GameState.PlayingSong;
		musicList.PlayCurrentSong ();
		Invoke ("HandleSongPreview", musicList.player.GetSongLength () + 2);
	}

	private void HandleSongPreview (){
		gameState = GameState.SelectingSong;
		musicList.ui.SelectFirstSong ();
	}

}
