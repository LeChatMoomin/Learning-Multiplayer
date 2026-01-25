using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliverySingleRecipeUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI recipeNameText;
	[SerializeField] private Transform iconContainer;
	[SerializeField] private Transform iconTemplate;

	public void SetRecipeSO(DeliveryRecipeSO recipeSO)
	{
		recipeNameText.text = recipeSO.RecipeName;

		foreach (Transform child in iconContainer) {
			Destroy(child);
		}

		foreach (var ingredient in recipeSO.Ingredients) {
			var icon = Instantiate(iconTemplate, iconContainer);
			icon.GetComponent<Image>().sprite = ingredient.Icon;
		}
	}
}
