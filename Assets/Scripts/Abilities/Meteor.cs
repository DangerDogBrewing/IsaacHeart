using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour {


    public GameObject target;
    public float speed;
    public Animator anim;

	// Use this for initialization
	void Start () {

        transform.position = new Vector3(0, 10, 0);

	}
	
	// Update is called once per frame
	void Update () {

        if (target)
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime * UniversalSpeed.speed);
        else
        {
            Debug.Log("No target for meteor");
            Destroy(gameObject);
        }

    }
}
