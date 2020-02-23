using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class GameManager : MonoBehaviour {
    public GameObject car;

    public GameObject Storage1,Storage2;
    public Text Clock;
    public Text Fertilizer;

    public ObjectiveList objectiveList;
    public GUIController guiController;
    public DialogList dialogList;

    public bool shouldFarmsCameraOpen;

    //3 phase of cultivation
    bool isCurrentObjStarted;
    bool isCurrentObjWorking;
    bool isCurrentObjCompleted;

    //Permission for moving to storage (crops)
    bool permissionMoveToStorage = false;

    bool isTouched = true;
    bool isTouchMoved = false;

    //Clock timer
    bool startTimer = false;
    float timerTime = 0f;

    bool fertilizerRequestNeed = false;


    public AudioClip clickSound, bubblePopSound, RainSound;
    public AudioSource audioSourceSound;


    // Use this for initialization
    void Start () {
        //when the game starts
        shouldFarmsCameraOpen = true;

        objectiveList = GetComponent<ObjectiveList>();
        Application.targetFrameRate = 60;

        //Crops will appear according to their phase and time*****************************************************************************

        iTweenEvent.GetEvent(car, "incoming").Play();
        StartCoroutine("waitforreached");
    }

    IEnumerator waitforreached()
    {
        yield return new WaitForSeconds(50f);
        iTweenEvent.GetEvent(car, "going").Play();
    }



    IEnumerator PlaySoundDelayed(AudioClip clip, float time)
    {
        yield return new WaitForSeconds(time);

        audioSourceSound.PlayOneShot(clip);

    }




    // Update is called once per frame
    void FixedUpdate () {

        //Mouse Control
        if (shouldFarmsCameraOpen && Input.GetMouseButtonDown(0))
        {
            //check if mouse over GUI
            if (!EventSystem.current.IsPointerOverGameObject() && guiController.isGUIOpen == false) //Since I check isGUIOpen is false so i can't add GUI objectives(sell) into this section
            {

                RaycastHit hit;
                Ray ray;

                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    print(hit.collider.name);

                    //after click agri office
                    if (hit.collider.name.Equals("AgriOffice") && fertilizerRequestNeed == true)
                    {
                        StartCoroutine(PlaySoundDelayed(bubblePopSound, 0.1f));

                        fertilizerRequestNeed = false;
                        //Start Clock
                        timerTime = 5f;
                        startTimer = true;
                        StartCoroutine(WaitForRequestedFertilizer(timerTime));
                    }

                    //for cultivating crops
                    if (objectiveList.GetCurrentObjectiveType().Equals("cultivate") && (hit.collider.name.Equals(objectiveList.GetCurrentObjectiveItem() + "Collider") || hit.collider.tag.Equals("WaterBarrel")))
                    {
                        StartCoroutine(PlaySoundDelayed(clickSound, 0.1f));


                        int fertilizerAmount = PlayerPrefs.GetInt("FertilizerAmount", 0);

                        //check if sufficient fertilizer is available
                        if (fertilizerAmount < objectiveList.GetFertilizerAmount(objectiveList.GetCurrentObjectiveItem()) && isCurrentObjStarted == false)
                        {
                            fertilizerRequestNeed = true;
                            GameObject.Find("FertilizerWarning").transform.Find("Text").GetComponent<Text>().text = "†Zvgvi Kv‡Q ch©vß mvi †bB | \n GLbB wbKU¯’ miKvix K…wl `ßi †_‡K mvi msMÖn Ki | \n <size=11>*(K…wl `ßi Lyu‡R †ei Ki Ges U¨vc Ki)</size> "; //tomar kase porjapto sar nei. ekhni nikotostho sorkari krishi doptor theke saar songroho koro.
                            guiController.ShowFertilizerWarningWindow();
                        }
                        else
                        {
                            //if hit current objctive selection indicator -> appearing water barrel
                            if (hit.collider.transform.parent.Find("SelectionIndicator") && isCurrentObjStarted == false)
                            {
                                isCurrentObjStarted = true;
                                StartCoroutine(SelectionIndicatorAnimEffect(hit.collider.gameObject));
                                GameObject.Find("World").transform.Find(objectiveList.GetCurrentObjectiveItem()).Find("Barrel4").GetComponent<MeshRenderer>().enabled = true;

                                //Crops Expense - money deduct
                                int currentAmount = PlayerPrefs.GetInt("TakaAmount", 1000) - objectiveList.GetCropsExpense(objectiveList.GetCurrentObjectiveItem());
                                PlayerPrefs.SetInt("TakaAmount", currentAmount);
                                GameObject.Find("Taka").transform.Find("Text").GetComponent<Text>().text = currentAmount.ToString();


                                //deduct fertilizer
                                int deductFertilizer = PlayerPrefs.GetInt("FertilizerAmount", 0) - objectiveList.GetFertilizerAmount(objectiveList.GetCurrentObjectiveItem());
                                if (deductFertilizer < 0)
                                {
                                    deductFertilizer = 0;
                                }
                                PlayerPrefs.SetInt("FertilizerAmount", deductFertilizer);
                                GameObject.Find("Fertilizer").transform.Find("Text").GetComponent<Text>().text = deductFertilizer.ToString() + " †KwR";



                                //Next Dialog (2nd cultivation dialog, 1st is declared in guicontroller)
                                int dialogCultivationIndex = PlayerPrefs.GetInt("DialogCultivationIndex");
                                if (dialogCultivationIndex == 0)
                                {
                                    dialogCultivationIndex++;
                                    PlayerPrefs.SetInt("DialogCultivationIndex", dialogCultivationIndex);
                                    StartCoroutine(guiController.WaitForNextDialogAppear(0.5f, dialogList.GetDialogForCultivation(dialogCultivationIndex)));
                                }
                            }

                            //if hit water barrel-> start rain and growing crops and start Clock timer
                            if (hit.collider.tag.Equals("WaterBarrel"))
                            {
                                StartCoroutine(PlaySoundDelayed(clickSound, 0.0f));

                                GameObject.Find("World").transform.Find(objectiveList.GetCurrentObjectiveItem()).Find("Barrel4").GetComponent<MeshRenderer>().enabled = false;

                                GameObject.Find("World").transform.Find(objectiveList.GetCurrentObjectiveItem()).Find("FX_Rain").GetComponent<EllipsoidParticleEmitter>().emit = true;
                                StartCoroutine(StartWettingSoil(GameObject.Find("World").transform.Find(objectiveList.GetCurrentObjectiveItem()).Find("FX_Rain").gameObject)); //timer will start
                            }

                            //if hit seletion indicator after grwing crops -> illustrate complete message, reset current objective and move to next objective
                            if (isCurrentObjCompleted == true)
                            {
                                StartCoroutine(PlaySoundDelayed(clickSound, 0.1f));

                                isCurrentObjCompleted = false;
                                StartCoroutine(SelectionIndicatorAnimEffect(hit.collider.gameObject));
                                CropsBigAndCollectAnimation(); //turn into one big crop and move to storage

                            }
                        }

                    }
                    else
                    {
                        if (hit.collider.transform.parent.Find("SelectionIndicator"))
                        {
                            StartCoroutine(PlaySoundDelayed(clickSound, 0.1f));

                            StartCoroutine(SelectionIndicatorAnimEffect(hit.collider.gameObject));
                        }
                    }

                    //Touch to jelly effect anim
                    if (GetComponent<GUIController>().isGUIOpen == false && hit.collider.gameObject.GetComponent<Animator>())
                    {
                        DoJellyAnim(hit.collider.gameObject);
                    }
                }
            }
        }


        //Touch Control is not fully implemented yet
        /*  foreach (Touch touch in Input.touches)
          {

             int id = touch.fingerId;
              if (EventSystem.current.IsPointerOverGameObject(id))
              {
                  // ui touched
              }

              if (touch.phase == TouchPhase.Began)
              {
                  isTouched = true;
              }
              else if (isTouched == true && touch.phase == TouchPhase.Moved)
              {
                  isTouchMoved = true;
              }
              else if (isTouched == true && isTouchMoved == false &&  touch.phase == TouchPhase.Ended)
              {
                  RaycastHit hit;
                  Ray ray;

                  ray = Camera.main.ScreenPointToRay(touch.position);
                  if (Physics.Raycast(ray, out hit))
                  {
                      print(hit.collider.name);

                      //selection indicator
                      if (hit.collider.transform.parent.Find ("SelectionIndicator")) {

                          hit.collider.transform.parent.Find ("SelectionIndicator").gameObject.GetComponent<SpriteRenderer> ().color = new Color(1,1,1,.5f);
                      }

                      if (GetComponent<GUIController>().isGUIOpen == false && hit.collider.gameObject.GetComponent<Animator>())
                      {
                          //hit.collider.gameObject.GetComponent<Animator>().SetBool("isPressed", true);
                         // StartCoroutine(StopAnim(hit.collider.gameObject));
                      }
                  }
                  isTouched = false;
                  isTouchMoved = false;
              }
              break;
          }

           */



        if (startTimer)
        {
            timerTime -= Time.deltaTime;
            Clock.text = ((int)timerTime).ToString() + " †m‡KÛ";

            if (timerTime <= 0)
            {
                timerTime = 0f;
                startTimer = false;
                
            }
        }

	}

    //Methods are serially

    void DoJellyAnim(GameObject hit)
    {
        StartCoroutine(PlaySoundDelayed(bubblePopSound, 0.1f));

        if (hit.GetComponent<Animator>().enabled)
        {
            hit.gameObject.GetComponent<Animator>().Play("Pressed", -1, 0f); //state name,layer,normalize
        }
        else
        {
            hit.gameObject.GetComponent<Animator>().enabled = true;
        }
    }



	IEnumerator SelectionIndicatorAnimEffect(GameObject hitObject)
	{
        hitObject.transform.parent.Find ("SelectionIndicator").gameObject.GetComponent<SpriteRenderer> ().color = new Color(1,1,1,.2f);
		yield return new WaitForSeconds (0.1f);
		hitObject.transform.parent.Find ("SelectionIndicator").gameObject.GetComponent<SpriteRenderer> ().color = new Color(1,1,1,0f);
    }



    IEnumerator StartWettingSoil(GameObject hitObject)
    {
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(PlaySoundDelayed(RainSound, 0.0f));
        yield return new WaitForSeconds(2f);
        hitObject.transform.parent.Find("SoilWetIndicator").gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, .1f);
        yield return new WaitForSeconds(0.1f);
        hitObject.transform.parent.Find("SoilWetIndicator").gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, .15f);
        yield return new WaitForSeconds(0.1f);
        hitObject.transform.parent.Find("SoilWetIndicator").gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, .2f);
        yield return new WaitForSeconds(0.1f);
        hitObject.transform.parent.Find("SoilWetIndicator").gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, .25f);
        yield return new WaitForSeconds(0.1f);
        hitObject.transform.parent.Find("SoilWetIndicator").gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, .3f);
        yield return new WaitForSeconds(0.1f);
        hitObject.transform.parent.Find("SoilWetIndicator").gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, .35f);
        yield return new WaitForSeconds(0.1f);
        hitObject.transform.parent.Find("SoilWetIndicator").gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, .4f);
        yield return new WaitForSeconds(1f);


        hitObject.transform.parent.Find("FX_Rain").GetComponent<EllipsoidParticleEmitter>().emit = false;
        


        //Next Dialog (3rd cultivation dialog, 2nd is declared in above)
        int dialogCultivationIndex = PlayerPrefs.GetInt("DialogCultivationIndex");
        if (dialogCultivationIndex == 1)
        {
            dialogCultivationIndex++;
            PlayerPrefs.SetInt("DialogCultivationIndex", dialogCultivationIndex);
            StartCoroutine(guiController.WaitForNextDialogAppear(0f, dialogList.GetDialogForCultivation(dialogCultivationIndex)));
        }


        //Start Clock
        timerTime = (float)objectiveList.GetCropsCultivationTime(objectiveList.GetCurrentObjectiveItem());
        startTimer = true;       

        StartCoroutine(StartGrowingCrops(timerTime/5));

        
    }


    IEnumerator StartGrowingCrops(float duration)
    {
        yield return new WaitForSeconds(duration);
        ScalingUpCrops(0.35f);
        yield return new WaitForSeconds(duration);
        ScalingUpCrops(0.25f);
        yield return new WaitForSeconds(duration);
        ScalingUpCrops(0.20f);
        yield return new WaitForSeconds(duration);
        ScalingUpCrops(0.1f);
        yield return new WaitForSeconds(duration);
        ScalingUpCrops(0.0f);

        //Growing Complete
        isCurrentObjCompleted = true;


        //Next Dialog (4th cultivation dialog, 3rd is declared in above at StartWettingSoil)
        int dialogCultivationIndex = PlayerPrefs.GetInt("DialogCultivationIndex");
        if (dialogCultivationIndex == 2)
        {
            dialogCultivationIndex++; //dialogCultivationIndex will be 3 and it is the last index of array
            PlayerPrefs.SetInt("DialogCultivationIndex", dialogCultivationIndex);
            StartCoroutine(guiController.WaitForNextDialogAppear(0f, dialogList.GetDialogForCultivation(dialogCultivationIndex))); 
        }

    }


    void ScalingUpCrops(float SoilWetIndicatorAlpha)
    {
            foreach (Transform child in GameObject.Find("World").transform.Find(objectiveList.GetCurrentObjectiveItem()))
            {
                if (child.tag.Equals("CropsModel"))
                {
                    Vector3 currScale = child.transform.localScale;
                    if (currScale.x < 1.0f)
                    {
                        child.transform.localScale = new Vector3(currScale.x + 0.2f, currScale.y + 0.2f, currScale.z + 0.2f);
                    }

                    GameObject.Find("World").transform.Find(objectiveList.GetCurrentObjectiveItem()).Find("SoilWetIndicator").gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, SoilWetIndicatorAlpha);
                }
            }
    }


    //this animation will appear after crops fully grown
    void CropsBigAndCollectAnimation()
    {
        foreach (Transform child in GameObject.Find("World").transform.Find(objectiveList.GetCurrentObjectiveItem()))
        {
            if (child.tag.Equals("CropsModel") && child.transform.Find("Big"))
            {
                if (child.GetComponent<Animator>().enabled)
                {
                    child.GetComponent<Animator>().Play(objectiveList.GetCurrentObjectiveItem() + "BigAndCollectAnim", -1, 0f); //state name,layer,normalize
                }
                else {
                    child.GetComponent<Animator>().enabled = true;
                }
                //Call Storage animation
                StartCoroutine(WaitForCropsAnimFinished(child.gameObject));
            }
            else if (child.tag.Equals("CropsModel"))
            {
                child.transform.localScale = Vector3.zero;
            }

        }
    }


    IEnumerator WaitForCropsAnimFinished(GameObject child)
    {
        yield return new WaitForSeconds(1.54f); //Do not change to 1.55f otherwise crops will scale up to 2f;
        //Storage Animation after getting crops
        if (objectiveList.GetCurrentObjectiveItem() == "Corn")
        {
            DoJellyAnim(Storage2.gameObject);
        }else {
            DoJellyAnim(Storage1.gameObject);
        }

        //Show Success and next obj from GUIController
        guiController.StartCoroutine("ShowSuccess");

        //Add Crops amount to storage or market
        //previous cropsAmount + cropsAmountTostore
        PlayerPrefs.SetInt(objectiveList.GetCurrentObjectiveItem() + "Amount", PlayerPrefs.GetInt(objectiveList.GetCurrentObjectiveItem() + "Amount", 0) + objectiveList.GetCropsAmountToStore() );
        GameObject.Find("Sell").transform.Find(objectiveList.GetCurrentObjectiveItem()).transform.Find("Text").GetComponent<Text>().text = PlayerPrefs.GetInt(objectiveList.GetCurrentObjectiveItem() + "Amount").ToString();


        //Set Earned Skill Point
        int skillPoint = PlayerPrefs.GetInt("SkillPoint", 0) + 10;
        PlayerPrefs.SetInt("SkillPoint", skillPoint);
        GameObject.Find("Skill").transform.Find("Text").GetComponent<Text>().text = skillPoint.ToString();


        //Set Next Objective // Here if player reached to last objective show Full game complete Window
        if (PlayerPrefs.GetInt("CurrentObjective") < objectiveList.TotalNumberOfObjective-1)
        {
            //Reset
            ResetCurrentObj(); 
        }

        objectiveList.SetNextObjective();

    }


    void ResetCurrentObj()
    {
        isCurrentObjStarted = false;
        isCurrentObjWorking = false;
        isCurrentObjCompleted = false;

    }


    IEnumerator WaitForRequestedFertilizer(float time)
    {
        yield return new WaitForSeconds(time);
        int increasedAmountFer = PlayerPrefs.GetInt("FertilizerAmount", 0) + 10;
        PlayerPrefs.SetInt("FertilizerAmount", increasedAmountFer);
        GameObject.Find("Fertilizer").transform.Find("Text").GetComponent<Text>().text = increasedAmountFer.ToString() + " †KwR";

        GameObject.Find("FertilizerWarning").transform.Find("Text").GetComponent<Text>().text = "Zzwg 10 †KwR mvi †c‡qQ |";
        guiController.ShowFertilizerWarningWindow();

    }




}