using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {


    public float speed;
    public float damage;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject obj = col.gameObject;

        if(obj.GetComponent<Attacker>())
        {
            obj.GetComponent<Health>().TakeDamage(damage);
            Destroy(gameObject);
        }

    }

    }
