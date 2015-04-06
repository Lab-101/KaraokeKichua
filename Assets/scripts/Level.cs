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
	[SerializeField]
	private Button selectLevel;

	void Awake(){
		SetUpActivities ();
		ClearActivitysList ();
		selectLevel.onClick.AddListener(() => ShowActivitysList());
	}

	private void SetUpActivities(){	
		int index = 0;
		foreach (Activity activity in activities) {
			if(activity != null){
				activity.SetLevel(numberLevel);
				DrawActivity(activity, index);
				index++;
			}
		}
	}

	private void DrawActivity (Activity activity, int index){
		Button newItem = Instantiate(activityButtonPrefab) as Button;
		newItem.name = GetActivityType(activity);
		newItem.transform.GetChild(0).GetComponent<Text>().text = GetActivityName(activity);
		newItem.transform.SetParent(activityList.gameObject.transform, false);	
		newItem.onClick.AddListener(delegate {
			HandleClickButtonActivity(newItem.gameObject);
		});
	}

	private string GetActivityType(Activity activity){
		return activity.GetType ().ToString();
	}

	private string GetActivityName (Activity activity){
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

	private void ShowActivitysList ()
	{
		ClearActivitysList ();
		SetUpActivities ();
	}

	private void ClearActivitysList ()
	{
		foreach(Transform  child in activityList.transform ) {
			Destroy (child.gameObject);
		}
	}
}
