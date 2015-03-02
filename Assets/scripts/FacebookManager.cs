using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FacebookManager : MonoBehaviour {

	public Button loginButton;
	public Text loadingMessage;
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
		
				Debug.Log ("El usuario esta logiao: " + result.ToString ());
				HideLoginButton ();
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
}
