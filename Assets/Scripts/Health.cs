using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    public float health;
    public float maxHealth;
    public Image healthbar;

	// Use this for initialization
	void Start () {
	


	}
	
	// Update is called once per frame
	void Update () {

        //Updates health bar 
        float healthRatio = Mathf.Clamp(health / maxHealth, 0f, 1f);

        healthbar.transform.localScale = new Vector3(healthRatio, healthbar.transform.localScale.y, healthbar.transform.localScale.z);

	}

    public void TakeDamage(float damage)
    {
        Debug.Log("taking damage!");
        health -= damage;
        if (health <= 0)
           Destroy(gameObject);
    }
}
