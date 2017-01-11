using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DefenderButton : MonoBehaviour {

    public static GameObject selectedDefender;
    public GameObject defenderType;
    private Text costText;

    private DefenderButton[] buttonArray;

    void Start()
    {
        buttonArray = GameObject.FindObjectsOfType<DefenderButton>();


        costText = GetComponentInChildren<Text>();
        if (!costText)
            Debug.LogWarning(name + " has no cost");

        int sporeCost = defenderType.GetComponent<Defender>().sporeCost;


        costText.text = sporeCost.ToString();


    }

    void OnMouseDown()
    {
        Debug.Log(name + " button pressed");
        foreach (DefenderButton currButton in buttonArray)
        {
            currButton.GetComponent<SpriteRenderer>().color = Color.black;
        }

        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        selectedDefender = defenderType;
        
    }
}
