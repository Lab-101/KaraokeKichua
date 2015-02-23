using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MusicListUI : MonoBehaviour {
	public MusicListController musicListController;
	public GameObject itemPrefab;
	public int indexItemSelected;

	void Start () {
		drawMusicList (musicListController.songsList.songs);
		if (indexItemSelected < transform.childCount) {
			selectItemOnEventSystem (transform.GetChild (indexItemSelected).gameObject);
			musicListController.playSongInList(indexItemSelected);
		}
	}

	public void drawMusicList(List<Song> songs){
		int itemCount = songs.Count; 

		for (int i = 0; i < itemCount; i++){

			GameObject newItem = Instantiate(itemPrefab) as GameObject;
			newItem.name = "itemSong_" + i;
			newItem.transform.SetParent(gameObject.transform, false);	

			newItem.transform.GetChild(0).GetComponent<Text>().text = songs[i].urlSong;
			newItem.GetComponent<Button>().onClick.AddListener(delegate {
				selectSongInList(newItem);
			});

		}
	}
	
	public void selectSongInList(GameObject song){
		indexItemSelected = getIndexOfSongInList (song);
		musicListController.playSongInList(indexItemSelected);
	}

	public int getIndexOfSongInList(GameObject itemSelected){
		return int.Parse(itemSelected.name.Substring(9));
	}

	public void selectItemOnEventSystem(GameObject item){
		EventSystem.current.SetSelectedGameObject (item);
	}

}
