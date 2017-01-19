using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conditions : MonoBehaviour {

    private Hero hero;
    public List<Condition> cond_list;

	// Use this for initialization
	void Start () {

        hero = GetComponent<Hero>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddCondition(Condition cond)
    {
        cond_list.Add(cond);
    }

    public void RemoveCondition(Condition cond)
    {
        cond_list.Remove(cond);
    }

    public void ApplyConditions()
    {
        for(int i = 0; i < cond_list.Count; i++)
        {
            cond_list[i].duration -= Time.deltaTime * UniversalSpeed.speed;
            if (cond_list[i].duration <= 0)
            {
                cond_list[i].EndCondition();
                RemoveCondition(cond_list[i]);
            }
            else
                cond_list[i].ApplyCondition();
        }
    }
}
