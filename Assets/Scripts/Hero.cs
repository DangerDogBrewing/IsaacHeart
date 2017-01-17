using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour
{


    [Range(-1f, 1.5f)]
    public float speed;

    private Animator anim;
    public GameObject currentTarget;
    private Vector3 destination;
    private Vector3 potentialDest;
    private LineRenderer lineRenderer;
    private bool firstCommandGiven;
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
        firstCommandGiven = false;
        inRange = false;
        currentTarget = null;

        spellbook = GetComponent<Spellbook>();
}

// Update is called once per frame
void Update()
    {

        
        if ( currentTarget && !inRange ) //Currently an enemy target out of range, move towards
        {  
            destination = currentTarget.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, destination , speed * Time.deltaTime * UniversalSpeed.speed);
            lineRenderer.SetPosition(1, destination);
            anim.SetBool("IsWalking", true);
        }
        else if (currentTarget && inRange) // has target and target is in range
        {
            anim.SetBool("IsWalking", false);
            destination = currentTarget.transform.position;
            lineRenderer.SetPosition(1, destination);
        }
        else if( Vector3.Distance(transform.position, destination) > 0.1f ) //move target but no enemy
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime * UniversalSpeed.speed);
            lineRenderer.SetPosition(1, destination);
            anim.SetBool("IsWalking", true);
        }
        else //idle
        {
            anim.SetBool("IsWalking", false);
        }

        //If target no longer exists, stop attacking
        if (currentTarget == null)
        {
            anim.SetBool("IsAttacking", false);
            inRange = false;
        }

        //Slows down animations in SlowMo
        anim.speed = UniversalSpeed.speed;

        //Sets draw line start at hero
     if(firstCommandGiven)
        lineRenderer.SetPosition(0, transform.position);

    }

    //Hero is selected, drag line to move to or attack enemy
    void OnMouseDown()
    {
        Debug.Log("you got me!");
        lineRenderer.SetVertexCount(2);
        lineRenderer.SetPosition(0, transform.position);
        anim.SetTrigger("Hop");
        UniversalSpeed.SlowMo();  //slows down time to allow planning



        if (spellbook)
           spellbook.OpenAbilities();
        

    }

    void OnMouseDrag()
    {
         potentialDest = GetGridPoint();
        lineRenderer.SetPosition(1, GetMousePos());

        CheckUnderPointer();
    }

    void OnMouseUp()
    {
        SetDestination(potentialDest);
        firstCommandGiven = true;
        UniversalSpeed.NormalSpeed();

        if (spellbook)
            spellbook.CloseAbilities();
    }


    void CheckUnderPointer()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.GetComponent<Attacker>())
            {
                currentTarget = hit.collider.gameObject;
                lineRenderer.SetColors(enemyLine, enemyLine);
                Debug.Log("attacking new target " + hit.collider.gameObject.transform.name);
            }
            else if(hit.collider.gameObject.GetComponent<AbilityIcon>())
            {
                Debug.Log("Casting spell: " + hit.collider.gameObject.transform.name);
            }
            else
            {
                currentTarget = null;
                lineRenderer.SetColors(moveLine, moveLine);
            }
        }
        else
        {

            currentTarget = null;
            lineRenderer.SetColors(moveLine, moveLine);
        }
    }
    

    public void SetDestination(Vector3 pos)
    {
        destination = pos;
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

        Vector3 roundedPos = new Vector3(Mathf.RoundToInt(worldPos.x), Mathf.RoundToInt(worldPos.y), 0f);

        return roundedPos;
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
