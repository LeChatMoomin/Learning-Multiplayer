using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
	private const string CutAnimation = "Cut";

	[SerializeField] private CuttingCounter cuttingCounter;
	private Animator animator;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	private void Start()
	{
		cuttingCounter.OnCut += OnCut;
	}

	private void OnCut(object sender, System.EventArgs e)
	{
		animator.SetTrigger(CutAnimation);
	}
}
