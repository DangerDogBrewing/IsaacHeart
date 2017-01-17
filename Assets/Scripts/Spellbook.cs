﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spellbook : MonoBehaviour {

    public Ability[] abilities;
    private Vector3[] iconOffset = new Vector3[4];
    private AbilityIcon[] abIcons;

	// Use this for initialization
	void Start () {
        iconOffset[0] = new Vector3(-.3f, 1f, -1);
        iconOffset[1] = new Vector3(0, 1, -1);
        iconOffset[2] = new Vector3(.3f, 1, -1);
        iconOffset[3] = new Vector3(.6f, 1, -1);

        abIcons = new AbilityIcon[abilities.Length];
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenAbilities()
    {
        int counter = 0;
        foreach (Ability ab in abilities)
        {
            AbilityIcon abIcon = Instantiate(ab.icon);
            abIcons[counter] = abIcon;

            abIcon.transform.position = transform.position + iconOffset[counter];

            
            counter++;
        }

    }

    public void CloseAbilities()
    {
        foreach (AbilityIcon abIcon in abIcons)
        {

            Destroy(abIcon.gameObject);
           // Destroy(abIcon);
            Debug.Log("destroyed icon");
        }
    }


}