using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class Hero : MonoBehaviour
{


    public float walkSpeed = 10f;
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
    protected float yceiling = 45f;
    protected Globals globals;
    protected bool isSelected = false;
    protected SpriteRenderer selectionCircle;
    protected Image uiSelectedFace;
    protected SpriteRenderer myFace;

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

        globals = FindObjectOfType<Globals>();

        Transform selectionCircleChild = transform.FindChild("Selection_Circle");
        selectionCircle = selectionCircleChild.GetComponent<SpriteRenderer>();
        selectionCircle.enabled = false;

        uiSelectedFace = GameObject.Find("SelectedFace/Center/FaceImage").GetComponent<Image>();
        if (uiSelectedFace == null)
            Debug.LogWarning("Could not find UI Face element");

        //Iterate through all children to find "Head"
        Transform faceobject = FindFace(transform);

        if (faceobject == null)
            Debug.LogWarning("Wheeeres my faaaaace");
        else
            myFace = faceobject.GetComponent<SpriteRenderer>();

    }





    // Update is called once per frame
    public virtual void Update()
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
        transform.position = new Vector3(transform.position.x, transform.position.y, (transform.position.y - yceiling) );


        conditions.ApplyConditions();
    }


    protected virtual void OnMouseDown()
    {

        //An ability is queued up
        if (globals.selectedAbIcon && globals.selectedAbIcon.isSelected)
        {
            if (globals.selectedAbIcon.caster != this) // Cast spell on me
            {
                globals.selectedAbIcon.caster.CastSpell(globals.selectedAbIcon.ab, this.gameObject);
                globals.selectedAbIcon.Unselect();

            }
            else //Selecting self 
            {
                globals.selectedAbIcon.Unselect();
                Debug.Log("Selecting self with own ability");
            }
        }
        else //No ability queued up
        {
            SelectMe();
            globals.selectedHero = this;
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



    public void SetAnimWalkSpeed(float speedMultiplier)
    {
        //Debug.Log("Updating anim walk speed to " + speedMultiplier * walkSpeed);
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


    void SelectMe()
    {
        UnselectOthers();
        selectionCircle.enabled = true;
        isSelected = true;
        uiSelectedFace.sprite = myFace.sprite;
    }

    void UnselectOthers()
    {
        Hero[] heroes = FindObjectsOfType<Hero>();
        foreach (Hero h in heroes)
        {
            h.isSelected = false;
            h.selectionCircle.enabled = false;
        }
    }

    //Recursively looks through all children, and if it finds "head" piece, returns that
    protected Transform FindFace(Transform transform_in)
    {
        foreach (Transform child in transform_in)
        {
            //Debug.Log("child component " + child.gameObject.name);
            if (Regex.IsMatch(child.gameObject.name, "Head_Hair", RegexOptions.None))
            {               
                //Debug.Log("found it!");
                return child;
            }
            else
            {
                Transform grandchild = FindFace(child);
                if (grandchild != null)
                    return grandchild;
            }            
        }

        return null;
    }


}
