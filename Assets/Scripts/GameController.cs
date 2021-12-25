using UnityEngine;
using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System;

public enum Player { NoMark, Naught, Cross, Draw }
public class GameController : MonoBehaviour
{
    // gaming golem
    public GameObject markerPrefab;
    public static event Action<Player> gameWin;
    public Player turn;
    public Player winner;
    public Player[,] markers;
    [SerializeField] public Vector3[,] markerSpots;
    public List<GameObject> markerObjects;
    public static bool playerHasWon = false;
    void Start()
	{
		Physics.queriesHitTriggers = true;
		markerObjects = new List<GameObject>();
		ResetGame();
		CanvasScript.resetGame += () =>
		{
            ResetGame();
		};
		void SetMarkerArray()
		{
			markers = new Player[3, 3];
			for (int x = 0; x < 3; x++)
			{
				for (int y = 0; y < 3; y++)
				{
					markers[x, y] = Player.NoMark;
				}
			}
		}

		void ResetGame()
		{
			turn = Player.Cross;
			SetMarkerArray();
			SetMarkerSpots();
			foreach (var item in markerObjects)
			{
				Destroy(item);
			}
			markerObjects.Clear();
			playerHasWon = false;
		}

	    void SetMarkerSpots()
        {
            markerSpots = new Vector3[3, 3];
            markerSpots[0, 0] = new Vector3(-2.8f, -2.8f, -1f);
            markerSpots[1, 0] = new Vector3(0f, -2.8f, -1f);
            markerSpots[2, 0] = new Vector3(2.8f, -2.8f, -1f);
            markerSpots[0, 1] = new Vector3(-2.8f, 0f, -1f);
            markerSpots[1, 1] = new Vector3(0f, 0f, -1f);
            markerSpots[2, 1] = new Vector3(2.8f, 0f, -1f);
            markerSpots[0, 2] = new Vector3(-2.8f, 2.8f, -1f);
            markerSpots[1, 2] = new Vector3(0f, 2.8f, -1f);
            markerSpots[2, 2] = new Vector3(2.8f, 2.8f, -1f);
        }
	}
    void OnMouseDown()
    {
        if (PauseMenu.isGamePaused)
            return;
        if (playerHasWon)
            return;
        Vector2 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        TryPlaceMarker(p);
        winner = FindWinner();
        if (winner != Player.NoMark)
        {
            playerHasWon = true;
            //Calling gameWin
            gameWin(winner);
            Debug.Log($"{winner} won the game!");
        }

        void TryPlaceMarker(Vector2 pos)
        {
            (int x, int y) spot = WinCalculator.GetNearestSpot(pos, markerSpots);
            if (markers[spot.x, spot.y] != Player.NoMark)
		    {
                Debug.Log("That spot is taken.");
                return;
		    }
            GameObject marker = Instantiate(markerPrefab, markerSpots[spot.x, spot.y], new Quaternion(0, 0, 0, 0));
            markerObjects.Add(marker);
            markers[spot.x, spot.y] = turn;
            marker.GetComponent<MarkerScript>().team = turn;
            if (turn == Player.Cross)
            {
                turn = Player.Naught;
            }
            else
            {
                turn = Player.Cross;
            }
        }

        Player FindWinner()
        {
            if (FindIfWon(Player.Cross)) return Player.Cross;
            if (FindIfWon(Player.Naught)) return Player.Naught;
            if (markerObjects.Count == 9) return Player.Draw;
            return Player.NoMark;
        }

        bool FindIfWon(Player player)
        {
            return (WinCalculator.FindRowWin(player, markers) || WinCalculator.FindColWin(player, markers) || WinCalculator.FindDiagWin(player, markers));
        }
    }

}