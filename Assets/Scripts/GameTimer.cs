using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameTimer : MonoBehaviour {

    public int gameTime = 60;
    public AudioClip winclip;

    private GameObject enemySpawners;
    private LevelManager levelmanager;
    private Slider progressSlider;
    private AudioSource audiosource;
    private GameObject winmessage;    
    private bool haveWon;


	// Use this for initialization
	void Start () {
        haveWon = false;

        progressSlider = GetComponent<Slider>();
        progressSlider.maxValue = gameTime;

        levelmanager = FindObjectOfType<LevelManager>();
        audiosource = new AudioSource();

        enemySpawners = GameObject.Find("EnemySpawners");
        if (!enemySpawners)
            Debug.LogWarning("Could not find object EnemySpawners");

        foreach(Transform child in transform)
        {
            if (child.name == "Win Message")
                winmessage = child.gameObject;
        }
	}
	
	// Update is called once per frame
	void Update () {

        progressSlider.value += Time.deltaTime;

        if ((progressSlider.value >= progressSlider.maxValue)&&(haveWon == false))
            WinLevel();
	}

    void WinLevel()
    {
        winmessage.gameObject.SetActive(true);

        AudioSource.PlayClipAtPoint(winclip, new Vector3(0f,0f,0f));
        Destroy(enemySpawners);
        
        Invoke("LoadNextLevel", 4);

        haveWon = true;      
    }


   

    void LoadNextLevel()
    {
        levelmanager.LoadNextLevel();
    }

}
