using UnityEngine;
using System.Collections;
using System;

public class Shooter : MonoBehaviour {

    public GameObject projectile;

    private GameObject projectileParent;
    private Animator anim ;
    private EnemySpawner mySpawner;

    void Start()
    {

        mySpawner = null;
        EnemySpawner[] allSpawners = FindObjectsOfType<EnemySpawner>();

        foreach(EnemySpawner currSpawner in allSpawners)
        {
            if (currSpawner.transform.position.y == transform.position.y)
                mySpawner = currSpawner;
        }

        if (mySpawner == null)
            Debug.LogWarning("Could not find spawner");

        anim = GetComponent<Animator>();
        projectileParent = GameObject.Find("Projectiles");

        if(projectileParent == null)
            projectileParent = new GameObject("Projectiles");

    }

    void Update()
    {
       
        if (IsAttackerAheadInLane())
        {
            anim.SetBool("isAttacking", true);
        }
        else
        {
            anim.SetBool("isAttacking", false);
        }

    }

    private bool IsAttackerAheadInLane()
    {
        //if there are children
        if (mySpawner)
        {
            if (mySpawner.transform.childCount > 0)
            {
                foreach (Transform child in mySpawner.transform)
                {
                    //if any of those children are ahead of this defender
                    if (child.transform.position.x > transform.position.x)
                        return true;
                }
            }
        }
        
        return false;
        
    }

    private void Fire()
    {
       GameObject newProjectile = Instantiate(projectile);

        if (projectileParent)
        {
            Transform myBody = transform.GetChild(0);
            Transform myLauncher = myBody.GetChild(0);
            newProjectile.transform.position = myLauncher.position;
            newProjectile.transform.SetParent(projectileParent.transform);

        }
        else
            Debug.Log("No projectile parent");

    }


    
}
