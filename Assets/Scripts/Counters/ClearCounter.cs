using UnityEngine;

public class ClearCounter : BaseCounter, IKitchenObjectParent
{
	public override void Interact(Player player)
	{
		if (!HasKitchenObject()) {
			if (player.HasKitchenObject()) {
				player.GetKitchenObject().SetParent(this);
			}
		} else {
			if (!player.HasKitchenObject()) {
				GetKitchenObject().SetParent(player);
			}
		}
	}
}
