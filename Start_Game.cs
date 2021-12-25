using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Start_Game : MonoBehaviour
{
    public Button button;

	private void Start()
	{
		button.onClick.AddListener(StartGame);
	}
	void StartGame()
	{
		SceneManager.LoadScene("Game");
	}
}
