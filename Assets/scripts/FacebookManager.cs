using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class FacebookManager : MonoBehaviour {

	public Button loginButton;
	public Text loadingMessage;
	public Text sessionMessage;
	private string permissions = "public_profile,email,user_friends";

	void Start () {

		loginButton.onClick.AddListener (delegate {
			HandleLoginSelected();
		});
	}
	
	void Update () {

	}
	
	private void HandleLoginSelected () {
		ShowLoading ();
		FB.Init(HandleInitComplete);
	}

	private void HandleInitComplete () {
		if (!FB.IsLoggedIn)
			FB.Login(permissions, HandleLoginComplete);
		HideLoginButton ();
	}

	private void HandleLoginComplete(FBResult result) {
		if (FB.IsLoggedIn) {
			ObtainUserInfo();
		} else {
			ShowLoginButton();
		}
	}

	private void HideLoginButton(){
		loadingMessage.gameObject.SetActive (false);
		loginButton.gameObject.SetActive (false);
	}

	private void ShowLoginButton ()
	{
		loadingMessage.gameObject.SetActive(false);
		loginButton.gameObject.SetActive (true);
	}

	private void ShowLoading(){
		loadingMessage.gameObject.SetActive(true);
		loginButton.gameObject.SetActive (false);
	}

	private void ObtainUserInfo(){
		FB.API("/me?fields=first_name", Facebook.HttpMethod.GET, HandleUserInfoObtained);
	}

	private void HandleUserInfoObtained(FBResult result)
	{
		if (result.Error != null)
			Debug.Log("Error Response:\n" + result.Error);
		else if (!FB.IsLoggedIn)
			Debug.Log("Login cancelled by Player");
		else{
			IDictionary dict = Facebook.MiniJSON.Json.Deserialize(result.Text) as IDictionary;
			ShowLoggedInMessage(dict["first_name"].ToString());
		}
	}

	private void ShowLoggedInMessage(string message)
	{
		HideLoginButton ();
		sessionMessage.gameObject.SetActive (true);
		sessionMessage.text = sessionMessage.text + " " + message;
	}
}