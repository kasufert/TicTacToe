using UnityEngine;

public class MarkerScript : MonoBehaviour
{
    public Player team;
    public Sprite naughtSprite;

    private void Start()
    {        
        if (team == Player.Naught)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = naughtSprite;
        }
    }

    void Update()
    {
    }
}
