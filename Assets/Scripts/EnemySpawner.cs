using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject[] myEnemies;
	
    
    void Spawn(GameObject enemyPrefab)
    {
        GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity) as GameObject;
        newEnemy.transform.SetParent(transform);
    }
    
    
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        foreach (GameObject myEnemy in myEnemies)
        {
            if(isTimeToSpawn(myEnemy))
             Spawn(myEnemy);
        }
	}


    bool isTimeToSpawn(GameObject myEnemy)
    {
        float rate = myEnemy.GetComponent<Attacker>().spawnRate;

        float spawnsPerSecond = 1 / rate;

       // if (Time.deltaTime > spawnsPerSecond)
           // Debug.LogWarning("Spawn rate faster than frame rate");

        float threshold = spawnsPerSecond * Time.deltaTime / 5;

        if (Random.value < threshold)
            return true;

        return false;
    }
}
