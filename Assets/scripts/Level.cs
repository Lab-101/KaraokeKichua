using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level : MonoBehaviour {

	public int numberLevel;
	[SerializeField]
	private string introduction;
	[SerializeField]
	private List<Activity> activities;

	void Awake(){
		SetLevelToActivities ();
	}

	private void SetLevelToActivities(){
		foreach (Activity activity in activities) {
			if(activity != null)
				activity.SetLevel(numberLevel);
		}
	}	
	
	private void OpenActivity (Activity activity) {
		activity.ActivityStarted();
	}

}
