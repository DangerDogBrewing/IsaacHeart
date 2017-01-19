using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condition : MonoBehaviour {

    public  float duration;
    public  GameObject target;
    public Hero origin;
    
    protected Condition(float duration_in, GameObject target_in)
    {
        duration = duration_in;
        target = target_in;
    }

    public abstract void ApplyCondition();
    public abstract void EndCondition();


}
