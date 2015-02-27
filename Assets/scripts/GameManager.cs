using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public MusicListController musicList;
	public KaraokeController karaoke;

	public GameState gameState;

	void Start () {
		gameState = GameState.SelectingSong;
		musicList.songStarted += HandleSongStarted;
		karaoke.songPreview += HandleSongPreview;
	}

	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)) 
			Application.Quit(); 
		
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
		karaoke.lyricText.rectTransform.position = new Vector3(-50f, 0f , 100f);
		karaoke.lyricText.text = musicList.songLyricsText;
		musicList.PlayCurrentSong ();
		Invoke ("HandleSongPreview", musicList.player.GetSongLength () + 1);
	}

	private void HandleSongPreview (){
		gameState = GameState.SelectingSong;
		musicList.ui.SelectFirstSong ();
	}

}
