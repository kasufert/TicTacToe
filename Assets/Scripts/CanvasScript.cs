using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class CanvasScript : MonoBehaviour
{
    [SerializeField] GameObject resetButtonObject;
    Button resetButton;
    public static event Action resetGame;
	private void Start()
	{
        GameController.gameWin += ActivateResetButton;

    }
    async void ActivateResetButton(Player winner)
	{
        resetButtonObject.SetActive(true);
        resetButton = resetButtonObject.GetComponent<Button>();
        Debug.Log("resetButton being set...");
        var textMeshPro = resetButtonObject.GetComponentInChildren<TextMeshProUGUI>();
        textMeshPro.text =
          winner switch
          {
              Player.Cross => "Cross wins!",
              Player.Naught => "Naught wins!",
              Player.Draw => "Draw!",
              _ => throw new ArgumentException("Draw cannot win!")
          };
        Debug.Log("Activating reset button");
        textMeshPro.text += "\nReset Game?";
        Debug.Log("Setting listener to onClick...");
        await Task.Delay(100);
        resetButton.onClick.AddListener(() =>
        {
            resetGame();
            resetButtonObject.SetActive(false);
            resetButton.onClick.RemoveAllListeners();
        }
        );
    }
}
