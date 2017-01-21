using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityIcon : MonoBehaviour {

    public Ability ab;
    public Hero caster;
    public bool isSelected = false;

    public GameObject currentTarget;
    private bool choosingTarget = false;
    private Image my_image;
    private Globals globals;
    private Image center_image;

    private Color unselectedColor = new Color(1f, 1f, 1f, 1f);
    private Color selectedColor = new Color(.2f, .2f, .2f, 1f);

	// Use this for initialization
	void Start () {
        Transform image_child = transform.Find("Center/AbImage");
         my_image = image_child.GetComponent<Image>();
        if (!my_image)
            Debug.Log("Unable to find AbImage of " + name);

        Transform center_child = transform.Find("Center");
        center_image = center_child.GetComponent<Image>();
        if (!center_image)
            Debug.Log("Unable to find CenterImage of " + name);
        
        globals = FindObjectOfType<Globals>();
    }

    public void Copy(AbilityIcon abIcon)
    {        
        ab = abIcon.ab;
        caster = abIcon.caster;
        currentTarget = abIcon.currentTarget;
        name = abIcon.name;

        //Debug.Log("Copying to " + abIcon.name);
        Transform image_child_in = abIcon.transform.Find("Center/AbImage");
        if (!image_child_in)
            Debug.Log("Unable to find AbImage of " + abIcon.name);
        Image sprite_in = image_child_in.GetComponent<Image>();
        my_image.sprite = sprite_in.sprite;
        
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
        Select();
    }

   

    public void Select()
    {
        globals.selectedAbIcon = this;
        isSelected = true;
        center_image.color = selectedColor;       

        Debug.Log("Selecting " + name);
    }

    public void Unselect()
    {
        isSelected = false;
        center_image.color = unselectedColor;
        Debug.Log("Unselecting " + name);
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
