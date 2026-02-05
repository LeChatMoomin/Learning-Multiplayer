using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameInput : MonoBehaviour
{
	public static GameInput Instance { get; private set; }

	private PlayerInputActions playerInputActions;
	public event EventHandler OnInteractAction; 
	public event EventHandler OnAlternateInteractAction; 
	public event EventHandler OnPauseAction; 

	private void Awake()
	{
		Instance = this;
		playerInputActions = new PlayerInputActions();
		playerInputActions.Player.Enable();
		playerInputActions.Player.Interact.performed += InteractPerformed;
		playerInputActions.Player.AlternateInteract.performed += AlternateInteractPerformed;
		playerInputActions.Player.Pause.performed += PausePerformed;
	}

	private void OnDestroy()
	{
		playerInputActions.Player.Interact.performed -= InteractPerformed;
		playerInputActions.Player.AlternateInteract.performed -= AlternateInteractPerformed;
		playerInputActions.Player.Pause.performed -= PausePerformed;
		playerInputActions.Dispose();
		Instance = null;
	}

	private void PausePerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
		OnPauseAction?.Invoke(this, EventArgs.Empty);
	}

	private void AlternateInteractPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
		OnAlternateInteractAction?.Invoke(this, EventArgs.Empty);
	}

	private void InteractPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
		OnInteractAction?.Invoke(this, EventArgs.Empty);
	}

	public Vector2 GetMovementVectorNormalized()
	{
		return playerInputActions.Player.Move.ReadValue<Vector2>().normalized;
	}
}
