using UnityEngine;
using UnityEngine.UI;

public class  ProgressBarController : MonoBehaviour
{
	[SerializeField]
	private RectTransform fillerCharge;
	private float maxBarWidth;

	void Awake()
	{
		maxBarWidth = fillerCharge.rect.width;
	}

	public void SetFillerSize(int levelActivity)
	{
		float barFulled = levelActivity * maxBarWidth / 12 ;
		fillerCharge.SetInsetAndSizeFromParentEdge(0, 0, barFulled);
	}
}