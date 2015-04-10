using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Level : MonoBehaviour {
	//Data level
	public int numberLevel;
	public int scoreLevel;
	public string nameLevel;
	public bool isUnlocked;
	[SerializeField]
	private string introduction;
	[SerializeField]
	private KaraokeController karaoke;
	[SerializeField]
	private List<Activity> activities;
	[SerializeField]
	private bool introScreenItsOpened;
	private static readonly object syncLock = new object();

	//GUI objects
	[SerializeField]
	private Button activityButtonPrefab;
	[SerializeField]
	private Text levelName;
	[SerializeField]
	private GameObject introScreen;
	private GameObject activityListUI;
	private ProgressBarController barLevel;

	void Awake(){
		FindObjectInScene ();
		introScreenItsOpened = false;
		SetUpActivities ();
		ClearActivitysList ();
	}

	public void BeginLevel () {
		OpenIntroScreenFirstTime ();
		ShowActivitysList ();
		PlayPreviewWordActivity ();
	}
	
	public void StopPreviewWordActivity(){
		WordActivity wordActivity = FindWordActivity ();
		if (wordActivity != null)
			wordActivity.StopSong ();
	}

	private void FindObjectInScene ()	{
		activityListUI = GameObject.FindGameObjectWithTag ("ActivityList");
		barLevel = GameObject.FindGameObjectWithTag ("ProgressBarLevel").GetComponent (typeof(ProgressBarController)) as ProgressBarController;
	}

	private void OpenIntroScreenFirstTime(){
		lock (syncLock) {
			if (CanOpenIntroScreen ()) { 
				introScreen.SetActive (true);
				introScreenItsOpened = true;
			}
		}
	}

	public bool CanOpenIntroScreen ()	{
		return introScreen != null && !introScreenItsOpened;
	}

	private void SetUpActivities(){	
		int index = 0;
		foreach (Activity activity in activities) {
			if(activity != null){
				activity.SetLevel(numberLevel);
				activity.ResetData();
				DrawActivity(activity, index);
				levelName.text = "Nivel: "+ numberLevel ;
				karaoke.SetHeaderInfo(numberLevel, nameLevel);
				barLevel.SetFillerSize (scoreLevel, 1);
				index++;
			}
		}
	}

	private void DrawActivity (Activity activity, int index){
		Button newItem = Instantiate(activityButtonPrefab) as Button;
		newItem.name = GetActivityType(activity);
		newItem.transform.GetChild(0).GetComponent<Text>().text = GetActivityName(activity);
		newItem.transform.SetParent(activityListUI.gameObject.transform, false);	
		newItem.onClick.AddListener(delegate {
			HandleClickButtonActivity(newItem.gameObject);
		});
		if (activity.IsDataFound ()) {
			newItem.interactable = true;
		} else {
			newItem.interactable = false;
		}
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
		foreach(Transform  child in activityListUI.transform ) {
			Destroy (child.gameObject);
		}
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
		if (wordActivity != null && wordActivity.IsDataFound ())
				wordActivity.PlayPreview ();
	}
}
