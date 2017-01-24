using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ability))]
public class Fire_Explosion : MonoBehaviour {


    public float speed;
    public Animator anim;
    private bool followTarget = true;
    public ParticleSystem ps;

    private Ability ab;

	// Use this for initialization
	void Start () {

        Vector3 offset = new Vector3(0, 0f, 0f);

        ab = GetComponent<Ability>();
        transform.position = ab.target.transform.position + offset;

        ps.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
        Instantiate(ps);

    }

    // Update is called once per frame
    void Update () {

        if (ab.target) 
        {
            transform.position = ab.target.transform.position;

        }
        else
        {
            Debug.Log("No target for ice_explosion");
            Destroy(gameObject);
        }

    }



    public void EndSpell()
    {
        Conditions conds = ab.target.GetComponent<Conditions>();
        if(conds)
        {
            Condition burn = new Cond_Burn(5, ab.target);
            conds.AddCondition(burn);

        }

        else
            Debug.Log("target " + ab.target.name + " has no conditions class");
        

        Destroy(ab.gameObject);
    }


}

