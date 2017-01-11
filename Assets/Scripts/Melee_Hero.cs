using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Hero))]
public class Melee_Hero : MonoBehaviour
{

    private Hero hero;
    private Animator anim;

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

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject obj = col.gameObject;
         // Debug.Log(gameObject.name + " collided with " + obj.name);
        

        if(obj == hero.currentTarget)
            hero.Attack(obj);

        //if (col.GetComponent<Attacker>())
        //  hero.Attack(obj);


    }


}
