using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AI_Hero))]
public class Fox : MonoBehaviour {

    private AI_Hero attacker;
    private Animator anim;

    // Use this for initialization
    void Start () {
        attacker = GetComponent<AI_Hero>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject obj = col.gameObject;

        //Debug.Log(gameObject.name + " collided with " + col.gameObject.name);

        if (col.tag == "Defender")
            attacker.Attack(obj);

    }

    void Jump()
    {
            anim.SetTrigger("jumpTrigger");

    }

}
