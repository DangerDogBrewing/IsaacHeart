using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Icons : MonoBehaviour {

    private Vector3[] iconPosition = new Vector3[4];


    // Use this for initialization
    void Start () {
        iconPosition[0] = new Vector3(675f, -390f, 0);
        iconPosition[1] = new Vector3(535f, -390f, 0);
        iconPosition[2] = new Vector3(395f, -390f, 0);
        iconPosition[3] = new Vector3(255f, -390f, 0);
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

   
}
