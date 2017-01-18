using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Condition))]
public class Cond_Chill : MonoBehaviour {


    private Condition cond;

	// Use this for initialization
	void Start () {
        cond = GetComponent<Condition>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
