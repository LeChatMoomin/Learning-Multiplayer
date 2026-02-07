using UnityEngine;

public class FlashingBarUI : MonoBehaviour
{
	private const string FlashAnimationTriggerName = "IsFlashing";
	private Animator animator;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	public void SetFlashingTrigger(bool value)
	{
		animator.SetBool(FlashAnimationTriggerName, value);
	}
}
