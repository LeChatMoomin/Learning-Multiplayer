using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameInput : MonoBehaviour
{
	private PlayerInputActions playerInputActions;
	public event EventHandler OnInteractAction; 

	private void Awake()
	{
		playerInputActions = new PlayerInputActions();
		playerInputActions.Player.Enable();
		playerInputActions.Player.Interact.performed += InteractPerformed;
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
