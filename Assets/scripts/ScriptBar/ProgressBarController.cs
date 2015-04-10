using UnityEngine;
using UnityEngine.UI;

public class  ProgressBarController : MonoBehaviour
{
	[SerializeField]
	private RectTransform fillerCharge;

	public void SetFillerSize(int levelActivity, int window)
	{
		float relationNumber = (window==1)? 227f : 465f;
		float maxBarWidth = (relationNumber / 12f) * levelActivity - relationNumber;
		fillerCharge.localPosition = new Vector2 (maxBarWidth , 0);
		fillerCharge.sizeDelta = new Vector2 ((maxBarWidth*2) , 0);
	}
}