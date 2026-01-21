using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameInput : MonoBehaviour
{
	private PlayerInputActions playerInputActions;
	public event EventHandler OnInteractAction; 
	public event EventHandler OnAlternateInteractAction; 

	private void Awake()
	{
		playerInputActions = new PlayerInputActions();
		playerInputActions.Player.Enable();
		playerInputActions.Player.Interact.performed += InteractPerformed;
		playerInputActions.Player.AlternateInteract.performed += AlternateInteractPerformed;
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
