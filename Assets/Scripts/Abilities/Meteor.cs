using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ability))]
public class Meteor : MonoBehaviour {


    public float speed;
    public Animator anim;
    private bool followTarget = true;
    public float damage;

    private Ability ab;

	// Use this for initialization
	void Start () {

        transform.position = new Vector3(-55, 55, 0);
        ab = GetComponent<Ability>();
	}
	
	// Update is called once per frame
	void Update () {

        

        if (ab.target) 
        {
            if ((Vector2.Distance(transform.position, ab.target.transform.position) > 9f) && followTarget )
            {
                transform.position = Vector2.MoveTowards(transform.position, ab.target.transform.position, speed * Time.deltaTime * UniversalSpeed.speed);
            }
            else
            {
                // anim.explode
                
                anim.SetTrigger("ExplodeTrigger");
                followTarget = false;
               // Debug.Log("explode!");
            }

        }
        else
        {
            Debug.Log("No target for meteor");
           // Destroy(gameObject);
        }

    }

    public void DamageTarget()
    {
        if(ab.target)
            ab.target.GetComponent<Health>().TakeDamage(damage);
    }


    public void EndSpell()
    {
        Destroy(gameObject);
    }


}

