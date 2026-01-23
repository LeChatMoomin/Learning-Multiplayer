using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Windows;

public class CuttingCounter : BaseCounter, IProgressBarOwner
{

	[SerializeField] private CuttingRecipeSO[] Recipes;
	private int cuttingProgress;

	public event EventHandler<IProgressBarOwner.OnProgressChagedEventArgs> OnProgressChanged;
	public event EventHandler OnCut;

	public override void Interact(Player player)
	{
		if (!HasKitchenObject()) {
			if (player.HasKitchenObject() && HasAnyRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
				player.GetKitchenObject().SetParent(this);
				cuttingProgress = 0;
				OnProgressChanged?.Invoke(this, new IProgressBarOwner.OnProgressChagedEventArgs { ProgressNormalized = 0 });
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
				OnProgressChanged?.Invoke(
					this,
					new IProgressBarOwner.OnProgressChagedEventArgs {
						ProgressNormalized = (float)cuttingProgress / recipe.CutsCount
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
