using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
	public enum Binding
	{
		MoveUp,
		MoveLeft,
		MoveDown,
		MoveRight,

		Interact,
		AltInteract,
		Pause,

		Gamepad_Interact,
		Gamepad_AltInteract,
		Gamepad_Pause,
	}

	private const string BindingsJsonName = "InputBindings";

	public static GameInput Instance { get; private set; }

	private PlayerInputActions playerInputActions;
	public event EventHandler OnInteractAction; 
	public event EventHandler OnAlternateInteractAction; 
	public event EventHandler OnPauseAction; 
	public event EventHandler OnRebindAction; 

	private void Awake()
	{
		Instance = this;
		playerInputActions = new PlayerInputActions();

		if (PlayerPrefs.HasKey(BindingsJsonName)) {
			playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(BindingsJsonName));
		}

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
		playerInputActions.Player.Disable();
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

	public string GetBindingText(Binding binding)
	{
		switch (binding) {
			case Binding.MoveUp:
				return playerInputActions.Player.Move.bindings[1].ToDisplayString();
			case Binding.MoveLeft:
				return playerInputActions.Player.Move.bindings[2].ToDisplayString();
			case Binding.MoveDown:
				return playerInputActions.Player.Move.bindings[3].ToDisplayString();
			case Binding.MoveRight:
				return playerInputActions.Player.Move.bindings[4].ToDisplayString();
			case Binding.Interact:
			return	playerInputActions.Player.Interact.bindings[0].ToDisplayString();
			case Binding.AltInteract:
				return playerInputActions.Player.AlternateInteract.bindings[0].ToDisplayString();
			case Binding.Pause:
				return playerInputActions.Player.Pause.bindings[0].ToDisplayString();
			case Binding.Gamepad_Interact:
				return playerInputActions.Player.Interact.bindings[1].ToDisplayString();
			case Binding.Gamepad_AltInteract:
				return playerInputActions.Player.AlternateInteract.bindings[1].ToDisplayString();
			case Binding.Gamepad_Pause:
				return playerInputActions.Player.Pause.bindings[1].ToDisplayString();
		}
		return string.Empty;
	}

	public void Rebind(Binding binding, Action onRebound)
	{
		InputAction inputAction;
		var actionIndex = 0;


		switch (binding) {
			default:
			case Binding.MoveUp:
				inputAction = playerInputActions.Player.Move;
				actionIndex = 1;
				break;
			case Binding.MoveLeft:
				inputAction = playerInputActions.Player.Move;
				actionIndex = 2;
				break;
			case Binding.MoveDown:
				inputAction = playerInputActions.Player.Move;
				actionIndex = 3;
				break;
			case Binding.MoveRight:
				inputAction = playerInputActions.Player.Move;
				actionIndex = 4;
				break;
			case Binding.Interact:
				inputAction = playerInputActions.Player.Interact;
				actionIndex = 0;
				break;
			case Binding.AltInteract:
				inputAction = playerInputActions.Player.AlternateInteract;
				actionIndex = 0;
				break;
			case Binding.Pause:
				inputAction = playerInputActions.Player.Pause;
				actionIndex = 0;
				break;
			case Binding.Gamepad_Interact:
				inputAction = playerInputActions.Player.Interact;
				actionIndex = 1;
				break;
			case Binding.Gamepad_AltInteract:
				inputAction = playerInputActions.Player.AlternateInteract;
				actionIndex = 1;
				break;
			case Binding.Gamepad_Pause:
				inputAction = playerInputActions.Player.Pause;
				actionIndex = 1;
				break;
		}

		playerInputActions.Player.Disable();
		inputAction.PerformInteractiveRebinding(actionIndex).OnComplete(
			callback => {
				callback.Dispose();
				playerInputActions.Player.Enable();
				onRebound();
				PlayerPrefs.SetString(BindingsJsonName, playerInputActions.SaveBindingOverridesAsJson());
				PlayerPrefs.Save();
				OnRebindAction?.Invoke(this, EventArgs.Empty);
			}
		).Start();
	}
}
