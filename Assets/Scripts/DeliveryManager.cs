using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
	//Singleton
	public static DeliveryManager Instance {  get; private set; }
	public int DeliveryScore { get; private set; }

	public event EventHandler OnRecipeSpawned;
	public event EventHandler OnRecipeCompleted;

	public event EventHandler OnRecipeSuccess;
	public event EventHandler OnRecipeFailed;

	[SerializeField] private RecipeListSO RecipeList;

	private List<DeliveryRecipeSO> WaitingList = new List<DeliveryRecipeSO>();

	private float spawnRecipeTimer;
	private float spawnRecipeTimeMax = 5f;
	private int maxWaitingRecipesCount = 4;

	private void Awake()
	{
		Instance = this;
	}

	private void Update()
	{
		if (WaitingList.Count < maxWaitingRecipesCount) {
			spawnRecipeTimer += Time.deltaTime;
			if (spawnRecipeTimer >= spawnRecipeTimeMax && GameManager.Instance.IsGamePlaying()) {
				spawnRecipeTimer = 0;
				var waitingRecipe = RecipeList.Recipes[UnityEngine.Random.Range(0, RecipeList.Recipes.Count)];
				WaitingList.Add(waitingRecipe);
				OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
				Debug.Log($"Need {waitingRecipe.RecipeName}");
			}
		}
	}

	public void Deliver(PlateKitchenObject plate)
	{
		//провер€ем что на тарелке то, что нам нужно
		var plateIngredients = plate.GetIngredientsList();
		for (int i = 0; i < WaitingList.Count; i++) {
			var waitingRecipe = WaitingList[i];
			if (waitingRecipe.Ingredients.Count == plateIngredients.Count) {
				var plateContentMatchesRecipe = true;
				foreach (var ingredient in waitingRecipe.Ingredients) {
					if (!plateIngredients.Contains(ingredient)) {
						plateContentMatchesRecipe = false;
					}
				}
				foreach (var ingredient in plateIngredients) {
					if (!waitingRecipe.Ingredients.Contains(ingredient)) {
						plateContentMatchesRecipe = false;
					}
				}
				if (plateContentMatchesRecipe) {
					//≈сть такой рецепт в листе ожидани€
					WaitingList.Remove(waitingRecipe);
					OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
					OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
					DeliveryScore++;
					return;
				}
			}
		}
		OnRecipeFailed?.Invoke(this, EventArgs.Empty);
	}

	public List<DeliveryRecipeSO> GetWaitingList() => WaitingList;
}
