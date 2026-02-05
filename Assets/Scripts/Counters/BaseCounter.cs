using System;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
	[SerializeField] private Transform counterTopPoint;

	public static event EventHandler OnSomethingPlaced;

	private KitchenObject kitchenObject;

	public static void ResetStaticData()
	{
		OnSomethingPlaced = null;
	}

	public Transform GetObjectHoldPoint() => counterTopPoint;
	public KitchenObject GetKitchenObject() => kitchenObject;
	public void ClearKitchenObject() => SetKitchenObject(null);
	public bool HasKitchenObject() => kitchenObject != null;

	public virtual void Interact(Player player) { }
	public virtual void AlternateInteract(Player player) { }

	public void SetKitchenObject(KitchenObject kitchenObject)
	{
		this.kitchenObject = kitchenObject;
		if (kitchenObject != null) {
			OnSomethingPlaced?.Invoke(this, EventArgs.Empty);
		}
	}
}
