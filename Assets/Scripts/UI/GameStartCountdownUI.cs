using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI countdownText;

	private void Start()
	{
		GameManager.Instance.OnStateChanged += OnStateChanged;
		Hide();
	}

	private void Update()
	{
		countdownText.text = $"{(int)GameManager.Instance.GetCountdownToStartTimer()+1}";
	}

	private void OnStateChanged(object sender, System.EventArgs e)
	{
		if (GameManager.Instance.IsCountdownToStartActive()) {
			Show();
		} else { 
			Hide();
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
