using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ParentsGameManager : MonoBehaviour {


    public ObjectiveList objectiveList;
    public GUIController guiController;
    public DialogList dialogList;

    public bool shouldParentalCameraOpen;

    public Camera cameraForParentalCare;

    public GameObject foodObjectives,safetyObjectives;

    public AudioClip clickSound, bubblePopSound, rightSound, wrongSound;
    public AudioSource audioSourceSound;

    bool isTappedRight = false;



    List<GameObject> tempListOfRightORWrongObj = new List<GameObject>();


    // Use this for initialization
    void Start () {
        //when the game starts
        shouldParentalCameraOpen = false;


        //if player run the game from parental care objectives then visible parental care objective
        if (PlayerPrefs.GetInt("CurrentObjective") < objectiveList.TotalNumberOfObjective &&  objectiveList.GetCurrentObjectiveType() == "FoodCare")
        {         
            foreach (GameObject foodObj in objectiveList.foodObjectivesList)
            {
                if (foodObj.name == PlayerPrefs.GetInt("CurrentObjectiveParentalCare").ToString())
                    foodObj.SetActive(true);
            }

        }
        else if(PlayerPrefs.GetInt("CurrentObjective") < objectiveList.TotalNumberOfObjective &&  objectiveList.GetCurrentObjectiveType().Equals("SafetyCare"))
        {

            foreach (GameObject safetyObj in objectiveList.safetyObjectivesList)
            {
                if (safetyObj.name == PlayerPrefs.GetInt("CurrentObjectiveParentalCare").ToString())
                    safetyObj.SetActive(true);
            }
            
        }



    }


    IEnumerator PlaySoundDelayed(AudioClip clip, float time)
    {
        yield return new WaitForSeconds(time);

        audioSourceSound.PlayOneShot(clip);

    }



    // Update is called once per frame
    void Update () {


        //Mouse Control                            
        if (shouldParentalCameraOpen && Input.GetMouseButtonDown(0) && PlayerPrefs.GetInt("DialogIntroParentsIndex", 0) == 4) //For avoiding accidental tap during dialog, turn touch off until intro dialog is finish
        {
            //check if mouse over GUI
            if (!EventSystem.current.IsPointerOverGameObject() && guiController.isGUIOpen == false) //Since I check isGUIOpen is false so i can't add GUI objectives(sell) into this section
            {

                RaycastHit hit;
                Ray ray;

                ray = cameraForParentalCare.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    print(hit.collider.name);


                    //for cultivating crops
                    if (objectiveList.GetCurrentObjectiveType().Equals("FoodCare") )
                    {

                        if (hit.collider.name == objectiveList.GetCurrentObjectiveItem() && isTappedRight == false)
                        {
                            StartCoroutine(PlaySoundDelayed(clickSound, 0.0f));
                            StartCoroutine(PlaySoundDelayed(bubblePopSound, 0.2f));
                            StartCoroutine(PlaySoundDelayed(rightSound, 0.6f));
                            //success
                            //do anim then call corotine
                            if (hit.collider.transform.parent.GetComponent<Animator>().enabled)
                            {
                                hit.collider.transform.parent.GetComponent<Animator>().Play("ParentalFood", -1, 0f);
                            }
                            else {
                                hit.collider.transform.parent.GetComponent<Animator>().enabled = true;
                            }

                            StartCoroutine( WaitForRightFoodAnimComplete(0.5f, hit.collider.transform.Find("RightOrWrong").gameObject, hit.collider.transform.parent.parent.gameObject)  );

                            isTappedRight = true;
                            tempListOfRightORWrongObj.Add(hit.collider.transform.Find("RightOrWrong").gameObject);

                            // StartCoroutine(WaitAfterParentalCareActionDone(1.5f, foodObjectives.transform.Find(PlayerPrefs.GetInt("CurrentObjectiveParentalCare", 0).ToString()).gameObject ));

                        }
                        else if (foodObjectives.transform.Find(PlayerPrefs.GetInt("CurrentObjectiveParentalCare", 0).ToString()).transform.Find(hit.collider.name) && isTappedRight == false)
                        {
                            StartCoroutine(PlaySoundDelayed(clickSound, 0.0f));
                            StartCoroutine(PlaySoundDelayed(bubblePopSound, 0.2f));
                            StartCoroutine(PlaySoundDelayed(wrongSound, 0.6f));
                            //wrong
                            //give a lesson
                            //deduct skill point 
                            //also give warning of deduct skill point
                            if (hit.collider.transform.parent.GetComponent<Animator>().enabled)
                            {
                                hit.collider.transform.parent.GetComponent<Animator>().Play("ParentalFood", -1, 0f);
                            }
                            else
                            {
                                hit.collider.transform.parent.GetComponent<Animator>().enabled = true;
                            }

                            isTappedRight = false;

                            StartCoroutine(WaitForWrongFoodAnimComplete(0.5f, hit.collider.transform.Find("RightOrWrong").gameObject));
                            tempListOfRightORWrongObj.Add(hit.collider.transform.Find("RightOrWrong").gameObject);

                        }

                           

                    }
                    else if (objectiveList.GetCurrentObjectiveType().Equals("SafetyCare"))
                    {

                        if (hit.collider.name == objectiveList.GetCurrentObjectiveItem())
                        {
                            //success
                            //do anim then call corotine
                            //StartCoroutine(WaitAfterParentalCareActionDone(1.5f, safetyObjectives.transform.Find(PlayerPrefs.GetInt("CurrentObjectiveParentalCare", 0).ToString()).gameObject ));

                        }
                        else if (safetyObjectives.transform.Find(PlayerPrefs.GetInt("CurrentObjectiveParentalCare", 0).ToString()).transform.Find(hit.collider.name))
                        {
                             //wrong
                             
                        }
   

                    }


                }
            }
        }

    }





    IEnumerator WaitForRightFoodAnimComplete(float time, GameObject Right ,GameObject foodObj)
    {
        yield return new WaitForSeconds(time);
        //show right or wrong icon
        Right.GetComponent<SpriteRenderer>().enabled = true;


        yield return new WaitForSeconds(1.5f);

        //hide all right or wrong icon
        foreach (GameObject rightOrWrongObj in tempListOfRightORWrongObj)
        {
            rightOrWrongObj.GetComponent<SpriteRenderer>().enabled = false;
        }


        //hide current foodObjective
        foodObj.SetActive(false);

        //set next objective
        objectiveList.SetNextObjective();
        //Show Success and next obj from GUIController
        guiController.StartCoroutine("ShowSuccess");

        //reset tapRightOrWrong
        isTappedRight = false;
        tempListOfRightORWrongObj.Clear();

        //Set Earned Skill Point
        int skillPoint = PlayerPrefs.GetInt("SkillPoint", 0) + 5;

        PlayerPrefs.SetInt("SkillPoint", skillPoint);
        GameObject.Find("Skill").transform.Find("Text").GetComponent<Text>().text = skillPoint.ToString();

    }



    IEnumerator WaitForWrongFoodAnimComplete(float time, GameObject Wrong)
    {
        yield return new WaitForSeconds(time);
        Wrong.GetComponent<SpriteRenderer>().enabled = true;

        yield return new WaitForSeconds(1f);
        StartCoroutine(guiController.WaitForNextDialogAppear(0.0f, "†Zvgvi DËiwU mwVK bq | \n <size=10>*(cÖwZwU fzj DË‡i `¶Zv c‡q›U 5 K‡i K‡i K‡g hv‡e)</size>" ) );

        //Deduct Skill Point for penalty
        int skillPoint = PlayerPrefs.GetInt("SkillPoint", 0) - 5;

        if (skillPoint < 0) {
            skillPoint = 0;
        }

        PlayerPrefs.SetInt("SkillPoint", skillPoint);
        GameObject.Find("Skill").transform.Find("Text").GetComponent<Text>().text = skillPoint.ToString();

    }



    //this method is called from target script which is attached with safety objectives items
    public void AfterParentalSafetySuccess(GameObject targettedSafetyCareObj) //targettedSafetyCareObj is like floorwater,brokenglass
    {

        //Hide current safetyObjective
        targettedSafetyCareObj.transform.parent.gameObject.SetActive(false);

        //set next objective
        objectiveList.SetNextObjective();
        //Show Success and next obj from GUIController
        guiController.StartCoroutine("ShowSuccess");

       
        //Set Earned Skill Point
        int skillPoint = PlayerPrefs.GetInt("SkillPoint", 0) + 5;

        PlayerPrefs.SetInt("SkillPoint", skillPoint);
        GameObject.Find("Skill").transform.Find("Text").GetComponent<Text>().text = skillPoint.ToString();
        
    }


}
