using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DeliveryRecipeSO : ScriptableObject
{
	public List<KitchenObjectSO> Ingredients;
	public string RecipeName;
}
