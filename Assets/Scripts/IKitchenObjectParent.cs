using UnityEngine;

public interface IKitchenObjectParent
{
	public Transform GetObjectHoldPoint();
	public KitchenObject GetKitchenObject();
	public void ClearKitchenObject();
	public bool HasKitchenObject();
	public void SetKitchenObject(KitchenObject kitchenObject);
}
