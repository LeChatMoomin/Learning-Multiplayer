using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public const string PlayerPrefsSoundEffectVolumeName = "SoundEffectsVolume";

	public static SoundManager Instance {  get; private set; }

	[SerializeField] private AudioRefsSO audioClips;

	private float volume = 1f;

	public float Volume => volume;

	private void Awake()
	{
		Instance = this;
		volume = PlayerPrefs.GetFloat(PlayerPrefsSoundEffectVolumeName, 1f);
	}

	private void Start()
	{
		DeliveryManager.Instance.OnRecipeSuccess += OnRecipeSuccess;
		DeliveryManager.Instance.OnRecipeFailed += OnRecipeFailed;
		CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
		Player.Instance.OnPickedSomething += OnPlayerPickedSomething;
		BaseCounter.OnSomethingPlaced += OnSomethingPlacedOnCounter;
		TrashCounter.OnAnyObjectTrashed += OnAnyObjectTrashed;
	}

	private void OnAnyObjectTrashed(object sender, System.EventArgs e)
	{
		var sourcePoisition = (sender as MonoBehaviour).transform.position;
		PlaySound(audioClips.trash, sourcePoisition);
	}

	private void OnSomethingPlacedOnCounter(object sender, System.EventArgs e)
	{
		var sourcePoisition = (sender as MonoBehaviour).transform.position;
		PlaySound(audioClips.objectDrop, sourcePoisition);
	}

	private void OnPlayerPickedSomething(object sender, System.EventArgs e)
	{
		var sourcePoisition = Player.Instance.transform.position;
		PlaySound(audioClips.objectPickup, sourcePoisition);
	}

	private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
	{
		var sourcePosition = (sender as MonoBehaviour).transform.position;
		PlaySound(audioClips.chop, sourcePosition);
	}

	private void OnRecipeFailed(object sender, System.EventArgs e)
	{
		var sourcePosition = DeliveryManager.Instance.transform.position;
		PlaySound(audioClips.deliveryFail, sourcePosition);
	}

	private void OnRecipeSuccess(object sender, System.EventArgs e)
	{
		var sourcePosition = DeliveryManager.Instance.transform.position;
		PlaySound(audioClips.deliverySuccess, sourcePosition);
	}

	private void PlaySound(AudioClip[] audioClips, Vector3 position, float volumeMultiplier = 1f)
	{
		PlaySound(audioClips[Random.Range(0, audioClips.Length)], position, volumeMultiplier);
	}

	private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f)
	{
		AudioSource.PlayClipAtPoint(audioClip, position, volume * volumeMultiplier);
	}

	public void PlayFootstepSound(Vector3 position, float volume = 1f) 
	{
		PlaySound(audioClips.footstep, position, volume);
	}

	public void ChangeVolume()
	{
		volume += .1f;

		if (volume > 1.01f) {
			volume = 0;
		}

		//Сохраняемся
		PlayerPrefs.SetFloat(PlayerPrefsSoundEffectVolumeName, volume);
		PlayerPrefs.Save();
		//currentVolume %= 1.1f;//залупили значение от 0 до 1
	}
}
