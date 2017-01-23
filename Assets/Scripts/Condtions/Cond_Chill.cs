using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cond_Chill : Condition {

    private float originalWalkSpeed;
    private float originalAttackSpeed;
    public float chillSpeed = 0.3f;
  

    public Cond_Chill(float duration_in, GameObject target_in) : base (duration_in, target_in)
    {
        originalWalkSpeed = target.GetComponent<Hero>().walkSpeed;
        originalAttackSpeed = target.GetComponent<Hero>().attackSpeed;
        shortName = "Chilled";
    }

    public override void ApplyCondition()
    {
        //Debug.Log("applying chill from " + origin + " to " + target);

        float newWalkSpeed = originalWalkSpeed * chillSpeed;

        if (newWalkSpeed < target.GetComponent<Hero>().condiWalkSpeed)
            target.GetComponent<Hero>().condiWalkSpeed = newWalkSpeed;

        float newAttackSpeed = originalAttackSpeed * chillSpeed;

        if (newAttackSpeed < target.GetComponent<Hero>().condiAttackSpeed)
            target.GetComponent<Hero>().condiAttackSpeed = newAttackSpeed;
    }

    public override void EndCondition()
    {
        // Debug.Log("ending chill from " + origin + " to " + target);
        target.GetComponent<Hero>().condiAttackSpeed = originalAttackSpeed;
        target.GetComponent<Hero>().condiWalkSpeed = originalWalkSpeed;


    }
}
