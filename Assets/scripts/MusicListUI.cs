using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MusicListUI : MonoBehaviour {

	public GameObject itemPrefab;

	void Start () {
	
	}

	void Update () {
	
	}

	public void drawMusicList(List<Song> songs){
		int itemCount = songs.Count; 

		for (int i = 0; i < itemCount; i++){			
			//create a new item, name it, and set the parent
			GameObject newItem = Instantiate(itemPrefab) as GameObject;
			newItem.name = "itemSong_" + i;
			newItem.transform.SetParent(gameObject.transform);	

			newItem.transform.GetChild(0).GetComponent<Text>().text = songs[i].urlSong;

		}
		//Destroy (itemPrefab);
	}



}
