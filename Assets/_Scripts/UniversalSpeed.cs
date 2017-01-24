using UnityEngine;
using System.Collections;

public class UniversalSpeed : MonoBehaviour {


    public static float speed;


	// Use this for initialization
	void Start () {
        speed = 1f;
	}
	
    public static void SlowMo()
    {
        speed = 0.05f;
    }

   public static void NormalSpeed()
    {
        speed = 1f;
    }
	
}
