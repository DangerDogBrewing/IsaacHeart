using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsController : MonoBehaviour {

    public Slider volumeSlider;
    public Slider difficultySlider;
    public LevelManager levelManager;

    private MusicManager musicManager;

	// Use this for initialization
	void Start () {
        musicManager = GameObject.FindObjectOfType<MusicManager>();
        volumeSlider.value = PlayerPrefsManager.GetMasterVolume();
        difficultySlider.value = PlayerPrefsManager.GetDifficulty();
        Debug.Log("Setting volume slider to " + PlayerPrefsManager.GetMasterVolume());
        Debug.Log("Setting difficulty slider to " + PlayerPrefsManager.GetDifficulty());


    }

    // Update is called once per frame
    void Update () {
        musicManager.ChangeVolume(volumeSlider.value);
	}


    public void SaveAndExit()
    {
        PlayerPrefsManager.SetMasterVolume(volumeSlider.value);
        PlayerPrefsManager.SetDifficulty(difficultySlider.value);

        Debug.Log("Setting volume pref to " + volumeSlider.value);
        Debug.Log("Setting difficulty pref to " + difficultySlider.value);

        levelManager.LoadLevel("01a Start");
    }


    public void SetDefaults()
    {

        PlayerPrefsManager.SetMasterVolume(0.8f);
        PlayerPrefsManager.SetDifficulty(2f);
        volumeSlider.value = PlayerPrefsManager.GetMasterVolume();
        difficultySlider.value = PlayerPrefsManager.GetDifficulty();
        Debug.Log("Setting Default values");
    }

    
}
