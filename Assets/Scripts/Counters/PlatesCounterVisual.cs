using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
	[SerializeField] private Transform counterTopPoint;
	[SerializeField] private Transform PlateVisualPrefab;
	[SerializeField] private PlatesCounter PlatesCounter;

	private List<GameObject> PlatesVisuals = new List<GameObject>();

	private void Start()
	{
		PlatesCounter.OnPlateSpawned += OnPlateSpawned;
		PlatesCounter.OnPlateRemoved += OnPlateRemoved;
	}

	private void OnPlateRemoved(object sender, System.EventArgs e)
	{
		var lastPlate = PlatesVisuals[PlatesVisuals.Count - 1];
		PlatesVisuals.Remove(lastPlate);
		Destroy(lastPlate);
	}

	private void OnPlateSpawned(object sender, System.EventArgs e)
	{
		var plateVisual = Instantiate(PlateVisualPrefab, counterTopPoint);
		var plateYOffset = .1f;
		plateVisual.localPosition = new Vector3(0, plateYOffset * PlatesVisuals.Count, 0);
		PlatesVisuals.Add(plateVisual.gameObject);
	}
}
