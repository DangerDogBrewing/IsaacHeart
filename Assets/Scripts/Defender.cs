using UnityEngine;
using System.Collections;

public class Defender : MonoBehaviour {

    public int sporeCost=30;

    private SporeDisplay sporeDisplay;
       

    void Start()
    {
        sporeDisplay = FindObjectOfType<SporeDisplay>();
    }

	void AddSpores(int sporesIn)
    {
        sporeDisplay.AddSpores(sporesIn);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
       // Debug.Log(gameObject.name + " collided with " + col.gameObject.name);

    }

}
