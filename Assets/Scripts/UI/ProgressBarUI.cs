using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
	[SerializeField] private Image barImage;
	[SerializeField] private GameObject ProgressOwnerObject;

	private IProgressBarOwner progressOwner;

	private void Start()
	{
		progressOwner = ProgressOwnerObject.GetComponent<IProgressBarOwner>();
		if (progressOwner is null) {
			Debug.LogError($"Game object {ProgressOwnerObject.name} does not have IProgressBarOwner component!!!!");
		}
		progressOwner.OnProgressChanged += OnCuttingProgressChanged;
		barImage.fillAmount = 0;
		Hide();
	}

	private void OnCuttingProgressChanged(object sender, IProgressBarOwner.OnProgressChagedEventArgs e)
	{
		barImage.fillAmount = e.ProgressNormalized;
		if (e.ProgressNormalized == 0f || e.ProgressNormalized == 1f) {
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
