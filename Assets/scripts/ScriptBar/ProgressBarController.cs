using UnityEngine;
using UnityEngine.UI;

public class  ProgressBarController : MonoBehaviour
{
	[SerializeField]
	private RectTransform fillerCharge;
	private float maxBarWidth;
	private float XOffset;

	void Awake()
	{
		maxBarWidth = fillerCharge.rect.width;
	    XOffset = (transform.GetComponent<RectTransform>().rect.width - fillerCharge.rect.width) /2 ;
	}

	public void SetFillerSize(int levelActivity)
	{
		float barFulled = levelActivity * maxBarWidth / 12 ;
		fillerCharge.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, XOffset, barFulled);
	}
}