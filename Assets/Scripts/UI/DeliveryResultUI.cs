using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{
	[SerializeField] private Image bgImage;
	[SerializeField] private Image iconImage;
	[SerializeField] private TextMeshProUGUI deliveryResultText;

	[SerializeField] private Color successColor;
	[SerializeField] private Color failColor;
	[SerializeField] private Sprite successSprite;
	[SerializeField] private Sprite failSprite;

	private const string PopupAnimationTrigger = "Popup";
	private Animator animator;
	private string successText = "DELIVERY\r\nSUCCESS";
	private string faliText = "DELIVERY\r\nFAILED";

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	private void Start()
	{
		DeliveryManager.Instance.OnRecipeSuccess += OnRecipeSuccess;
		DeliveryManager.Instance.OnRecipeFailed += OnRecipeFailed;
	}

	private void OnRecipeFailed(object sender, System.EventArgs e)
	{
		bgImage.color = failColor;
		iconImage.sprite = failSprite;
		deliveryResultText.text = faliText;
		animator.SetTrigger(PopupAnimationTrigger);
	}

	private void OnRecipeSuccess(object sender, System.EventArgs e)
	{
		bgImage.color = successColor;
		iconImage.sprite = successSprite;
		deliveryResultText.text = successText;
		animator.SetTrigger(PopupAnimationTrigger);
	}
}
