using UnityEngine;
using System.Collections;

public class LoseCollider : MonoBehaviour {

    private LevelManager levelmanager;

    void Start()
    {
        levelmanager = FindObjectOfType<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        AI_Hero myAttacker = col.gameObject.GetComponent<AI_Hero>();
        if (myAttacker)
            levelmanager.LoadLevel("03b Lose");

    }
}
