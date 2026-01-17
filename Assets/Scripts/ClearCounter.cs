using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{
	[SerializeField] private KitchenObjectSO kitchenObjectSO;
	[SerializeField] private Transform counterTopPoint;

	private KitchenObject kitchenObject;

	public Transform GetObjectHoldPoint() => counterTopPoint;
	public KitchenObject GetKitchenObject() => kitchenObject;
	public void ClearKitchenObject() => SetKitchenObject(null);
	public bool HasKitchenObject() => kitchenObject != null;

	public void Interact(Player player)
	{
		if (kitchenObject is null) {
			var kitchenObjectTransform = Instantiate(kitchenObjectSO.Prefab, counterTopPoint);
			kitchenObjectTransform.GetComponent<KitchenObject>().SetParent(this);
		} else {
			kitchenObject.SetParent(player);
		}
	}

	public void SetKitchenObject(KitchenObject kitchenObject)
	{
		this.kitchenObject = kitchenObject;
	}
}
