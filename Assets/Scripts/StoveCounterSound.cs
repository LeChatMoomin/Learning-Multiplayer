using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
	private AudioSource audioSource;
	[SerializeField] private StoveCounter stoveCounter;

	private bool isBurning = false;
	private float timer;
	private float burnWarningTime = .5f;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}

	private void Start()
	{
		stoveCounter.OnStateChanged += OnStoveCounterStateChanged;
	}

	private void Update()
	{
		if (isBurning) {
			timer += Time.deltaTime;
			if (timer > burnWarningTime) {
				timer = 0;
				SoundManager.Instance.PlayWarningSound(transform.position);
			}
		}
	}

	private void OnStoveCounterStateChanged(object sender, OnStoveStateChangedEventArgs e)
	{
		var isOn = e.State == StoveCounter.State.Frying || e.State == StoveCounter.State.Fried;
		if (isOn) {
			audioSource.Play();
		} else { 
			audioSource.Pause();
		}
		isBurning = e.State == StoveCounter.State.Fried;
	}
}
