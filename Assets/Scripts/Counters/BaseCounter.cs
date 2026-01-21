using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
	[SerializeField] private Transform counterTopPoint;

	private KitchenObject kitchenObject;

	public Transform GetObjectHoldPoint() => counterTopPoint;
	public KitchenObject GetKitchenObject() => kitchenObject;
	public void ClearKitchenObject() => SetKitchenObject(null);
	public bool HasKitchenObject() => kitchenObject != null;

	public virtual void Interact(Player player) { }
	public virtual void AlternateInteract(Player player) { }

	public void SetKitchenObject(KitchenObject kitchenObject)
	{
		this.kitchenObject = kitchenObject;
	}
}
