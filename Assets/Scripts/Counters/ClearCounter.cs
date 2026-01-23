using UnityEngine;

public class ClearCounter : BaseCounter, IKitchenObjectParent
{
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
			}
		}
	}
}
