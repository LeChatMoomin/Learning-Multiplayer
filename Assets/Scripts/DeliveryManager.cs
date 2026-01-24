using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
	//Singleton
	public static DeliveryManager Instance {  get; private set; }

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
			if (spawnRecipeTimer >= spawnRecipeTimeMax) {
				spawnRecipeTimer = 0;
				var waitingRecipe = RecipeList.Recipes[Random.Range(0, RecipeList.Recipes.Count)];
				WaitingList.Add(waitingRecipe);
				Debug.Log($"Need {waitingRecipe.RecipeName}");
			}
		}
	}

	public void Deliver(PlateKitchenObject plate)
	{
		//проверяем что на тарелке то, что нам нужно
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
					Debug.Log("Correct!");
					WaitingList.Remove(waitingRecipe);
					return;
				}
			}
		}
		//No matches
		Debug.Log("Incorrect");
	}
}
