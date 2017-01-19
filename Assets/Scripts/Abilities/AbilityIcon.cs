using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityIcon : MonoBehaviour {

    public Ability ab;
    public Vector3 pos;


    public GameObject currentTarget;
    private bool choosingTarget = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if(choosingTarget)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CheckUnderPointer();
            }
        }

	}

   public void PrepareToCast()
    {

        UniversalSpeed.SlowMo();
        choosingTarget = true;

    }

    void CastSpell()
    {
        Debug.Log("casting " + ab.name + " on " + currentTarget.name);
        ab.target = currentTarget;
        Instantiate(ab);
    }

    void OnMouseDown()
    {

    }

    void OnMouseDrag()
    {
     
    }

    void OnMouseUp()
    {
        
    }


    void CheckUnderPointer()
    {
        
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.GetComponent<AI_Hero>())
                {
                    currentTarget = hit.collider.gameObject;
                    CastSpell();
                    UniversalSpeed.NormalSpeed();
                    choosingTarget = false;

                }
                else if (hit.collider.gameObject.GetComponent<AbilityIcon>())
                {

                    UniversalSpeed.NormalSpeed();

                    choosingTarget = false;

                    Debug.Log("Canceled spell  " + hit.collider.gameObject.transform.name);
                }
                else
                {
                    currentTarget = null;
                 
                }
            }
            else
            {
                currentTarget = null;              
            }
        }
    



}
