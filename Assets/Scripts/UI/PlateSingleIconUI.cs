using UnityEngine;
using UnityEngine.UI;

public class PlateSingleIconUI : MonoBehaviour
{
	[SerializeField] private Image Image;

	public void SetKitchenObjectSO(KitchenObjectSO kso)
	{
		Image.sprite = kso.Icon;
	}
}
