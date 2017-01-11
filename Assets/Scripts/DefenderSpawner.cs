using UnityEngine;
using System.Collections;

public class DefenderSpawner : MonoBehaviour {

    public Camera myCamera;
    private GameObject defenderParent;
    private SporeDisplay sporeDisplay;


    void Start()
    {
        sporeDisplay = FindObjectOfType<SporeDisplay>();

        defenderParent = GameObject.Find("Defenders");

        if (defenderParent == null)
            defenderParent = new GameObject("Defenders");
    }

    void OnMouseDown()
    {
        GameObject def = DefenderButton.selectedDefender;
        int defCost = def.GetComponent<Defender>().sporeCost;

        if (sporeDisplay.UseSpores(defCost) == SporeDisplay.Status.SUCCESS)
        {
            Vector3 pos = GetGridPoint();
            GameObject defenderObj = Instantiate(DefenderButton.selectedDefender, pos, Quaternion.identity) as GameObject;
            defenderObj.transform.SetParent(defenderParent.transform);
        }
        else
            Debug.Log("Insufficient funds");


        //print(Input.mousePosition);
        //print(GetGridPoint());
    }
    
   Vector3 GetGridPoint()
    {
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;

        float distanceFromCamera = 10f;

        Vector3 weirdTriplet = new Vector3(mouseX, mouseY, distanceFromCamera);
        Vector2 worldPos = myCamera.ScreenToWorldPoint(weirdTriplet);

        Vector3 roundedPos = new Vector3(Mathf.RoundToInt(worldPos.x), Mathf.RoundToInt(worldPos.y), 0f);

        return roundedPos;

        /*x = x / transform.GetComponent<RectTransform>().rect.width * 10;
        y = y / transform.GetComponent<RectTransform>().rect.height * 10;

        x = Mathf.Floor(x);
        y = Mathf.Floor(y);

        return new Vector2(x,y); */
    }
}
