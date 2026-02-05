using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
	[SerializeField] private Button PlayButton;
	[SerializeField] private Button QuitButton;

	public void OnPlayButtonClicked()
	{
		Loader.LoadScene(Scene.Game);
	}

	public void OnQuitButtonClicked()
	{
		Application.Quit();
	}
}
