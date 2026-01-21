using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Windows;

public class CuttingCounter : BaseCounter
{
	public class OnCuttingProgressChagedEventArgs : EventArgs
	{
		public float CuttingProgressNormalized;
	}

	[SerializeField] private CuttingRecipeSO[] Recipes;
	private int cuttingProgress;

	public event EventHandler<OnCuttingProgressChagedEventArgs> OnCuttingProgressChanged;
	public event EventHandler OnCut;

	public override void Interact(Player player)
	{
		if (!HasKitchenObject()) {
			if (player.HasKitchenObject() /*&& HasAnyRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())*/) {
				player.GetKitchenObject().SetParent(this);
				cuttingProgress = 0;
				OnCuttingProgressChanged?.Invoke(this, new OnCuttingProgressChagedEventArgs { CuttingProgressNormalized = 0 });
			}
		} else {
			if (!player.HasKitchenObject()) {
				GetKitchenObject().SetParent(player);
			}
		}
	}

	public override void AlternateInteract(Player player)
	{
		if (HasKitchenObject()) {
			var ko = GetKitchenObject();
			var recipe = GetRecipeForInput(ko.GetKitchenObjectSO());
			if (recipe != null) {
				cuttingProgress++;
				OnCuttingProgressChanged?.Invoke(
					this,
					new OnCuttingProgressChagedEventArgs {
						CuttingProgressNormalized = (float)cuttingProgress / recipe.CutsCount
					});
				OnCut?.Invoke(this, EventArgs.Empty);
				if (cuttingProgress >= recipe.CutsCount) {
					GetKitchenObject().Destroy();
					KitchenObject.Spawn(recipe.Output, this);
				}
			}
		}
	}

	private bool HasAnyRecipeWithInput(KitchenObjectSO input) => GetRecipeForInput(input) != null;

	private KitchenObjectSO GetOutputForInput(KitchenObjectSO input)
	{
		var recipe = GetRecipeForInput(input);
		if (recipe != null) {
			return recipe.Output; 
		}
		return null;
	}

	private CuttingRecipeSO GetRecipeForInput(KitchenObjectSO input)
	{
		for (int i = 0; i < Recipes.Length; i++) {
			var recipe = Recipes[i];
			if (recipe.Input == input) {
				return recipe;
			}
		}
		return null;
	}
}
