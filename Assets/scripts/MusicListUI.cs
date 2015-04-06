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
	private Button currentButtonSelected;

	public void SetSongs(List<Song> songs) {
		DrawMusicList (songs);
		SelectFirstSong ();
	}

	public void SelectFirstSong (){
		Button firstSong = buttons[0];
		SelectItemOnEventSystem (transform.GetChild (0).gameObject);
		SelectSongInList (firstSong.gameObject);
	}

	private void DrawMusicList(List<Song> songs){
		
		foreach (Song song in songs){
			Button newItem = Instantiate(itemPrefab) as Button;
			newItem.name = song.songName;
			newItem.transform.SetParent(gameObject.transform, false);	
			newItem.transform.GetChild(0).GetComponent<Text>().text = song.songName;
			newItem.onClick.AddListener(delegate {
				SelectSongInList(newItem.gameObject);
			});
			
			buttons.Add(newItem);
		}
	}

	private void SelectSongInList(GameObject song){
		//ChangeStylesOfButtonSelected (song);
		currentButtonSelected = song.GetComponent<Button>();
		if (songSelected != null) {
			songSelected(currentButtonSelected.name);
		}
	}
	
	private void ChangeStylesOfButtonSelected(GameObject song){		
		if(currentButtonSelected != null)
			currentButtonSelected.image.color = Color.white;
		
		song.GetComponent<Button>().image.color = Color.red;
	}
	
	private void SelectItemOnEventSystem(GameObject item){
		EventSystem.current.SetSelectedGameObject (item);
	}
}