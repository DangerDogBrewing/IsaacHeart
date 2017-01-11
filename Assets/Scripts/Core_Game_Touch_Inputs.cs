using UnityEngine;
using System.Collections;

public class Core_Game_Touch_Inputs : MonoBehaviour
{

    public Camera myCamera;
    
    private SporeDisplay sporeDisplay;
    public Hero hero;

    private GameObject mySelection;

    void Start()
    {
        
    }

    void OnMouseDown()
    {


        Vector3 pos = GetGridPoint();
        hero.SetDestination(pos);

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
