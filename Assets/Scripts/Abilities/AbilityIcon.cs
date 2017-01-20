using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityIcon : MonoBehaviour {

    public Ability ab;
    public Hero caster;

    public GameObject currentTarget;
    private bool choosingTarget = false;
    private Image my_sprite;
    

	// Use this for initialization
	void Start () {
        Transform image_child = transform.Find("Center/AbImage");
         my_sprite = image_child.GetComponent<Image>();

    }

    public void Copy(AbilityIcon abIcon)
    {
        ab = abIcon.ab;
        caster = abIcon.caster;
        currentTarget = abIcon.currentTarget;

        Debug.Log("Copying to " + abIcon.name);

        Transform image_child_in = abIcon.transform.Find("Center/AbImage");
        Image sprite_in = image_child_in.GetComponent<Image>();
        my_sprite.sprite = sprite_in.sprite;
        
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
        if (caster)
            caster.CastSpell(ab, currentTarget);
        else
            Debug.Log("No caster for spell " + ab.name);        
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
