using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
	[SerializeField] private Button PlayButton;
	[SerializeField] private Button QuitButton;

	private void Awake()
	{
		Time.timeScale = 1.0f;
	}

	public void OnPlayButtonClicked()
	{
		Loader.LoadScene(Scene.Game);
	}

	public void OnQuitButtonClicked()
	{
		Application.Quit();
	}
}
