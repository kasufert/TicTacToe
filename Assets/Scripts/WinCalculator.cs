using UnityEngine;

namespace Assets.Scripts
{
    static class WinCalculator
    {
        public static Player OtherMark(this Player p)
		{
            if (p == Player.NoMark)
                throw new System.ArgumentException("Tried to find the other player to a NoMark.");
			if (p == Player.Naught)
                return Player.Cross;
            return Player.Naught;
		}
        public static (int x, int y) GetNearestSpot(Vector2 position, Vector3[,] markerSpots)
        {
            float lowestDist = 100f;
            float currentDist = 0f;
            (int, int) closestSpot = (2, 0);
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    currentDist = Vector2.Distance(position, markerSpots[x, y]);
                    if (currentDist < lowestDist)
                    {
                        lowestDist = currentDist;
                        closestSpot = (x, y);
                    }
                }
            }
            return closestSpot;
        }
        public static bool FindDiagWin(Player player, Player[,] markers)
        {
            bool[] diagWin = { true, true };
            for (int x = 0; x < 3; x++)
            {
                if (markers[x, x] != player) diagWin[0] = false;
            }
            int y = 0;
            for (int x = 2; x >= 0; x--)
            {
                if (markers[x, y] != player) diagWin[1] = false;
                y++;
            }
            for (int i = 0; i < 2; i++)
            {
                if (diagWin[i]) return true;
            }

            return false;
        }
        public static bool FindColWin(Player player, Player[,] markers)
        {
            bool[] colWin = { true, true, true };
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (markers[x, y] != player) colWin[x] = false;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                if (colWin[i]) return true;
            }

            return false;
        }
        public static bool FindRowWin(Player player, Player[,] markers)
        {
            bool[] rowWin = { true, true, true };
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (markers[x, y] != player) rowWin[y] = false;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                if (rowWin[i]) return true;
            }
            return false;
        }
    }
}
