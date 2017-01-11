using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour
{


    [Range(-1f, 1.5f)]
    public float currentSpeed;

    private Animator anim;
    GameObject currentTarget;
    private Vector3 destination;
    private Vector3 potentialDest;
    private LineRenderer lineRenderer;
    private bool isMoving;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        destination = transform.position;
        lineRenderer = GetComponent<LineRenderer>();
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.right * currentSpeed * Time.deltaTime);
        //transform.position = Vector3.Lerp(transform.position, destination, currentSpeed );

        transform.position = Vector3.MoveTowards(transform.position, destination, currentSpeed * Time.deltaTime);


        if (currentTarget == null)
            anim.SetBool("IsAttacking", false);

     if(isMoving)
        lineRenderer.SetPosition(0, transform.position);

    }


    void OnMouseDown()
    {
        Debug.Log("you got me!");
        lineRenderer.SetVertexCount(2);
        lineRenderer.SetPosition(0, transform.position);
    
    }

    void OnMouseDrag()
    {
         potentialDest = GetGridPoint();
        lineRenderer.SetPosition(1, GetMousePos());
    }

    void OnMouseUp()
    {
        SetDestination(potentialDest);
        isMoving = true;
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
          //  SetSpeed(0);
        }
        else
        {
            anim.SetBool("IsAttacking", false);
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
