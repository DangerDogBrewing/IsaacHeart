using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cond_Chill : Condition {

    private float originalWalkSpeed;
    public float chillSpeed = 0.3f;

    public Cond_Chill(float duration_in, GameObject target_in) : base (duration_in, target_in)
    {
        originalWalkSpeed = target.GetComponent<AI_Hero>().walkSpeed;
    }

    public override void ApplyCondition()
    {
        //Debug.Log("applying chill from " + origin + " to " + target);

        float newWalkSpeed = originalWalkSpeed * chillSpeed;

        if (newWalkSpeed < target.GetComponent<AI_Hero>().condiWalkSpeed)
            target.GetComponent<AI_Hero>().condiWalkSpeed = newWalkSpeed;
    }

    public override void EndCondition()
    {
        // Debug.Log("ending chill from " + origin + " to " + target);
        target.GetComponent<AI_Hero>().condiWalkSpeed = originalWalkSpeed;

    }
}
