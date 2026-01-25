using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public static SoundManager Instance {  get; private set; }

	[SerializeField] private AudioRefsSO audioClips;

	private void Awake()
	{
		Instance = this;
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

	private void PlaySound(AudioClip[] audioClips, Vector3 position, float volume = 1f)
	{
		PlaySound(audioClips[Random.Range(0, audioClips.Length)], position, volume);
	}

	private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
	{
		AudioSource.PlayClipAtPoint(audioClip, position, volume);
	}

	public void PlayFootstepSound(Vector3 position, float volume = 1f) 
	{
		PlaySound(audioClips.footstep, position, volume);
	}
}
