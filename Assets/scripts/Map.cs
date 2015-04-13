using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour {
	
	[SerializeField]
	private int indexLevelSelected;
	[SerializeField]
	private List<Button> buttonLevelList;
	[SerializeField]
	private List<Level> levelList;

	void Start(){
		SetupLevelButton ();
	}

	void Update(){
		if (levelList [indexLevelSelected].IsAllActivitiesCompleted ()) {
			if( indexLevelSelected+1 < levelList.Count){
				levelList [indexLevelSelected + 1].isUnlocked = true;
				UnlockOrLockButtonLevel (indexLevelSelected + 1);
			}
		}
	}

	private void SelectLevel(int indexLevel){
		SetLevelAsNotSelected (indexLevelSelected);
		SetLevelAsSelected (indexLevel);
		levelList [indexLevel].StopPreviewWordActivity ();
		indexLevelSelected = indexLevel;
	}

	private void SetLevelAsSelected(int index){
		buttonLevelList [index].image.color = Color.green;
	}

	private void SetLevelAsNotSelected(int index){
		buttonLevelList [index].image.color = Color.grey;
	}

	private void UnlockLevelButton(int index){
		buttonLevelList [index].interactable = true;
	}

	private void LockLevelButton(int index){
		buttonLevelList [index].interactable = false;		
	}

	private bool IsTheLevelExist (int index) {
		return index < levelList.Count;
	}

	private void UnlockOrLockButtonLevel(int index)	{
		if (levelList [index].isUnlocked)
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
		Action levelBegun = levelList [index].BeginLevel;	
		buttonLevelList [index].onClick.AddListener (delegate {
			SelectLevel(index);
			levelBegun ();

			SaveLoad.SaveActivityLevelScore(1,1,1);

			foreach (ActivityScore scores in SaveLoad.dataActivitiesScore) {
				Debug.Log("--------" + scores.level + " - " + scores.activity + " - " + scores.score + " - ");
			}
			


		});
	
	}

	private void SetupLevelButton (){
		for (int indexButtonLevel = 0; indexButtonLevel < buttonLevelList.Count ; indexButtonLevel++) {
			if (IsTheLevelExist (indexButtonLevel)) {
				//leer datos nivel
				//SaveLoad.IsLevelUnLock(levelList [index].numberLevel, levelList [index-1].)
				//modificar variable unlock
				UnlockOrLockButtonLevel (indexButtonLevel);
				SelectOrOrNotSelectButtonLevel(indexButtonLevel);
				AddActionToButtonLevel(indexButtonLevel);
			} else {
				LockLevelButton (indexButtonLevel);
			}
		}
	}

}
