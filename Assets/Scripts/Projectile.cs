using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {


    public float speed;
    public float damage;
    public GameObject target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (target)
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime * UniversalSpeed.speed);
        else
        {
            Debug.Log("No target for projectile");
            Destroy(gameObject);
        }
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
