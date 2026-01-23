using System;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
	public event EventHandler OnPlateSpawned;
	public event EventHandler OnPlateRemoved;

	[SerializeField] private KitchenObjectSO PlateSO;
	[SerializeField] private float PlateSpawnTime = 4f;
	[SerializeField] private int MaxPlates = 4;
	private float spawnPlateTimer;
	private int platesAmount;

	

	private void Update()
	{
		if (platesAmount < MaxPlates) {
			spawnPlateTimer += Time.deltaTime;
			if (spawnPlateTimer >= PlateSpawnTime) {
				OnPlateSpawned?.Invoke(this, EventArgs.Empty);
				platesAmount++;
				spawnPlateTimer = 0;
			}
		}
	}

	public override void Interact(Player player)
	{
		if (!player.HasKitchenObject() && platesAmount > 0) {
			platesAmount--;
			KitchenObject.Spawn(PlateSO, player);
			OnPlateRemoved?.Invoke(this, EventArgs.Empty);
		}
	}
}
