using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCounter : MonoBehaviour
{
    private int naughtWins = 0;
    private int crossWins = 0;
    private int draws = 0;
	[SerializeField] Text naughtWinsText;
	[SerializeField] Text crossWinsText;
	[SerializeField] Text drawsText;
	private void Start()
	{
		naughtWinsText.text = $"Naught Wins: {naughtWins}";
		crossWinsText.text = $"Cross Wins: {crossWins}";
		drawsText.text = $"Draws: {draws}";
		GameController.gameWin += Refresh;
		void Refresh(Player winner)
		{
			switch (winner)
			{
				case Player.Naught:
					naughtWinsText.text = $"Naught Wins: {++naughtWins}";
					break;
				case Player.Cross:
					crossWinsText.text = $"Cross Wins: {++crossWins}";
					break;
				case Player.Draw:
					drawsText.text = $"Draws: {++draws}";
					break;
				default:
					throw new ArgumentException("NoMark can't win!");
			}
		}
	}
}
