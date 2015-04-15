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
		UpdateLockStateOfLevels ();
		SetupLevelButton ();
	}

	void Update(){
		if (levelList [indexLevelSelected].IsAllActivitiesCompleted ()) {
			if( indexLevelSelected+1 < levelList.Count){
				int numberLevel = levelList [indexLevelSelected + 1].numberLevel;
				bool introScreenItsOpened = LevelDataPersistent.IsLevelIntroOpened (numberLevel);
				SaveLevelData(numberLevel, true, introScreenItsOpened);
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
		});
	
	}

	private void SetupLevelButton (){
		for (int index = 0; index < buttonLevelList.Count ; index++) {
			if (IsTheLevelExist (index)) {
				UnlockOrLockButtonLevel (index);
				SelectOrOrNotSelectButtonLevel(index);
				AddActionToButtonLevel(index);
			} else {
				LockLevelButton (index);
			}
		}
	}

	private void UpdateLockStateOfLevels(){
		bool isFirstLevel = true;
		foreach (Level level in levelList) {
			if(isFirstLevel){
				bool introScreenItsOpened = LevelDataPersistent.IsLevelIntroOpened (level.numberLevel);
				SaveLevelData(level.numberLevel, true, introScreenItsOpened);
				isFirstLevel = false;
			}

			bool isLevelUnlocked = LevelDataPersistent.IsLevelUnlock (level.numberLevel);
			level.isUnlocked = isLevelUnlocked;
		}
	}

	private void SaveLevelData(int numberLevel, bool isLevelUnlock, bool isLevelIntroOpened){		
		LevelData data = new LevelData();
		data.level = numberLevel;
		data.isUnlocked = isLevelUnlock;
		data.isIntroOpened = isLevelIntroOpened;
		LevelDataPersistent.SaveLevelData(data);
	}
}