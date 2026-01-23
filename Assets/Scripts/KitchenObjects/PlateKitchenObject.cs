using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
	public class OnIngrediendAddEventArgs : EventArgs
	{
		public KitchenObjectSO KitchenObjectSO;
	}

	public event EventHandler<OnIngrediendAddEventArgs> OnIngredientAdded;

	[SerializeField] private List<KitchenObjectSO> ValidIngredientsSO;

	private List<KitchenObjectSO> IngredientSOList = new List<KitchenObjectSO>();

	public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
	{
		if (ValidIngredientsSO.Contains(kitchenObjectSO) && !IngredientSOList.Contains(kitchenObjectSO)) {
			IngredientSOList.Add(kitchenObjectSO);
			OnIngredientAdded?.Invoke(this, new OnIngrediendAddEventArgs { KitchenObjectSO = kitchenObjectSO });
			return true;
		}
		return false;
	}

	public List<KitchenObjectSO> GetIngredientsList()
	{
		return IngredientSOList;
	}
}
