using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI countdownText;

	private Animator animator;
	private const string PopupAnimationName = "Popup";
	private int prevNumber;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	private void Start()
	{
		GameManager.Instance.OnStateChanged += OnStateChanged;
		Hide();
	}

	private void Update()
	{
		var currentNumber = (int)GameManager.Instance.GetCountdownToStartTimer() + 1;
		countdownText.text = $"{currentNumber}";
		if (!currentNumber.Equals(prevNumber)) {
			prevNumber = currentNumber;
			animator.SetTrigger(PopupAnimationName);
			SoundManager.Instance.PlayCountdownSound();
		}
	}

	private void OnStateChanged(object sender, System.EventArgs e)
	{
		if (GameManager.Instance.IsCountdownToStartActive()) {
			Show();
		} else { 
			Hide();
		}
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
