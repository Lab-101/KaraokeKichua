using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Level : MonoBehaviour {
	//Data level
	public int numberLevel;
	public int scoreLevel;
	public string nameLevel;
	public bool isUnlocked;
	[SerializeField]
	private Sprite imageIntroduction;
	[SerializeField]
	[Multiline]
	private string informationIntroduction;
	[SerializeField]
	private List<Activity> activities;
	[SerializeField]
	private bool introScreenItsOpened;
	private static readonly object syncLock = new object();

	//GUI objects
	[SerializeField]
	private Button activityButtonPrefab;
	private Text numberLevelField;
	private Text nameLevelField;
	[SerializeField]
	private IntroController introController;
	private GameObject activityListUI;
	private ProgressBarController barLevel;
	private Button introButton;
	private GameStateBehaviour gameStateBehaviour;
	[SerializeField]
	private List<Button> buttonsActivities;

	public Action<Level> UnlockNextLevel;

	void Awake(){
		FindObjectInScene ();
		SetUpActivities ();
		ClearActivitysList ();

		introButton.onClick.AddListener (delegate {
			gameStateBehaviour.GameState = GameState.ShowingIntro;
		});
	}

	public void BeginLevel () {
		SetUpIntroScreen ();
		OpenIntroScreenFirstTime ();
		ShowActivitysList ();
		PlayPreviewWordActivity ();
		SetUpLevelProperties ();
		ChangeColorOfActivitiesCompleted ();

		if (IsAllActivitiesCompleted()) {
			if (UnlockNextLevel != null)
				UnlockNextLevel (this);
		}
	}
	
	public void StopPreviewWordActivity(){
		WordActivity wordActivity = FindWordActivity ();
		if (wordActivity != null)
			wordActivity.StopSong ();
	}

	public bool IsAllActivitiesCompleted(){
		foreach (Activity activity in activities)
			if (!activity.IsCompleted)
				return false;

		return true;
	}

	public int GetTotalScore(){
		int scoreLevel = 0;
		foreach (Activity activity in activities)
			scoreLevel += activity.ScoreObtained;

		GameSettings.Instance.scoreByCurrentLevel = scoreLevel;
		return scoreLevel;
	}

	private void FindObjectInScene ()	{
		activityListUI = GameObject.FindGameObjectWithTag ("ActivityList");
		introButton = GameObject.FindGameObjectWithTag ("IntroButton").GetComponent (typeof(Button)) as Button;
		barLevel = GameObject.FindGameObjectWithTag ("ProgressBarLevel").GetComponent (typeof(ProgressBarController)) as ProgressBarController;
		gameStateBehaviour = GameObject.FindGameObjectWithTag ("GameState").GetComponent (typeof(GameStateBehaviour)) as GameStateBehaviour;
		numberLevelField = GameObject.FindGameObjectWithTag ("MainLevelNumber").GetComponent (typeof(Text)) as Text;
		nameLevelField = GameObject.FindGameObjectWithTag ("MainLevelName").GetComponent (typeof(Text)) as Text;
	}

	private void SetUpLevelProperties()	{
		numberLevelField.text = "NIVEL " + numberLevel;
		nameLevelField.text = nameLevel;
		GameSettings.Instance.nameLevel [0] = numberLevel.ToString ();
		GameSettings.Instance.nameLevel [1] = nameLevel;
		scoreLevel = GetTotalScore ();
		barLevel.SetFillerSize (scoreLevel);
	}

	private void OpenIntroScreenFirstTime(){
		lock (syncLock) {
			if (CanOpenIntroScreen ()) { 
				gameStateBehaviour.GameState = GameState.ShowingIntro;
				introScreenItsOpened = true;
			}
		}
	}

	public bool CanOpenIntroScreen ()	{
		return introController != null && !introScreenItsOpened;
	}

	private void SetUpIntroScreen(){
		introScreenItsOpened = LevelDataPersistent.IsLevelIntroOpened (numberLevel);
		introController.SetSpriteImage (imageIntroduction);
		introController.SetLevelTittle (numberLevel, nameLevel);
		introController.SetInformation (informationIntroduction);

		if(!introScreenItsOpened)
			introController.ContinueButtonClicked = HandleFirstTimeContinueButtonClicked;
		else
			introController.ContinueButtonClicked = HandleContinueButtonClicked;
	}

	private void SetUpActivities(){	
		int index = 0;
		foreach (Activity activity in activities) {
			if(activity != null){
				activity.SetLevel(numberLevel);
				activity.ResetData();
				activity.ActivityCompleted = HandleActivityCompleted;
				DrawActivity(activity, index);
				index++;
			}
		}
	}

	private void DrawActivity (Activity activity, int index){
		Button newItem = Instantiate(activityButtonPrefab) as Button;
		newItem.name = GetActivityType(activity);
		newItem.transform.FindChild("Title").GetComponent<Text>().text = GetActivityName(activity, 0);
		newItem.transform.FindChild("Description").GetComponent<Text>().text = GetActivityName(activity, 1);
		newItem.transform.SetParent(activityListUI.gameObject.transform, false);	
		newItem.onClick.AddListener(delegate {
			HandleClickButtonActivity(newItem.gameObject);
		});
		buttonsActivities.Add (newItem);
		if (activity.IsDataFound ()) {
			newItem.interactable = true;
		} else {
			newItem.interactable = false;
		}
	}

	private string GetActivityType(Activity activity){
		return activity.GetType ().ToString();
	}

	private string GetActivityName (Activity activity, int index){
		string name = "";

		if(activity is  WordActivity)
			name =  GameSettings.Instance.wordActivityTag[index];
		if(activity is WriteActivity)
			name = GameSettings.Instance.writeActivityTag[index];
		if(activity is ImageActivity)
			name = GameSettings.Instance.imageActivityTag[index];
		if(activity is PhraseActivity)
			name = GameSettings.Instance.phraseActivityTag[index];

		return name;
	}

	private void ShowActivitysList () {
		ClearActivitysList ();
		SetUpActivities ();
	}

	private void ClearActivitysList () {
		buttonsActivities = new List<Button>();
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
		
	private void ChangeColorOfActivitiesCompleted(){
		for (int index = 0; index < activities.Count; index++) {
			Activity activity = activities [index];
			if(activity.IsCompleted)
				SetColorCompleteToBarButton(buttonsActivities[index]);
		}
	}

	private Transform GetImageBarFromActivityButton(Button activityButton){
		return activityButton.transform.FindChild("ImageBar");
	}

	private void SetColorCompleteToBarButton(Button button){
		Transform transformImage = GetImageBarFromActivityButton (button);

		Image imageBar =  transformImage.GetComponent<Image>() as Image;
		Color color = new Color();
		color.r = 0;
		color.g = 255;
		color.b = 0;
		color.a = 1;
		imageBar.color = color;
		
		RectTransform rectTransform = transformImage.GetComponent<RectTransform> () as RectTransform;
		Vector2 anchorMin = rectTransform.anchorMin;
		anchorMin.x = 0;
		Vector2 anchorMax = rectTransform.anchorMax;
		anchorMax.x = 1;

		rectTransform.anchorMin = anchorMin;
		rectTransform.anchorMax = anchorMax;
	}

	private void HandleFirstTimeContinueButtonClicked (){
		introController.ContinueButtonClicked = HandleContinueButtonClicked;

		WordActivity wordActivity = FindWordActivity ();
		if (wordActivity != null && wordActivity.IsDataFound ()) {
			wordActivity.StopSong ();
			wordActivity.StartActivity ();
		} else {
			gameStateBehaviour.GameState = GameState.SelectingLevel;
			LevelData data = new LevelData();
			data.level = numberLevel;
			data.isUnlocked = true;
			data.isIntroOpened = true;
			LevelDataPersistent.SaveLevelData(data);
		}
	}

	private void HandleContinueButtonClicked (){
		gameStateBehaviour.GameState = GameState.SelectingLevel;
	}

	private void HandleClickButtonActivity (GameObject activityButton) {
		foreach (Activity activity in activities) {
			if(activityButton.name == GetActivityType(activity)){
				StopPreviewWordActivity();
				activity.StartActivity();
			}
		}
	}
	
	private void HandleActivityCompleted ()	{
		scoreLevel = GetTotalScore ();
		barLevel.SetFillerSize (scoreLevel);
		ChangeColorOfActivitiesCompleted ();

		if (IsAllActivitiesCompleted()) {
			if (UnlockNextLevel != null)
				UnlockNextLevel (this);
		}
	}
}