using System;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
	[SerializeField] private KitchenObjectSO KitchenObjectSO;

	private IKitchenObjectParent koParent;

	public KitchenObjectSO GetScriptableObject() => KitchenObjectSO;
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

	public static KitchenObject Spawn(KitchenObjectSO objectSO, IKitchenObjectParent parent)
	{
		var result = Instantiate(objectSO.Prefab).GetComponent<KitchenObject>();
		result.SetParent(parent);
		return result;
	}

	public bool IsPlate(out PlateKitchenObject plate)
	{
		plate = null;
		if (this is PlateKitchenObject) {
			plate = this as PlateKitchenObject;
			return true;
		}
		return false;
	}

	public void Destroy()
	{
		koParent.ClearKitchenObject();
		Destroy(gameObject);
	}
}
