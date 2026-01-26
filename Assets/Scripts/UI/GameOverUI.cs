using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI RecipesDeliveredCountText;

	private void Start()
	{
		GameManager.Instance.OnStateChanged += OnStateChanged;
		Hide();
	}

	private void OnStateChanged(object sender, System.EventArgs e)
	{
		if (GameManager.Instance.IsGameOver()) {
			Show();
		}
	}

	private void Show()
	{
		RecipesDeliveredCountText.text = DeliveryManager.Instance.DeliveryScore.ToString();
		gameObject.SetActive(true);
	}

	private void Hide()
	{
		gameObject.SetActive(false);
	}
}
