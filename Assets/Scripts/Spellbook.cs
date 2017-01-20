using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spellbook : MonoBehaviour {

    public Ability[] abilities;
    private AbilityIcon[] abIcons;
    private Hero caster;

    private UI_Icons ui_icons;

    // Use this for initialization
    void Start () {
        

        abIcons = new AbilityIcon[abilities.Length];
        caster = GetComponent<Hero>();

        OnLevelWasLoaded();
    }

    void OnLevelWasLoaded()
    {
        ui_icons = GameObject.FindObjectOfType<UI_Icons>();
        if (!ui_icons)
        {
            Debug.LogWarning("no icon parent!");
        }
        else
            abIcons = ui_icons.GetComponentsInChildren<AbilityIcon>();

        //abIcons = ui_icons.InitializeAbIcons(abilities, caster);
        CloseAbilities();
    }


    public void OpenAbilities()
    {

        int counter = 0;
        foreach (Ability ab in abilities)
        {
            ab.icon.caster = caster;
            abIcons[counter].Copy(ab.icon);
            abIcons[counter].transform.localScale = new Vector3(1f, 1f, 1f);
            counter++;
        }


      //  foreach (AbilityIcon abIcon in abIcons)
      //  {
       //     abIcon.transform.localScale = new Vector3(1f, 1f, 1f);
       // }
    }

    public void CloseAbilities()
    {
        foreach (AbilityIcon abIcon in abIcons)
        {
            abIcon.transform.localScale = new Vector3(0f, 0f, 0f);
           // abIcon.enabled = false;
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
