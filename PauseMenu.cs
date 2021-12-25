using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	public static bool isGamePaused = false;
	public GameObject pauseMenu;
	public Button resumeButton;
	public Button menuButton;
	public Button quitButton;

	private void Start()
	{
		resumeButton.onClick.AddListener(() => 
		{ isGamePaused = false; pauseMenu.SetActive(false); Time.timeScale = 1f; });

		menuButton.onClick.AddListener(() =>
		{ 
			isGamePaused = false;
			SceneManager.LoadScene("Menu");
		});

		quitButton.onClick.AddListener(() =>
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
		});
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (!GameController.playerHasWon)
			{
				isGamePaused = !isGamePaused;
				pauseMenu.SetActive(isGamePaused);
				Time.timeScale = isGamePaused ? 0f : 1f;
			}
		}
	}

}