using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
	[SerializeField] private Image barImage;
	[SerializeField] private CuttingCounter cuttingCounter;

	private void Start()
	{
		cuttingCounter.OnCuttingProgressChanged += OnCuttingProgressChanged;
		barImage.fillAmount = 0;
		Hide();
	}

	private void OnCuttingProgressChanged(object sender, CuttingCounter.OnCuttingProgressChagedEventArgs e)
	{
		barImage.fillAmount = e.CuttingProgressNormalized;
		if (e.CuttingProgressNormalized == 0f || e.CuttingProgressNormalized == 1f) {
			Hide();
		} else {
			Show();
		}
	}

	private void Show()
	{
		gameObject.SetActive(true);
	}

	private void Hide()
	{
		gameObject.SetActive(false);
	}
}
