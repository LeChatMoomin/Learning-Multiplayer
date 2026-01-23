using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.CullingGroup;

public class OnStoveStateChangedEventArgs : EventArgs
{
	public StoveCounter.State State;
}

public class StoveCounter : BaseCounter, IProgressBarOwner
{
	public enum State
	{
		Idle,
		Frying,
		Fried,
		Burned,
	}

	public event EventHandler<OnStoveStateChangedEventArgs> OnStateChanged;
	public event EventHandler<IProgressBarOwner.OnProgressChagedEventArgs> OnProgressChanged;

	[SerializeField] private FryingRecipeSO[] FryingRecipes;
	[SerializeField] private BurnRecipeSO[] BurningRecipes;

	private float fryingProgress;
	private float burnTimer;
	private FryingRecipeSO fryingRecipe;
	private BurnRecipeSO burningRecipe;
	private State state;

	private void Start()
	{
		state = State.Idle;
	}

	private void Update()
	{
		switch (state) {
			case State.Idle:
				break;
			case State.Frying:
				fryingProgress += Time.deltaTime;
				OnProgressChanged?.Invoke(
					this,
					new IProgressBarOwner.OnProgressChagedEventArgs {
						ProgressNormalized = fryingProgress/ fryingRecipe.FryingTime
					}
				);
				if (fryingProgress > fryingRecipe.FryingTime) {
					GetKitchenObject().Destroy();
					KitchenObject.Spawn(fryingRecipe.Output, this);
					state = State.Fried;
					OnStateChanged?.Invoke(this, new OnStoveStateChangedEventArgs { State = State.Fried });
					burnTimer = 0;
					burningRecipe = GetBurnedOutputForInput(GetKitchenObject().GetKitchenObjectSO());
				}
				break;
			case State.Fried:
				burnTimer += Time.deltaTime;
				OnProgressChanged?.Invoke(
					this,
					new IProgressBarOwner.OnProgressChagedEventArgs {
						ProgressNormalized = burnTimer / burningRecipe.BurningTime
					}
				);
				if (burnTimer > burningRecipe.BurningTime) {
					GetKitchenObject().Destroy();
					KitchenObject.Spawn(burningRecipe.Output, this);
					state = State.Burned;
					OnStateChanged?.Invoke(this, new OnStoveStateChangedEventArgs { State = State.Burned });
					OnProgressChanged?.Invoke(
						this,
						new IProgressBarOwner.OnProgressChagedEventArgs {
							ProgressNormalized = 0
						}
					);
				}
				break;
			case State.Burned:
				break;
		}
	}

	public override void Interact(Player player)
	{
		if (player.HasKitchenObject() && HasAnyRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
			if (!HasKitchenObject()) {
				player.GetKitchenObject().SetParent(this);
				fryingRecipe = GetRecipeForInput(GetKitchenObject().GetKitchenObjectSO());
				fryingProgress = 0;
				state = State.Frying;
				OnStateChanged?.Invoke(this, new OnStoveStateChangedEventArgs { State = State.Frying });
			}
		} else {
			if (HasKitchenObject()) {
				GetKitchenObject().SetParent(player);
				state = State.Idle;
				OnStateChanged?.Invoke(this, new OnStoveStateChangedEventArgs { State = State.Idle });
				OnProgressChanged?.Invoke(
					this,
					new IProgressBarOwner.OnProgressChagedEventArgs {
						ProgressNormalized = 0
					}
				);
			}
		}
	}

	public override void AlternateInteract(Player player)
	{

	}

	private bool HasAnyRecipeWithInput(KitchenObjectSO input) => GetRecipeForInput(input) != null;

	private FryingRecipeSO GetRecipeForInput(KitchenObjectSO input)
	{
		for (int i = 0; i < FryingRecipes.Length; i++) {
			var recipe = FryingRecipes[i];
			if (recipe.Input == input) {
				return recipe;
			}
		}
		return null;
	}
	private BurnRecipeSO GetBurnedOutputForInput(KitchenObjectSO input)
	{
		for (int i = 0; i < BurningRecipes.Length; i++) {
			var recipe = BurningRecipes[i];
			if (recipe.Input == input) {
				return recipe;
			}
		}
		return null;
	}
}
