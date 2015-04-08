using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Level : MonoBehaviour {
	//Data level
	public int numberLevel;
	public string nameLevel;
	[SerializeField]
	private string introduction;
	[SerializeField]
	private KaraokeController karaoke;
	[SerializeField]
	private List<Activity> activities;

	//GUI objects
	[SerializeField]
	private Button activityButtonPrefab;
	[SerializeField]
	private GameObject activityList;
	[SerializeField]
	private Button levelButton;
	[SerializeField]
	private Text levelName;

	private Map map;

	void Awake(){
		map = new Map ();
		SetUpActivities ();
		ClearActivitysList ();
		levelButton.onClick.AddListener(delegate {
			map.SetNumberCurrentLevel (numberLevel);
			map.IdentifyCurrentLevel (levelButton);
			HandleLevelButtonClicked ();
		} );
	}

	private void SetUpActivities(){	
		int index = 0;
		foreach (Activity activity in activities) {
			if(activity != null){
				activity.SetLevel(numberLevel);
				activity.ResetData();
				DrawActivity(activity, index);
				index++;
				levelName.text = "Nivel: "+ numberLevel ;
				karaoke.SetHeaderInfo(numberLevel, nameLevel);
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
			name =  GameSettings.Instance.wordActivityName + "\n" + GameSettings.Instance.wordActivityInstruction;
		if(activity is WriteActivity)
			name = GameSettings.Instance.writeActivityName + "\n" + GameSettings.Instance.writeActivityInstruction;

		return name;
	}
	
	private void HandleClickButtonActivity (GameObject activityButton) {
		foreach (Activity activity in activities) {
			if(activityButton.name == GetActivityType(activity)){
				StopPreviewWordActivity();
				activity.StartActivity();
			}
		}
	}

	private void ShowActivitysList () {
		ClearActivitysList ();
		SetUpActivities ();
	}

	private void ClearActivitysList () {
		foreach(Transform  child in activityList.transform ) {
			Destroy (child.gameObject);
		}
	}

	private void HandleLevelButtonClicked () {
		ShowActivitysList ();
		PlayPreviewWordActivity ();
	}

	private WordActivity FindWordActivity(){
		foreach (Activity activity in activities) {
			if(activity is WordActivity)
				return (WordActivity) activity;
		}
		return null;
	}

	private void PlayPreviewWordActivity(){
		WordActivity wordActivity = FindWordActivity ();
		if (wordActivity != null)
			wordActivity.PlayPreview ();
	}

	private void StopPreviewWordActivity(){
		WordActivity wordActivity = FindWordActivity ();
		if (wordActivity != null)
			wordActivity.StopSong ();
	}
}
