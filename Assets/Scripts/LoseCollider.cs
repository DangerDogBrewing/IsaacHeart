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
        Attacker myAttacker = col.gameObject.GetComponent<Attacker>();
        if (myAttacker)
            levelmanager.LoadLevel("03b Lose");

    }
}
