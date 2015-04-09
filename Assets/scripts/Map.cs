using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour {
	
	[SerializeField]
	private int indexLevelSelected;
	[SerializeField]
	private List<Button> listButtonLevel;
	[SerializeField]
	private List<Level> levels;

	void Start(){
		SetupLevelButton ();
	}

	private void SelectLevel(int indexLevel){
		SetLevelAsNotSelected (indexLevelSelected);
		SetLevelAsSelected (indexLevel);
		indexLevelSelected = indexLevel;
	}

	private void SetLevelAsSelected(int index){
		listButtonLevel [index].image.color = Color.green;
	}

	private void SetLevelAsNotSelected(int index){
		listButtonLevel [index].image.color = Color.grey;
	}

	private void UnlockLevelButton(int index){
		listButtonLevel [index].interactable = true;
	}

	private void LockLevelButton(int index){
		listButtonLevel [index].interactable = false;		
	}

	private bool IsTheLevelExist (int index) {
		return index < levels.Count;
	}

	private void UnlockOrLockButtonLevel(int index)	{
		if (levels [index].isUnlocked)
			UnlockLevelButton (index);
		else
			LockLevelButton (index);
	}

	private void SelectOrOrNotSelectButtonLevel(int index)	{
		if (indexLevelSelected == index)
			SetLevelAsSelected (index);
		else
			SetLevelAsNotSelected (index);
	}

	private void AddActionToButtonLevel(int index){
		Action levelClicked = levels [index].BeginLevel;
		listButtonLevel [index].onClick.AddListener (delegate {
			levelClicked ();
		});
	
	}

	private void SetupLevelButton (){
		for (int indexButtonLevel = 0; indexButtonLevel < listButtonLevel.Count ; indexButtonLevel++) {
			if (IsTheLevelExist (indexButtonLevel)) {
				UnlockOrLockButtonLevel (indexButtonLevel);
				SelectOrOrNotSelectButtonLevel(indexButtonLevel);
				AddActionToButtonLevel(indexButtonLevel);
			} else {
				LockLevelButton (indexButtonLevel);
			}
		}
	}

}
