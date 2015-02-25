using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class MusicListUI : MonoBehaviour {

	public Button itemPrefab;
	public Action <string> songSelected;
	private List <Button> buttons = new List<Button>();

	public void SetSongs(List<Song> songs) {
		DrawMusicList (songs);
		SelectItemOnEventSystem (transform.GetChild (0).gameObject);
		SelectFirstSong ();
	}

	private void DrawMusicList(List<Song> songs){

		foreach (Song song in songs){

			Button newItem = Instantiate(itemPrefab) as Button;
			newItem.name = song.urlSong;
			newItem.transform.SetParent(gameObject.transform, false);	
			newItem.transform.GetChild(0).GetComponent<Text>().text = song.urlSong;
			newItem.onClick.AddListener(delegate {
				SelectSongInList(newItem.gameObject);
			});

			buttons.Add(newItem);
		}
	}
	
	public void SelectSongInList(GameObject song){
		if (songSelected != null) {
			songSelected(song.GetComponent<Button>().name);
		}
	}

	public void SelectItemOnEventSystem(GameObject item){
		EventSystem.current.SetSelectedGameObject (item);
	}

	private void SelectFirstSong ()
	{
		Button firstSong = buttons[0];
		SelectSongInList (firstSong.gameObject);
	}
}