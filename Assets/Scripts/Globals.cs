using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Globals : Singleton<Globals> {

    protected Globals() { }

    public AbilityIcon selectedAbIcon;  //Icon that was most recently pressed
    public Hero selectedHero;   //Hero that was most recently selected

}
