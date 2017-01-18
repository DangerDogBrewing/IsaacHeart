using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ability))]
public class Ice_Explosion : MonoBehaviour {


    public float speed;
    public Animator anim;
    private bool followTarget = true;

    private Ability ab;

	// Use this for initialization
	void Start () {

        Vector3 offset = new Vector3(0, 0f, 0f);

        ab = GetComponent<Ability>();
        transform.position = ab.target.transform.position + offset;

    }

    // Update is called once per frame
    void Update () {

        if (ab.target) 
        {
            transform.position = ab.target.transform.position;

        }
        else
        {
            Debug.Log("No target for meteor");
            Destroy(gameObject);
        }

    }



    public void EndSpell()
    {
        Destroy(gameObject);
    }


}

