using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
	private AudioSource audioSource;
	[SerializeField] private StoveCounter stoveCounter;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}

	private void Start()
	{
		stoveCounter.OnStateChanged += OnStoveCounterStateChanged;
	}

	private void OnStoveCounterStateChanged(object sender, OnStoveStateChangedEventArgs e)
	{
		var isOn = e.State == StoveCounter.State.Frying || e.State == StoveCounter.State.Fried;
		if (isOn) {
			audioSource.Play();
		} else { 
			audioSource.Pause();
		}
	}
}
