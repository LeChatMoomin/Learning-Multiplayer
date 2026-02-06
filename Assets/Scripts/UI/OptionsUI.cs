using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
	public static OptionsUI Instance { get; private set; }

	[SerializeField] private Button SoundEffectsButton;
	[SerializeField] private Button MusicButton;
	[SerializeField] private Button ReturnButton;
	[SerializeField] private TextMeshProUGUI soundEffectsText;
	[SerializeField] private TextMeshProUGUI musicText;

	[SerializeField] private TextMeshProUGUI moveUpText;
	[SerializeField] private TextMeshProUGUI moveLeftText;
	[SerializeField] private TextMeshProUGUI moveDownText;
	[SerializeField] private TextMeshProUGUI moveRightText;
	[SerializeField] private TextMeshProUGUI interactText;
	[SerializeField] private TextMeshProUGUI altInteractText;
	[SerializeField] private TextMeshProUGUI pauseText;

	[SerializeField] private Button moveUpButton;
	[SerializeField] private Button moveLeftButton;
	[SerializeField] private Button moveDownButton;
	[SerializeField] private Button moveRightButton;
	[SerializeField] private Button interactButton;
	[SerializeField] private Button altInteractButton;
	[SerializeField] private Button pauseButton;

	[SerializeField] private GameObject RebindWindow;

	private void Awake()
	{
		Instance = this;
		SoundEffectsButton.onClick.AddListener(() => {
			SoundManager.Instance.ChangeVolume();
			UpdateVisual();
		});
		MusicButton.onClick.AddListener(() => {
			MusicManager.Instance.ChangeVolume();
			UpdateVisual();
		});
		ReturnButton.onClick.AddListener(() => {
			Hide();
			GamePauseUI.Instance.Show();
		});

		moveUpButton.onClick.AddListener(() => {
			StartRebinding(GameInput.Binding.MoveUp);
		});
		moveLeftButton.onClick.AddListener(() => {
			StartRebinding(GameInput.Binding.MoveLeft);
		});
		moveDownButton.onClick.AddListener(() => {
			StartRebinding(GameInput.Binding.MoveDown);
		});
		moveRightButton.onClick.AddListener(() => {
			StartRebinding(GameInput.Binding.MoveRight);
		});
		interactButton.onClick.AddListener(() => {
			StartRebinding(GameInput.Binding.Interact);
		});
		altInteractButton.onClick.AddListener(() => {
			StartRebinding(GameInput.Binding.AltInteract);
		});
		pauseButton.onClick.AddListener(() => {
			StartRebinding(GameInput.Binding.Pause);
		});
	}

	private void OnGameUnpaused(object sender, System.EventArgs e)
	{
		Hide();
	}

	private void Start()
	{
		GameManager.Instance.OnGameUnpaused += OnGameUnpaused;
		UpdateVisual();
		Hide();
	}

	private void UpdateVisual()
	{
		soundEffectsText.text = $"Sound Effects: {Mathf.Round(SoundManager.Instance.Volume * 10)}";
		musicText.text = $"Music: {Mathf.Round(MusicManager.Instance.Volume * 10)}";

		moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveUp);
		moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveLeft);
		moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveDown);
		moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveRight);

		interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
		altInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.AltInteract);
		pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
	}

	public void Show()
	{
		gameObject.SetActive(true);
	}

	private void Hide()
	{
		gameObject.SetActive(false);
	}

	private void StartRebinding(GameInput.Binding binding)
	{
		ShowRebindWindow();
		GameInput.Instance.Rebind(binding, () => { HideRebindWindow(); UpdateVisual(); });
	}

	private void ShowRebindWindow()
	{
		RebindWindow.SetActive(true);
	}

	private void HideRebindWindow()
	{
		RebindWindow.SetActive(false);
	}
}
