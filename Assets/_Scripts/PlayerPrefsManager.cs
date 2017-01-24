using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour {

    const string MASTER_VOLUME_KEY = "master_volume";
    const string DIFFICULTY_KEY = "difficulty";
    const string LEVEL_KEY = "level_unlocked_";  //use as "level_unlocked_1 = 1" or "level_unlocked_5 = 0"
    const string FIRST_RUN = "first_run"; //check if this is first time running
    
    public static void SetMasterVolume(float volume)
    {
        if (volume >= 0f && volume <= 1f)
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
        else
            Debug.LogError("Master Volume out of range");
    }

    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
    }

    public static void SetUnlockLevel(int level)
    {
        if (level <= SceneManager.sceneCountInBuildSettings - 1)
            PlayerPrefs.SetInt(LEVEL_KEY + level.ToString(), 1);  //use 1 for true
        else
            Debug.LogError("Level set" + level + " out of range");
    }

    public static bool IsUnlockLevel(int level)
    {
        if (PlayerPrefs.GetInt(LEVEL_KEY + level.ToString()) == 1)
            return true;
        else
            return false;
    }


    public static void SetDifficulty(float difficulty)
    {
        if ((difficulty >= 1) && (difficulty <= 3))
        {
            PlayerPrefs.SetFloat(DIFFICULTY_KEY, difficulty);
            Debug.Log("Set difficulty to: " + difficulty);
        }
        else
            Debug.LogError("Difficulty out of range" + difficulty);
    }

    public static float GetDifficulty()
    {
        float difficulty = PlayerPrefs.GetFloat(DIFFICULTY_KEY);
        Debug.Log("Returning saved difficulty: " + difficulty);
        return difficulty;
    }

    public static void CheckFirstRun()
    {
        if(PlayerPrefs.GetInt(FIRST_RUN)==0)
        {
            PlayerPrefs.SetInt(FIRST_RUN, 1);
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, 1);
            PlayerPrefs.SetFloat(DIFFICULTY_KEY, 2);
        }
    }
    


}
