using UnityEngine;
using UnityEngine.Rendering;

public class MusicManager : MonoBehaviour
{
	public const string PlayerPrefsMusicVolumeName = "MusicVolume";

	public static MusicManager Instance { get; private set; }
	public float Volume => volume;

	private float volume = .3f;
	private AudioSource audioSource;


	private void Awake()
	{
		Instance = this;
		audioSource = GetComponent<AudioSource>();
		audioSource.volume = volume = PlayerPrefs.GetFloat(PlayerPrefsMusicVolumeName, .3f);
	}

	public void ChangeVolume()
	{
		volume += .1f;

		if (volume > 1.01f) {
			volume = 0;
		}
		audioSource.volume = volume;

		//Сохраняемся
		PlayerPrefs.SetFloat(PlayerPrefsMusicVolumeName, volume);
		PlayerPrefs.Save();
		//currentVolume %= 1.1f;//залупили значение от 0 до 1
	}
}
