using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
	[SerializeField] private ClearCounter ClearCounter;
	[SerializeField] private GameObject VisualsGameObject;

	private void Start()
	{
		Player.Instance.OnSelectedCounterChanged += OnPlayerSelectedCounterChanged;
	}

	private void OnPlayerSelectedCounterChanged(object sender, SelectedCounterChangedEventArgs e)
	{
		VisualsGameObject.SetActive(ClearCounter == e.SelectedCounter);
	}
}
