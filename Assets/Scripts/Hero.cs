using UnityEngine;
using System.Collections;
using System;

public class Hero : MonoBehaviour
{


    [Range(-1f, 1.5f)]
    public float speed;

    private Animator anim;
    public GameObject currentTarget;
    private Vector3 destination;
    private Vector3 potentialDest;
    private LineRenderer lineRenderer;
    public bool inRange;
    private Spellbook spellbook;
    
    public float damage;

    private Color moveLine = new Color(0.2F, 0.3F, 0.4F, 0.5F);
    private Color enemyLine = new Color(0.9F, 0.3F, 0.4F, 0.5F);

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        destination = transform.position;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.numPositions = 2;

        inRange = false;
        currentTarget = null;

        spellbook = GetComponent<Spellbook>();

}

// Update is called once per frame
void Update()
    {
        
        lineRenderer.SetPosition(0, transform.position);

        if ( currentTarget && !inRange ) //Currently an enemy target out of range, move towards
        {  
            destination = currentTarget.transform.position;
            transform.position = Vector2.MoveTowards(transform.position, destination , speed * Time.deltaTime * UniversalSpeed.speed);
            lineRenderer.SetPosition(1, destination);
            anim.SetBool("IsWalking", true);
        }
        else if (currentTarget && inRange) // has target and target is in range
        {
            anim.SetBool("IsWalking", false);
            destination = currentTarget.transform.position;
            lineRenderer.SetPosition(1, destination);

        }
        else if( Vector2.Distance(transform.position, destination) > 0.1f ) //move target but no enemy
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime * UniversalSpeed.speed);
            lineRenderer.SetPosition(1, destination);
            anim.SetBool("IsWalking", true);
        }
        else //idle
        {
            anim.SetBool("IsWalking", false);
            lineRenderer.SetPosition(1, transform.position);
        }

        //If target no longer exists, stop attacking
        if (currentTarget == null)
        {
            anim.SetBool("IsAttacking", false);
            inRange = false;
        }

        //Slows down animations in SlowMo
        anim.speed = UniversalSpeed.speed;

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

    }



    //Hero is selected, drag line to move to or attack enemy
    void OnMouseDown()
    {
       // Debug.Log("you got me!");
        lineRenderer.SetPosition(0, transform.position);
       // lineRenderer.numPositions = 2;
        anim.SetTrigger("Hop");
        UniversalSpeed.SlowMo();  //slows down time to allow planning
        
        if (spellbook)
           spellbook.OpenAbilities();
        
    }

    void OnMouseDrag()
    {
        lineRenderer.SetPosition(1, GetMousePos());
        lineRenderer.SetPosition(0, transform.position);

       CheckUnderPointerDrag();
    }

    void OnMouseUp()
    {
        UniversalSpeed.NormalSpeed();

        CheckUnderPointerLift();

        if (spellbook)
            spellbook.CloseAbilities();
    }


    void CheckUnderPointerDrag()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider)
        {
            if (hit.collider.gameObject.GetComponent<Attacker>())
            {
                currentTarget = hit.collider.gameObject;
                lineRenderer.startColor = enemyLine;
                lineRenderer.endColor = enemyLine;
                //Debug.Log("attacking new target " + hit.collider.gameObject.transform.name);
            }
            else if(hit.collider.gameObject.GetComponent<AbilityIcon>())
            {
                //hit.collider.gameObject.GetComponent<AbilityIcon>().PrepareToCast();

                //Debug.Log("Casting spell: " + hit.collider.gameObject.transform.name);
            }
            else
            {
                currentTarget = null;
                lineRenderer.startColor = moveLine;
                lineRenderer.endColor = moveLine;
                destination = GetGridPoint();

            }
        }
        else
        {

            currentTarget = null;
            lineRenderer.startColor = moveLine;
            lineRenderer.endColor = moveLine;
            destination = GetGridPoint();

        }
    }


    void CheckUnderPointerLift()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider)
        {
            if (hit.collider.gameObject.GetComponent<Attacker>())
            {
                currentTarget = hit.collider.gameObject;
                lineRenderer.startColor=enemyLine;
                lineRenderer.endColor = enemyLine;
                //Debug.Log("attacking new target " + hit.collider.gameObject.transform.name);
            }
            else if (hit.collider.gameObject.GetComponent<AbilityIcon>())
            {
                hit.collider.gameObject.GetComponent<AbilityIcon>().PrepareToCast();
                destination = transform.position;

                //Debug.Log("Casting spell: " + hit.collider.gameObject.transform.name);
            }
            else
            {
                currentTarget = null;
                lineRenderer.startColor = moveLine;
                lineRenderer.endColor = moveLine;
                destination = GetGridPoint();

            }
        }
        else
        {

            currentTarget = null;
            lineRenderer.startColor = moveLine;
            lineRenderer.endColor = moveLine;
            destination = GetGridPoint();

        }
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



    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public float GetSpeed()
    {
        return speed;
    }


    public void Attack(GameObject obj)
    {
        if (currentTarget != null)
        {
            anim.SetBool("IsAttacking", true);
            destination = transform.position;
        }
        else
        {
            anim.SetBool("IsAttacking", false);
            inRange = false;
            // SetSpeed(1);
        }

    }

    Vector3 GetGridPoint()
    {
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;

        float distanceFromCamera = 10f;

        Vector3 weirdTriplet = new Vector3(mouseX, mouseY, distanceFromCamera);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(weirdTriplet);

        //Vector3 roundedPos = new Vector3(Mathf.RoundToInt(worldPos.x), Mathf.RoundToInt(worldPos.y), 0f);
        Vector3 mousePos = new Vector3(worldPos.x, worldPos.y, 0f);

        return mousePos;
    }


    Vector3 GetMousePos()
    {
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;

        float distanceFromCamera = 10f;

        Vector3 weirdTriplet = new Vector3(mouseX, mouseY, distanceFromCamera);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(weirdTriplet);

       Vector3 mapPos = new Vector3(worldPos.x, worldPos.y, 0f);

        return mapPos;

    }

}
