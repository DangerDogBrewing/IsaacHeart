using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour
{


    [Range(-1f, 1.5f)]
    public float currentSpeed;

    [Tooltip("Average number of time in seconds between spawning")]
    public float spawnRate;

    private Animator anim;
    GameObject currentTarget;
    private Vector3 destination;


    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        destination = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.right * currentSpeed * Time.deltaTime);
        //transform.position = Vector3.Lerp(transform.position, destination, currentSpeed );

        transform.position = Vector3.MoveTowards(transform.position, destination, currentSpeed * Time.deltaTime);


        if (currentTarget == null)
            anim.SetBool("IsAttacking", false);


    }


    void OnMouseDown()
    {
        Debug.Log("you got me!");
    }

    public void SetDestination(Vector3 pos)
    {
        destination = pos;
    }


    public void StrikeCurrentTarget(float damage)
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



    public void SetSpeed(float speed)
    {
        currentSpeed = speed;
    }

    public float GetSpeed()
    {
        return currentSpeed;
    }


    public void Attack(GameObject obj)
    {
        currentTarget = obj;
        if (currentTarget)
        {
            anim.SetBool("IsAttacking", true);
            SetSpeed(0);
        }
        else
        {
            anim.SetBool("IsAttacking", false);
            SetSpeed(1);
        }

    }


}
