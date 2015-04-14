using UnityEngine;
using UnityEngine.UI;

public class  ProgressBarController : MonoBehaviour
{
	[SerializeField]
	private RectTransform fillerCharge;
	[SerializeField]
	private RectTransform barCharge;

	public void SetFillerSize(int levelActivity)
	{
		barCharge.sizeDelta = new Vector2 ((Screen.width * 0.31f) , (Screen.width * 0.057f));
		barCharge.anchoredPosition = new Vector2 ( 0.0f , 0.0f );
		fillerCharge.sizeDelta = new Vector2 ( barCharge.sizeDelta.x, barCharge.sizeDelta.y );
		float maxBarWidth = (barCharge.sizeDelta.x * levelActivity / 12f);
		fillerCharge.sizeDelta = new Vector2 (maxBarWidth , (Screen.width * 0.057f));
	}
}