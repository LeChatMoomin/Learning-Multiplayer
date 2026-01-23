using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
	[SerializeField] private StoveCounter Counter;
	[SerializeField] private GameObject stoveOnVisual;
	[SerializeField] private GameObject particlesObject;

	private void Start()
	{
		Counter.OnStateChanged += OnCounterStateChanged;
	}

	private void OnCounterStateChanged(object sender, OnStoveStateChangedEventArgs e)
	{
		var isEffectsVisible = e.State == StoveCounter.State.Frying || e.State == StoveCounter.State.Fried;

		stoveOnVisual.SetActive(isEffectsVisible);
		particlesObject.SetActive(isEffectsVisible);
	}
}
