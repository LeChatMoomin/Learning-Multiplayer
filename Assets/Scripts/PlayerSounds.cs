using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
	private Player player;
	private float footStepTimer;
	private float footStepTime = .1f;


	private void Awake()
	{
		player = GetComponent<Player>();
	}

	private void Update()
	{
		if (player.IsWalking) {
			footStepTimer += Time.deltaTime;
			if (footStepTimer >= footStepTime) {
				footStepTimer = 0;
				SoundManager.Instance.PlayFootstepSound(transform.position);
			}
		}
	}
}
