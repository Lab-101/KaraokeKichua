using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Level : MonoBehaviour {
	//Data level
	public int numberLevel;
	[SerializeField]
	private string introduction;
	[SerializeField]
	private List<Activity> activities;

	//GUI objects
	[SerializeField]
	private Button activityButtonPrefab;
	[SerializeField]
	private GameObject activityList;

	void Awake(){
		SetLevelToActivities ();
		DrawActivityList (activities);
	}

	private void SetLevelToActivities(){
		foreach (Activity activity in activities) {
			if(activity != null)
				activity.SetLevel(numberLevel);
		}
	}	

	private void DrawActivityList(List<Activity> activities){	
		int index = 0;
		foreach (Activity activity in activities){
			Button newItem = Instantiate(activityButtonPrefab) as Button;
			newItem.name = GetActivityType(activity);
			newItem.transform.GetChild(0).GetComponent<Text>().text = GetActivityName(activity);
			newItem.transform.SetParent(activityList.gameObject.transform, false);	
			newItem.onClick.AddListener(delegate {
				HandleClickButtonActivity(newItem.gameObject);
			});
			index ++;
		}
	}

	private string GetActivityType(Activity activity){
		return activity.GetType ().ToString();
	}

	private string GetActivityName(Activity activity){
		string name = "";

		if(activity is  WordActivity)
			name = "Karaoke + Actividad de palabreas";
		if(activity is WriteActivity)
			name = "Actividad de escritura";

		return name;
	}
	
	private void HandleClickButtonActivity (GameObject activityButton) {
		foreach (Activity activity in activities) {
			if(activityButton.name == GetActivityType(activity))
				activity.StartActivity();
		}
	}
}
