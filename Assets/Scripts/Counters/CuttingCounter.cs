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

	public static event EventHandler OnAnyCut;

	new public static void ResetStaticData()
	{
		OnAnyCut = null;
	}

	public override void Interact(Player player)
	{
		if (HasKitchenObject()) {
			if (player.HasKitchenObject()) {
				if (player.GetKitchenObject().IsPlate(out var plate)) {
					if (plate.TryAddIngredient(GetKitchenObject().GetScriptableObject())) {
						GetKitchenObject().Destroy();
					}
				} else if (GetKitchenObject().IsPlate(out plate)) {
					if (plate.TryAddIngredient(player.GetKitchenObject().GetScriptableObject())) {
						player.GetKitchenObject().Destroy();
					}
				}
			} else {
				GetKitchenObject().SetParent(player);
			}
		} else {
			if (player.HasKitchenObject()) {
				player.GetKitchenObject().SetParent(this);
				cuttingProgress = 0;
				OnProgressChanged?.Invoke(this, new IProgressBarOwner.OnProgressChagedEventArgs { ProgressNormalized = 0 });
			}
		}
	}

	public override void AlternateInteract(Player player)
	{
		if (HasKitchenObject()) {
			var ko = GetKitchenObject();
			var recipe = GetRecipeForInput(ko.GetScriptableObject());
			if (recipe != null) {
				cuttingProgress++;
				OnProgressChanged?.Invoke(
					this,
					new IProgressBarOwner.OnProgressChagedEventArgs {
						ProgressNormalized = (float)cuttingProgress / recipe.CutsCount
					});
				OnCut?.Invoke(this, EventArgs.Empty);
				OnAnyCut?.Invoke(this, EventArgs.Empty);
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
