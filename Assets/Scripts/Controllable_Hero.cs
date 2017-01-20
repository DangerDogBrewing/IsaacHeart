using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controllable_Hero : Hero {

    public Vector2 formationPos;


    public override void Start()
    {
        base.Start();

        DontDestroyOnLoad(transform.gameObject);
        if (formationPos == new Vector2(0, 0))
            formationPos = transform.position;
    }

    void OnLevelWasLoaded()
    {
        //Puts heros into their formation position (if it exists)
        transform.position = formationPos;
        destination = transform.position;
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
            if (hit.collider.gameObject.GetComponent<AI_Hero>() )
            {
                currentTarget = hit.collider.gameObject;
                lineRenderer.startColor = enemyLine;
                lineRenderer.endColor = enemyLine;
                //Debug.Log("attacking new target " + hit.collider.gameObject.transform.name);
            }
            else if (hit.collider.gameObject.GetComponent<AbilityIcon>())
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
            if (hit.collider.gameObject.GetComponent<AI_Hero>() )
            {
                currentTarget = hit.collider.gameObject;
                lineRenderer.startColor = enemyLine;
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
