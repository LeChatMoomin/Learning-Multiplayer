using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisuals : MonoBehaviour
{
	[Serializable]
	public struct KitchenObjectSo_GameObject
	{
		public GameObject GameObject;
		public KitchenObjectSO KitchenObjectSO;
	}

	[SerializeField] private PlateKitchenObject plate;
	[SerializeField] private List<KitchenObjectSo_GameObject> SOLinksToGameObjects;

	private void Start()
	{
		plate.OnIngredientAdded += OnIngredientAdded;
		foreach (var item in SOLinksToGameObjects) {
			item.GameObject.SetActive(false);
		}
	}

	private void OnIngredientAdded(object sender, PlateKitchenObject.OnIngrediendAddEventArgs e)
	{
		var so = e.KitchenObjectSO;
		foreach (var link in SOLinksToGameObjects) {
			if (link.KitchenObjectSO == so) {
				link.GameObject.SetActive(true);
			}
		}
	}
}
