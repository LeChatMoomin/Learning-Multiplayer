using System;
using UnityEngine;

public class SelectedCounterChangedEventArgs
{
	public BaseCounter SelectedCounter;
}

public class Player : MonoBehaviour, IKitchenObjectParent
{
	public static Player Instance { get; private set;}

	public event EventHandler<SelectedCounterChangedEventArgs> OnSelectedCounterChanged;

	[SerializeField] private float MoveSpeed = 7f;
	[SerializeField] private float RotationSpeed = 20f;
	[SerializeField] private GameInput gameInput;
	[SerializeField] private LayerMask countersMask;
	[SerializeField] private Transform kitchenObjectHoldPoint;

	private float playerRadius = .7f;
	private float playerHeight = 2f;
	private float interactDistance = 2f;
	private bool isWalking;
	private Vector3 position => transform.position;
	private Vector3 interactDirection;
	private BaseCounter selectedCounter;
	private KitchenObject kitchenObject;
	public bool IsWalking => isWalking;

	private void Awake()
	{
		if (Instance != null) {
			Debug.LogError("More than one player!");
		}
		Instance = this;
	}

	private void Start()
	{
		gameInput.OnInteractAction += GameInputOnInteractAction;
		gameInput.OnAlternateInteractAction += GameInputOnAlternateInteractAction;
	}

	private void GameInputOnAlternateInteractAction(object sender, EventArgs e)
	{
		selectedCounter?.AlternateInteract(this);
	}

	private void GameInputOnInteractAction(object sender, EventArgs e)
	{
		selectedCounter?.Interact(this);
	}

	private void Update()
	{
		HandleMovement();
		HandleSelection();
	}

	private void HandleSelection()
	{
		var inputVector = gameInput.GetMovementVectorNormalized();
		var moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);
		if (moveDirection != Vector3.zero) {
			interactDirection = moveDirection;
		}
		if (Physics.Raycast(position, interactDirection, out var hit, interactDistance, countersMask)) {
			if (hit.transform.TryGetComponent<BaseCounter>(out var counter)) {
				if (selectedCounter != counter) {
					SetSelectedCounter(counter);
				}
			} else {
				SetSelectedCounter(null);
			}
		} else {
			SetSelectedCounter(null);
		}
	}

	private void HandleMovement()
	{
		var inputVector = gameInput.GetMovementVectorNormalized();
		if (inputVector.Equals(Vector2.zero)) {
			isWalking = false;
			return;
		}

		var moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);
		transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * RotationSpeed);

		var moveDistance = MoveSpeed * Time.deltaTime;
		var playerHeadPosition = position + Vector3.up * playerHeight;
		var canMove = !Physics.CapsuleCast(position, playerHeadPosition, playerRadius, moveDirection, moveDistance);
		if (!canMove) {
			//Проверяем можем ли идти по диагонали
			var moveX = new Vector3(moveDirection.x, 0, 0);
			canMove = moveDirection.x != 0 && !Physics.CapsuleCast(position, playerHeadPosition, playerRadius, moveX, moveDistance);
			if (canMove) {
				moveDirection = moveX.normalized;
			} else {
				var moveZ = new Vector3(0,0, moveDirection.z);
				canMove = moveDirection.z != 0 && !Physics.CapsuleCast(position, playerHeadPosition, playerRadius, moveZ, moveDistance);
				if (canMove) {
					moveDirection = moveZ.normalized;
				}
			}
		}
		if (canMove) {
			transform.position += moveDirection * moveDistance;
		}
		isWalking = moveDirection != Vector3.zero;
	}

	private void SetSelectedCounter(BaseCounter counter)
	{
		selectedCounter = counter;
		OnSelectedCounterChanged?.Invoke(
			this,
			new SelectedCounterChangedEventArgs {
				SelectedCounter = counter
			}
		);
	}

	public Transform GetObjectHoldPoint() => kitchenObjectHoldPoint;
	public KitchenObject GetKitchenObject() => kitchenObject;
	public void ClearKitchenObject() => kitchenObject = null;
	public bool HasKitchenObject() => kitchenObject != null;
	public void SetKitchenObject(KitchenObject kitchenObject) => this.kitchenObject = kitchenObject;
}