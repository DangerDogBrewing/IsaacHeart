using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Icons : MonoBehaviour {

    private Vector3[] iconPosition = new Vector3[4];
    private AbilityIcon[] icons;

    // Use this for initialization
    void Start () {
        icons = GetComponentsInChildren<AbilityIcon>();
    }

    public AbilityIcon[] InitializeAbIcons(Ability[] abilities, Hero caster)
    {
        AbilityIcon[] abIcons = new AbilityIcon[abilities.Length];

        int counter = 0;
        foreach (Ability ab in abilities)
        {
            AbilityIcon abIcon = Instantiate(ab.icon);
            abIcon.transform.SetParent(transform, false);

            //abIcon.transform.parent = transform;
            abIcon.caster = caster;
            //abIcon.transform.position = new Vector3(iconPosition[counter].x * 0.1f, iconPosition[counter].y * 0.1f, iconPosition[counter].z * 0.1f);
            //abIcon.transform.localScale = new Vector3(1f, 1f, 1f);
            abIcon.transform.position = iconPosition[counter];


            // abIcon.transform.localScale = new Vector3(level_canvas.transform.localScale.x, level_canvas.transform.localScale.y, level_canvas.transform.localScale.z);

            abIcons[counter] = abIcon;
            counter++;

            Debug.Log("position for " + abIcon.name + " position " + abIcon.transform.position);
        }

        return abIcons;
    }

   
    public void UnselectAll()
    {
        foreach (AbilityIcon abIcon in icons)
        {
            abIcon.Unselect();
        }
    }

}
