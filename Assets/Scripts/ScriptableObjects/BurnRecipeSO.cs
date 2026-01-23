using UnityEngine;

[CreateAssetMenu()]
public class BurnRecipeSO: ScriptableObject
{
	public KitchenObjectSO Input;
	public KitchenObjectSO Output;
	public float BurningTime;
}