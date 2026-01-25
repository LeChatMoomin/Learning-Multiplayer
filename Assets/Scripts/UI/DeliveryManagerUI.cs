using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
	[SerializeField] private Transform container;
	[SerializeField] private Transform recipeTemplate;

	private void Start()
	{
		DeliveryManager.Instance.OnRecipeSpawned += OnRecipeSpawned;
		DeliveryManager.Instance.OnRecipeCompleted += OnRecipeCompleted;
	}

	private void OnRecipeCompleted(object sender, System.EventArgs e)
	{
		UpdateVisuals();
	}

	private void OnRecipeSpawned(object sender, System.EventArgs e)
	{
		UpdateVisuals();
	}

	private void UpdateVisuals()
	{
		foreach (Transform child in container) {
			Destroy(child.gameObject);
		}
		var waitingList = DeliveryManager.Instance.GetWaitingList();

		foreach (var recipe in waitingList) {
			var recipeTransform = Instantiate(recipeTemplate, container);
			recipeTransform.GetComponent<DeliverySingleRecipeUI>().SetRecipeSO(recipe);
		}
	}
}
