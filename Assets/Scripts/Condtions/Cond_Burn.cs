using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cond_Burn : Condition {

    public float burnDamage = 10f;
    private float nextTic;
    

    public Cond_Burn(float duration_in, GameObject target_in) : base (duration_in, target_in)
    {
        nextTic = duration;
        shortName = "Burning";
    }

    public override void ApplyCondition()
    {
      if (duration <= nextTic)
        {
           // Debug.Log("applying burn from " + origin + " to " + target);
            target.GetComponent<Health>().TakeDamage(burnDamage);
            nextTic--;
        }
    }

    public override void EndCondition()
    {

    }
}
