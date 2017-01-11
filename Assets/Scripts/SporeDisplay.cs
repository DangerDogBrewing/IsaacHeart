using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SporeDisplay : MonoBehaviour {

    private Text myText;
    private int spores = 100;
    public enum Status { SUCCESS, FAILURE};

	// Use this for initialization
	void Start () {
        myText = GetComponent<Text>();
        myText.text = spores.ToString();

    }

    // Update is called once per frame
    void Update () {
	}

    public void AddSpores(int sporesAdded)
    {
        spores += sporesAdded;
        myText.text = spores.ToString();
    }

    public Status UseSpores (int sporesRemoved)
    {
        if (spores >= sporesRemoved)
        {
            spores -= sporesRemoved;
            myText.text = spores.ToString();
            return Status.SUCCESS;
        }

        return Status.FAILURE;        

    }

}
