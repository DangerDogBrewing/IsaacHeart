using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConditionsWindow : MonoBehaviour {

    public Text[] ui_cond_list = new Text[5];    
    public Globals globals;

	// Use this for initialization
	void Start () {
       ui_cond_list = GetComponentsInChildren<Text>();
       globals = FindObjectOfType<Globals>();
    }

    // Update is called once per frame
    void Update () {
        //Clear conditions window
        foreach (Text ui_cond in ui_cond_list)
        {
            ui_cond.text = "";
        }
        Hero hero = globals.selectedHero;
        //Add all of the selected hero's conditions to the window
        if (hero != null)
        {
            Conditions conditions = hero.GetComponent<Conditions>();
            int counter = 0;
            foreach (Condition condi in conditions.cond_list)
            {
                //Debug.Log("cond name " + condi.shortName + " counter " + counter);
                ui_cond_list[counter].text = condi.shortName;
                counter++;
            }
        }
       

	}
}
