using UnityEngine;
using UnityEngine.UI;

public class PlateIconsUI : MonoBehaviour
{
	[SerializeField] private PlateKitchenObject PlateObject;
	[SerializeField] private Transform IconTemplate;

	private void Start()
	{
		PlateObject.OnIngredientAdded += OnIngredientAdded;
	}

	private void OnIngredientAdded(object sender, PlateKitchenObject.OnIngrediendAddEventArgs e)
	{
		UpdateVisuals();
	}

	private void UpdateVisuals()
	{
		foreach (Transform child in transform) {
			Destroy(child.gameObject);
		}
		foreach (var kso in PlateObject.GetIngredientsList()) {
			var icon = Instantiate(IconTemplate, transform);
			icon.GetComponent<PlateSingleIconUI>().SetKitchenObjectSO(kso);
		}
	}
}
