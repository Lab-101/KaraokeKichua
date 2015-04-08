using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Map : MonoBehaviour {

	[SerializeField]
	private Button levelOneButton;
	[SerializeField]
	private Button levelTwoButton;

	private int numberCurrentLevel;

	void Start()
	{
		
		levelOneButton.onClick.AddListener(delegate {
			IdentifyInactiveLevels ();
			IdentifyCurrentLevel(levelOneButton);
		} );
		
		levelTwoButton.onClick.AddListener(delegate {
			IdentifyInactiveLevels ();
			IdentifyCurrentLevel(levelTwoButton);
		} );
	}

	public void SetNumberCurrentLevel(int numberLevel) {
		this.numberCurrentLevel = numberLevel;
	}

	public int GetNumberCurrentLevel() {
		return this.numberCurrentLevel;
	}

	public void IdentifyCurrentLevel (Button levelButton)	{
	    levelButton.image.color = Color.green;
	}

	private void IdentifyInactiveLevels ()
	{
		levelOneButton.image.color = Color.grey;
		levelTwoButton.image.color = Color.grey;
	}

}
