using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Hero))]
public class Melee_Hero : MonoBehaviour
{

    private Hero hero;
    private Animator anim;
    public float range = 1;


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
            float distance = Vector2.Distance(hero.transform.position, hero.currentTarget.transform.position);
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

    

}
