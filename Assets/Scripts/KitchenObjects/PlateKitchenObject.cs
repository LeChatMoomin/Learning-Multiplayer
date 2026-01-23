using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
	[SerializeField] private List<KitchenObjectSO> ValidIngredientsSO;

	private List<KitchenObjectSO> IngredientSOList = new List<KitchenObjectSO>();
	

	public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
	{
		if (ValidIngredientsSO.Contains(kitchenObjectSO) && !IngredientSOList.Contains(kitchenObjectSO)) {
			IngredientSOList.Add(kitchenObjectSO);
			return true;
		}
		return false;
	}
}
