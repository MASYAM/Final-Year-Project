using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour {


    bool shouldTimeOn = false;

    float Timertime = 5f;

    float test = 0.9f;
    float final;


	// Use this for initialization
	void Start () {
		
	}



    private void OnTriggerStay(Collider other)
    {
        //GetComponent<SpriteRenderer>().color = Color.yellow;

        /* if (other.gameObject.GetComponent<Drag>().isTouchExit)
         {
             GetComponent<SpriteRenderer>().color = Color.white;
             other.transform.position = transform.position;
             other.gameObject.GetComponent<Collider>().enabled = false;
         }*/

        if (other.gameObject.name == "Towel" && other.gameObject.name == GameObject.Find("GameManager").GetComponent<ObjectiveList>().GetCurrentObjectiveItem())
        {
            shouldTimeOn = true;
        }
        

    }

    private void OnTriggerExit(Collider other)
    {      
       // GetComponent<SpriteRenderer>().color = Color.white;
        shouldTimeOn = false;
    }

   


    // Update is called once per frame
    void Update () {

        if (shouldTimeOn && Timertime > 0)
        {
            Timertime -= Time.deltaTime;
          
            //Task complete
            if (Timertime <= 0)
            {
                Timertime = 0f;
                GameObject.Find("GameManager").GetComponent<ParentsGameManager>().AfterParentalSafetySuccess(this.gameObject);
            }

            test -= 0.005f;
            final = Mathf.Round(test * 100f) / 100f;

            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, final);

        }

	}
}
