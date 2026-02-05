using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
	private static Scene targetScene;

	public static void LoadScene(Scene scene)
	{
		targetScene = scene;
		SceneManager.LoadScene((int)Scene.Loading);
	}

	public static void LoaderCallback()
	{
		SceneManager.LoadScene((int)targetScene);
	}
}

public enum Scene
{
	MainMenu = 0,
	Game = 1,
	Loading = 2,
}
