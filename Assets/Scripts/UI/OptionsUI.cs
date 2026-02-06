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
	}

	public void Show()
	{
		gameObject.SetActive(true);
	}

	private void Hide()
	{
		gameObject.SetActive(false);
	}
}
