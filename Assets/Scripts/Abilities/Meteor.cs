using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ability))]
public class Meteor : MonoBehaviour {


    public float speed;
    public Animator anim;

    private Ability ab;

	// Use this for initialization
	void Start () {

        transform.position = new Vector3(0, 10, 0);
        ab = GetComponent<Ability>();
	}
	
	// Update is called once per frame
	void Update () {

        if (ab.target)
            transform.position = Vector3.MoveTowards(transform.position, ab.target.transform.position, speed * Time.deltaTime * UniversalSpeed.speed);
        else
        {
            Debug.Log("No target for meteor");
            Destroy(gameObject);
        }

    }
}
