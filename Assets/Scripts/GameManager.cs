using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance {  get; private set; }

	public event EventHandler OnStateChanged;
	public event EventHandler OnGamePaused;
	public event EventHandler OnGameUnpaused;

	private enum GameState
	{
		WaitingToStart,
		CountdownToStart,
		GamePlaying,
		GameOver,
	}

	private GameState state = GameState.WaitingToStart;

	private float timer;

	private float startTime = 1f;
	private float countdownToStartTime = 3f;
	private float gamePlaingTime = 10f;

	private bool isGamePaused = false;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		GameInput.Instance.OnPauseAction += OnPauseInput;
	}

	private void Update()
	{
		switch (state) {
			case GameState.WaitingToStart:
				timer += Time.deltaTime;
				if(timer >= startTime) {
					timer = 0f;
					state = GameState.CountdownToStart;
					OnStateChanged?.Invoke(this, EventArgs.Empty);
				}
				break;
			case GameState.CountdownToStart:
				timer += Time.deltaTime;
				if (timer >= countdownToStartTime) {
					timer = 0f;
					state = GameState.GamePlaying;
					OnStateChanged?.Invoke(this, EventArgs.Empty);
				}
				break;
			case GameState.GamePlaying:
				timer += Time.deltaTime;
				if (timer >= gamePlaingTime) {
					timer = 0f;
					state = GameState.GameOver;
					OnStateChanged?.Invoke(this, EventArgs.Empty);
				}
				break;
			case GameState.GameOver:
				break;
		}
	}

	private void OnPauseInput(object sender, EventArgs e)
	{
		ToggleGamePause();
	}

	public void ToggleGamePause()
	{
		isGamePaused = !isGamePaused;
		Time.timeScale = isGamePaused ? 0 : 1;
		if (isGamePaused) {
			OnGamePaused?.Invoke(this, EventArgs.Empty);
		} else {
			OnGameUnpaused?.Invoke(this, EventArgs.Empty);
		}
	}

	public bool IsGamePlaying() => state == GameState.GamePlaying;

	public bool IsGamePaused() => isGamePaused;

	public bool IsCountdownToStartActive() => state == GameState.CountdownToStart;

	public float GetCountdownToStartTimer()
	{
		if (state == GameState.CountdownToStart) {
			return countdownToStartTime - timer;
		}
		return 0f;
	}

	public float GetGamePlayTimerNormalized()
	{
		if (state == GameState.GamePlaying) {
			return 1 - (timer / gamePlaingTime);
		}
		return 0f;
	}

	public bool IsGameOver() => state == GameState.GameOver;
}
