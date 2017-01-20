using UnityEngine;
using System.Collections;
using System;

public class Hero : MonoBehaviour
{


    [Range(-1f, 1.5f)]
    public float walkSpeed = 1f;
    public GameObject currentTarget;
    public bool inRange;
    public float damage;

    public float currentWalkSpeed;
    public float condiWalkSpeed;
    public float animWalkSpeed;

    public float attackSpeed = 1;
    public float condiAttackSpeed;

    protected Animator anim;
    protected Vector3 destination;
    protected Vector3 potentialDest;
    protected LineRenderer lineRenderer;
    protected Spellbook spellbook;
    protected Conditions conditions;
    protected Color moveLine = new Color(0.2F, 0.3F, 0.4F, 0.5F);
    protected Color enemyLine = new Color(0.9F, 0.3F, 0.4F, 0.5F);

    // Use this for initialization
   public virtual void Start()
    {

        anim = GetComponent<Animator>();
        destination = transform.position;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.numPositions = 2;

        inRange = false;
        //currentTarget = null;

        spellbook = GetComponent<Spellbook>();
        conditions = GetComponent<Conditions>();

        condiWalkSpeed = walkSpeed;
        animWalkSpeed = walkSpeed;
        condiAttackSpeed = attackSpeed;

        
}


    


// Update is called once per frame
    void Update()
    {
        currentWalkSpeed = Mathf.Min(condiWalkSpeed, animWalkSpeed);       

        lineRenderer.SetPosition(0, transform.position);

        if ( currentTarget && !inRange ) //Currently an enemy target out of range, move towards
        {  
            destination = currentTarget.transform.position;
            transform.position = Vector2.MoveTowards(transform.position, destination , currentWalkSpeed * Time.deltaTime * UniversalSpeed.speed);
            lineRenderer.SetPosition(1, destination);
            anim.SetBool("IsWalking", true);
            anim.SetBool("IsAttacking", false);

        }
        else if (currentTarget && inRange) // has target and target is in range
        {
            anim.SetBool("IsWalking", false);
            anim.SetBool("IsAttacking", true);
            destination = currentTarget.transform.position;
            lineRenderer.SetPosition(1, destination);

        }
        else if( Vector2.Distance(transform.position, destination) > 0.1f ) //move target but no enemy
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, currentWalkSpeed * Time.deltaTime * UniversalSpeed.speed);
            lineRenderer.SetPosition(1, destination);
            anim.SetBool("IsWalking", true);
            anim.SetBool("IsAttacking", false);
        }
        else //idle
        {
            anim.SetBool("IsWalking", false);
            anim.SetBool("IsAttacking", false);
            lineRenderer.SetPosition(1, transform.position);
        }

        //If target no longer exists, stop attacking
        if (currentTarget == null)
        {
            anim.SetBool("IsAttacking", false);
            inRange = false;
        }

        //Slows down animations in SlowMo
        anim.speed = UniversalSpeed.speed * condiAttackSpeed;

        //Rotate if target/destination is behind
        if ((destination.x < transform.position.x) && (transform.rotation.y == 0))
        {
            //transform.Rotate(0, 180, 0);
            //transform.localRotation = Quaternion.Euler(0, 180, 0);
            transform.eulerAngles = new Vector2(0, 180);
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, -1);
            
            //Debug.Log("face back quaternioin " + Quaternion.identity);
        }
        else if ((destination.x > transform.position.x) && (transform.rotation.y != 0))
        {
            //transform.Rotate(0, 180, 0);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1);
            
            //Debug.Log("face forward " + Quaternion.identity);
        }

        //Changes Z order so things lower on y axis are closer to screen
        transform.position = new Vector3(transform.position.x, transform.position.y, (transform.position.y - 10) );


        conditions.ApplyConditions();
    }



   


   


    public void StrikeCurrentTarget()
    {
        Health targetHealth = null;

        if (currentTarget != null)
        {
            targetHealth = currentTarget.GetComponent<Health>();

            if (targetHealth != null)
            {
                targetHealth.TakeDamage(damage);
            }
            else
                Debug.Log("currentTarget " + currentTarget.name + " exists but doesn't have health component");
        }
        else
        {
            Debug.Log("No current target to damage");
        }

    }



    public void SetAnimWalkSpeed(float speedMultiplier)
    {
        Debug.Log("Updating anim walk speed to " + speedMultiplier * walkSpeed);
        animWalkSpeed = walkSpeed * speedMultiplier;
    }





    public void Attack(GameObject obj)
    {
        if (currentTarget != null)
        {
            anim.SetBool("IsAttacking", true);
            //destination = transform.position;
        }
        else
        {
            anim.SetBool("IsAttacking", false);
            inRange = false;
            // SetSpeed(1);
        }

    }

    
    public void CastSpell(Ability ab, GameObject spellTarget)
    {
        Debug.Log("casting " + ab.name + " on " + spellTarget.name);
        ab.target = spellTarget;
        Instantiate(ab);
    }

}
