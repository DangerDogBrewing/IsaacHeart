using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Hero))]
public class Ranged_Hero : MonoBehaviour
{

    private Hero hero;
    private Animator anim;
    public float range=5;
    public Projectile projectile;


    // Use this for initialization
    void Start()
    {
        hero = GetComponent<Hero>();
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (hero.currentTarget)
        {
            float distance = Vector3.Distance(hero.transform.position, hero.currentTarget.transform.position);
           // Debug.Log("Distance to target " + distance);
                if (distance <= range)
                {
                    hero.Attack(hero.currentTarget);
                    hero.inRange = true;
                }
                else
                {
                    hero.inRange = false;
                }
            }
        

    }

    private void Fire()
    {
        Projectile newProjectile = Instantiate(projectile);
               
        newProjectile.transform.position = transform.position + new Vector3(0,.5f, 0);
        newProjectile.transform.SetParent(transform);
        newProjectile.target = hero.currentTarget;

       
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject obj = col.gameObject;
        // Debug.Log(gameObject.name + " collided with " + obj.name);

      

    }

    void OnTriggerExit2D(Collider2D col)
    {
        GameObject obj = col.gameObject;
     
    }

}
