using UnityEngine;
using System.Collections;

public class Attacker : MonoBehaviour {


    [Range(-1f, 1.5f)] public float currentSpeed;

    [Tooltip ("Average number of time in seconds between spawning")]
    public float spawnRate;

    private Animator anim;
    GameObject currentTarget;


    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update () {
        transform.Translate(Vector3.left * currentSpeed * Time.deltaTime * UniversalSpeed.speed);

        //Slows down animation in slomode
        anim.speed = UniversalSpeed.speed;


        if (currentTarget == null)
            anim.SetBool("IsAttacking", false);


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
        if(currentTarget)
            anim.SetBool("IsAttacking", true);
        else
            anim.SetBool("IsAttacking", false);

    }


}
