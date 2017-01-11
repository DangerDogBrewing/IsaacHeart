using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class MusicManager : MonoBehaviour {

    public AudioClip[] LevelMusicChangeArray;
    private AudioSource audioSource;


    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Debug.Log("Don't destroy on load: " + name);
    }

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();

        PlayerPrefsManager.CheckFirstRun();
        audioSource.volume = PlayerPrefsManager.GetMasterVolume();
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnLevelWasLoaded(int level)
    {
        AudioClip thisLevelMusic = LevelMusicChangeArray[level];
        Debug.Log("Playing clip: " + thisLevelMusic);


        
        if ((thisLevelMusic)&& (audioSource.clip != thisLevelMusic))
        {
            audioSource.Stop();
            audioSource.clip = thisLevelMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void ChangeVolume(float volumeIn)
    {
        audioSource.volume = volumeIn;
    }

}
