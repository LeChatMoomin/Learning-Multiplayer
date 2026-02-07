using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI moveUpText;
	[SerializeField] private TextMeshProUGUI moveLeftText;
	[SerializeField] private TextMeshProUGUI moveDownText;
	[SerializeField] private TextMeshProUGUI moveRightText;

	[SerializeField] private TextMeshProUGUI interactText;
	[SerializeField] private TextMeshProUGUI altInteractText;
	[SerializeField] private TextMeshProUGUI pauseText;

	[SerializeField] private TextMeshProUGUI gamepadInteractText;
	[SerializeField] private TextMeshProUGUI gamepadAltInteractText;
	[SerializeField] private TextMeshProUGUI gamepadPauseText;

	private void Start()
	{
		UpdateVisuals();
		GameInput.Instance.OnRebindAction += OnRebindAction;
		GameManager.Instance.OnStateChanged += OnGameStateChanged;
		Show();
	}

	private void OnGameStateChanged(object sender, System.EventArgs e)
	{
		Hide();
	}

	private void OnRebindAction(object sender, System.EventArgs e)
	{
		UpdateVisuals();
	}

	private void UpdateVisuals()
	{
		moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveUp);
		moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveLeft);
		moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveDown);
		moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveRight);

		interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
		altInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.AltInteract);
		pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);

		gamepadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
		gamepadAltInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_AltInteract);
		gamepadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause);
	}

	private void Show()
	{
		gameObject.SetActive(true);
	}

	private void Hide()
	{
		gameObject.SetActive(false);
	}
}
