using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Map : MonoBehaviour {

	[SerializeField]
	private Button levelOneButton;
	[SerializeField]
	private Button levelTwoButton;
	[SerializeField]
	private Button levelThreeButton;
	[SerializeField]
	private Button levelFourButton;
	[SerializeField]
	private Button levelFiveButton;
	[SerializeField]
	private Button levelSixButton;
	[SerializeField]
	private Button levelSevenButton;
	[SerializeField]
	private Button levelEigthButton;
	[SerializeField]
	private Button levelNineButton;
	[SerializeField]
	private Button levelTenButton;

	private int numberCurrentLevel;

	void Start()
	{
		IdentifyInactiveLevels ();
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

	public void IdentifyInactiveLevels ()
	{
		levelOneButton.image.color = Color.grey;
		levelTwoButton.image.color = Color.grey;
		levelThreeButton.image.color = Color.grey;
		levelFourButton.image.color = Color.grey;
		levelFiveButton.image.color = Color.grey;
		levelSixButton.image.color = Color.grey;
		levelSevenButton.image.color = Color.grey;
		levelEigthButton.image.color = Color.grey;
		levelNineButton.image.color = Color.grey;
		levelTenButton.image.color = Color.grey;
	}

}
