using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour {

    Vector3 dist;
    float posX;
    float posY;

    public bool isTouchExit;

	// Use this for initialization
	void Start () {
        isTouchExit = false;
	}

    
    private void OnMouseDown()
    {

        dist = GameObject.Find("CameraForParentalCare").GetComponent<Camera>().WorldToScreenPoint(transform.position);
        posX = Input.mousePosition.x - dist.x;
        posY = Input.mousePosition.y - dist.y;
        isTouchExit = false;
    }

    private void OnMouseDrag()
    {
        Vector3 currentPos = new Vector3(Input.mousePosition.x - posX, Input.mousePosition.y - posY, dist.z);
        Vector3 worldPos = GameObject.Find("CameraForParentalCare").GetComponent<Camera>().ScreenToWorldPoint(currentPos);
        transform.position = worldPos;
        isTouchExit = false;
    }


    private void OnMouseUp()
    {
        isTouchExit = true;
    } 

    // Update is called once per frame
    void Update () {

        /*
        foreach (Touch touch in Input.touches)
        {

            //RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero);

            if (touch.phase == TouchPhase.Began)
                {
                        dist = Camera.main.WorldToScreenPoint(transform.position);
                        posX = touch.position.x - dist.x;
                        posY = touch.position.y - dist.y;
                        isTouchExit = false;
                    
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    
                        Vector3 currentPos = new Vector3(touch.position.x - posX, touch.position.y - posY, dist.z);
                        Vector3 worldPos = Camera.main.ScreenToWorldPoint(currentPos);
                        transform.position = worldPos;
                        isTouchExit = false;
                    
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    isTouchExit = true;
                }
           
        }*/

    }
}
