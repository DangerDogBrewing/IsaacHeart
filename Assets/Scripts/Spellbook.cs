using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spellbook : MonoBehaviour {

    public Ability[] abilities;
    private Vector3[] iconOffset = new Vector3[4];
    private AbilityIcon[] abIcons;
    private GameObject icon_parent;
    private Hero caster;

	// Use this for initialization
	void Start () {
        iconOffset[0] = new Vector3(-.7f, 1f, -1);
        iconOffset[1] = new Vector3(0, 1.1f, -1);
        iconOffset[2] = new Vector3(.7f, 1f, -1);
        iconOffset[3] = new Vector3(.8f, 1, -1);

        abIcons = new AbilityIcon[abilities.Length];

        

        caster = GetComponent<Hero>();

        //Also call everything from here
        OnLevelWasLoaded();

    }

    void OnLevelWasLoaded()
    {
        icon_parent = GameObject.Find("Icons");
        if (!icon_parent)
        {
            icon_parent = new GameObject("Icons");
            Instantiate(icon_parent);
        }

        InitializeAbIcons();

    }
    

    public void InitializeAbIcons()
    {
        int counter = 0;
        foreach (Ability ab in abilities)
        {
            AbilityIcon abIcon = Instantiate(ab.icon);
            abIcon.caster = caster;
            abIcon.transform.parent = icon_parent.transform;
            abIcons[counter] = abIcon;
            counter++;
            abIcon.transform.localScale = new Vector3(0, 0, 0);
        }
    }

    public void OpenAbilities()
    {
        int counter = 0;
        foreach (AbilityIcon abIcon in abIcons)
        {            
            abIcon.transform.localScale = new Vector3(.2f, .2f, 0);
            abIcon.transform.position = transform.position + iconOffset[counter];
            counter++;

        }

    }

    public void CloseAbilities()
    {
        foreach (AbilityIcon abIcon in abIcons)
        {            
            //Hide Gameobject instead of destroy
            abIcon.transform.localScale = new Vector3(0, 0, 0);         
           
        }
    }

    public void DestroyAbIcons()
    {
        foreach (AbilityIcon abIcon in abIcons)
        {
            Destroy(abIcon);
        }
    }

}
