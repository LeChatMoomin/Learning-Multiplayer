using UnityEngine;

public class KitchenObject : MonoBehaviour
{
	[SerializeField] private KitchenObjectSO KitchenObjectSO;

	private IKitchenObjectParent koParent;

	public KitchenObjectSO GetKitchenObjectSO() => KitchenObjectSO;
	public IKitchenObjectParent GetParent() => koParent;

	public void SetParent(IKitchenObjectParent parent)
	{
		koParent?.ClearKitchenObject();
		if (parent.HasKitchenObject()) {
			Debug.LogError("Parent already has a kitchenObject");
		}
		parent.SetKitchenObject(this);
		transform.parent = parent.GetObjectHoldPoint();
		koParent = parent;
		transform.localPosition = Vector3.zero;
	}

}
