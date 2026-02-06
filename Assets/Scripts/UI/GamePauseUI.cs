using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
	public static GamePauseUI Instance;

	[SerializeField] private Button ResumeButton;
	[SerializeField] private Button MainMenuButton;
	[SerializeField] private Button OptionsButton;

	private void Awake()
	{
		Instance = this;
		ResumeButton.onClick.AddListener(() => {
			GameManager.Instance.ToggleGamePause();
		});
		MainMenuButton.onClick.AddListener(() => {
			Loader.LoadScene(Scene.MainMenu);
		});
		OptionsButton.onClick.AddListener(() => {
			OptionsUI.Instance.Show();
			Hide();
		});
	}

	private void Start()
	{
		GameManager.Instance.OnGamePaused += OnGamePaused;
		GameManager.Instance.OnGameUnpaused += OnGameUnpaused;
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

	public void Show()
	{
		gameObject.SetActive(true);
	}

	private void Hide()
	{
		gameObject.SetActive(false);
	}
}
