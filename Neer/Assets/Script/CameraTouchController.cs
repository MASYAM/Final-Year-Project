using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CameraTouchController : MonoBehaviour
{  
    float panSpeed = 5.0f;       // Speed of the camera when being panned

    private Vector3 touchOrigin;    // Position of cursor when mouse dragging starts
    private bool isPanning;     // Is the camera being panned?
    Vector3 pos;

    public GUIController guiController;
    public ParentsGameManager parentsGameManager;


    private static readonly float[] BoundsX = new float[] { -145.5f, 51.6f };
    private static readonly float[] BoundsZ = new float[] { -130f, 95.7f };

     void Update()
     {
        if (parentsGameManager.shouldParentalCameraOpen == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!EventSystem.current.IsPointerOverGameObject() && guiController.isGUIOpen == false)
                {
                    touchOrigin = Input.mousePosition;
                    isPanning = true;
                }
            }

            // Disable movements on button release
            if (!Input.GetMouseButton(0)) isPanning = false;

            // Move the camera on it's XY plane
            if (isPanning)
            {
                pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - touchOrigin);

                Vector3 move = new Vector3(-pos.y * panSpeed, 0, pos.x * panSpeed);
                transform.Translate(move, Space.World);


                // Ensure the camera remains within bounds.
                Vector3 posNew = transform.position;
                posNew.x = Mathf.Clamp(transform.position.x, BoundsX[0], BoundsX[1]);
                posNew.z = Mathf.Clamp(transform.position.z, BoundsZ[0], BoundsZ[1]);
                transform.position = posNew;
            }
        }

     }
}





/* foreach (Touch touch in Input.touches)
           {
               if (touch.phase == TouchPhase.Began)
               {
                   touchOrigin = new Vector3(touch.position.x, touch.position.y, 0f);

                   isPanning = true;
               }


               if (isPanning && touch.phase == TouchPhase.Moved)
               {
                   pos = Camera.main.ScreenToViewportPoint(new Vector3(touch.position.x, touch.position.y, 0f) - touchOrigin);

                   Vector3 move = new Vector3(-pos.y * panSpeed, 0, pos.x * panSpeed);
                   transform.Translate(move, Space.World);

               }

               if (touch.phase == TouchPhase.Ended)
                   isPanning = false;
           } */



