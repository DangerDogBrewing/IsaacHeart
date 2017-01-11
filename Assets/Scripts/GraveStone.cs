using UnityEngine;
using System.Collections;

public class GraveStone : MonoBehaviour {

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }


void OnTriggerStay2D(Collider2D col)
    {
        Attacker attacker = col.GetComponent<Attacker>();

        Debug.Log("somebodys in here");
        if(attacker)
        {
            anim.SetTrigger("underAttack");
        }

    }
}
