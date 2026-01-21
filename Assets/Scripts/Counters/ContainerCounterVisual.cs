using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
	private const string OpenClose = "OpenClose";

	[SerializeField] private ContainerCounter containerCounter;
	private Animator animator;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	private void Start()
	{
		containerCounter.OnPlayerGrabbedObject += OnPlayerGrabbedObject;
	}

	private void OnPlayerGrabbedObject(object sender, System.EventArgs e)
	{
		animator.SetTrigger(OpenClose);
	}
}
