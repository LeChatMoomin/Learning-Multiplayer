using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
	[SerializeField] private BaseCounter Counter;
	[SerializeField] private GameObject[] Visuals;

	private void Start()
	{
		Player.Instance.OnSelectedCounterChanged += OnPlayerSelectedCounterChanged;
	}

	private void OnPlayerSelectedCounterChanged(object sender, SelectedCounterChangedEventArgs e)
	{
		for (int i = 0; i < Visuals.Length; i++) {
			Visuals[i].SetActive(Counter == e.SelectedCounter);
		}

	}
}
