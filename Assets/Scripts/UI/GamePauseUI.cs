using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
	[SerializeField] private Button ResumeButton;
	[SerializeField] private Button MainMenuButton;


	private void Start()
	{
		GameManager.Instance.OnGamePaused += OnGamePaused;
		GameManager.Instance.OnGameUnpaused += OnGameUnpaused;
		ResumeButton.onClick.AddListener(() => {
			GameManager.Instance.ToggleGamePause();
		});
		MainMenuButton.onClick.AddListener(() => {
			Loader.LoadScene(Scene.MainMenu);
		});
		Hide();
	}

	private void OnGameUnpaused(object sender, System.EventArgs e)
	{
		Hide();
	}

	private void OnGamePaused(object sender, System.EventArgs e)
	{
		Show();
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
