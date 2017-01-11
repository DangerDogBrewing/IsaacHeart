using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

    LevelManager levelmanager;

    void Start()
    {
        levelmanager = GameObject.FindObjectOfType<LevelManager>();
    }

    void OnMouseDown()
    {
        levelmanager.LoadLevel("01a Start");
    }

}
