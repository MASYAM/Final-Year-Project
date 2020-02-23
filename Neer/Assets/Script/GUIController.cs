using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{

    public Image objectiveWindow;
    public bool isGUIOpen;
    public Animator SuccessAnimator;
    public Animator NextObjAnimator;
    public ObjectiveList objectiveList;
    public DialogList dialogList;

    public GameObject[] disableGameobjectForParentalCare;
    public GameObject cameraForParentalCare;
    public GameObject goToFarmButton, goToParentsKitchenButton, goToParentsLivingAndFoodButton, foodInstructionButton;
    public GameObject LivingAndFood, Kitchen;
    public GameObject Towel,SmallCleaner,BigCleaner;

    public Sprite SpriteOfObjButtonForParents, SpriteOfObjButtonForFarm;

    public AudioClip clickSound, coinSound, levelCompleteSound, rightSound, wrongSound;
    public AudioSource audioSourceSound;


    // Use this for initialization

    void Start()
    {
        isGUIOpen = false;

        goToFarmButton.SetActive(false);
        goToParentsKitchenButton.SetActive(false);
        foodInstructionButton.SetActive(false);

        //Set Crops Amount to Sell Center

        GameObject.Find("Sell").transform.Find("Pumpkin").transform.Find("Text").GetComponent<Text>().text = PlayerPrefs.GetInt("PumpkinAmount", 0).ToString();
        GameObject.Find("Sell").transform.Find("Watermelon").transform.Find("Text").GetComponent<Text>().text = PlayerPrefs.GetInt("WatermelonAmount", 0).ToString();
        GameObject.Find("Sell").transform.Find("Carrot").transform.Find("Text").GetComponent<Text>().text = PlayerPrefs.GetInt("CarrotAmount", 0).ToString();
        GameObject.Find("Sell").transform.Find("Cabbage").transform.Find("Text").GetComponent<Text>().text = PlayerPrefs.GetInt("CabbageAmount", 0).ToString();
        GameObject.Find("Sell").transform.Find("Corn").transform.Find("Text").GetComponent<Text>().text = PlayerPrefs.GetInt("CornAmount", 0).ToString();


        //Set Remaining Taka & Skill & Fertilizer
        GameObject.Find("Taka").transform.Find("Text").GetComponent<Text>().text = PlayerPrefs.GetInt("TakaAmount", 1000).ToString() + "$";
        GameObject.Find("Skill").transform.Find("Text").GetComponent<Text>().text = PlayerPrefs.GetInt("SkillPoint", 0).ToString();
        GameObject.Find("Fertilizer").transform.Find("Text").GetComponent<Text>().text = PlayerPrefs.GetInt("FertilizerAmount", 0).ToString() + " †KwR";

        //Set Time based on condition of obj.

    
        //Intro Dialog
        if (PlayerPrefs.GetInt("DialogIntroIndex", 0) < 3) //check if dialog intro has done or incomplete
        {
            PlayerPrefs.SetInt("isCurrDialogCont", 1);           
            StartCoroutine(WaitForNextDialogAppear(1.5f, dialogList.GetDialogIntro(0))); // wait a while after loading scene
        } 
    }



    IEnumerator PlaySoundDelayed(AudioClip clip, float time)
    {
        yield return new WaitForSeconds(time);

        audioSourceSound.PlayOneShot(clip);

    }



    //Left Bottom Button (Objective) Start
    public void ShowObjectiveWindow()
    {
        //check if any GUI is open
        if (!isGUIOpen)
        {
            StartCoroutine(PlaySoundDelayed(clickSound, 0.3f));
           
            objectiveWindow.GetComponent<Animator>().SetBool("ShowWindow", true);
            GameObject.Find("ObjectiveButton").GetComponent<Image>().enabled = false;
            GameObject.Find("ObjectiveButton").GetComponent<Button>().enabled = false;
            GameObject.Find("ObjectiveButton").transform.Find("Text").GetComponent<Text>().enabled = false;
            isGUIOpen = true;
        }
    }

    public void CloseObjectiveWindow()
    {
        StartCoroutine(PlaySoundDelayed(clickSound, 0.1f));

        objectiveWindow.GetComponent<Animator>().SetBool("ShowWindow", false);
        StartCoroutine("WaitForFinishAnimOfLeftBottomObjectiveWindow");

        //if dialogIntroIndex is 4 then intro dialog ends and start cultivation dialog
        if (PlayerPrefs.GetInt("DialogIntroIndex") == 4 && PlayerPrefs.GetInt("DialogCultivationIndex", 0) == 0)
        {
            StartCoroutine(WaitForNextDialogAppear(1.5f, dialogList.GetDialogForCultivation(0) ));
        }
    }


    IEnumerator WaitForFinishAnimOfLeftBottomObjectiveWindow()
    {

        yield return new WaitForSeconds(0.7f);
        GameObject.Find("ObjectiveButton").GetComponent<Image>().enabled = true;
        GameObject.Find("ObjectiveButton").GetComponent<Button>().enabled = true;
        GameObject.Find("ObjectiveButton").transform.Find("Text").GetComponent<Text>().enabled = true;
        isGUIOpen = false;
    }
    //Left Bottom Button End


    //Storage Icon

    public void ShowMarketWindow()
    {
        //check if any GUI is open
        if (!isGUIOpen)
        {
            StartCoroutine(PlaySoundDelayed(clickSound, 0.1f));

            GameObject.Find("Sell").GetComponent<Animator>().SetBool("ShouldAppear", true);
            isGUIOpen = true;
        }
    }

    public void CloseMarketWindow()
    {
        StartCoroutine(PlaySoundDelayed(clickSound, 0.1f));

        GameObject.Find("Sell").GetComponent<Animator>().SetBool("ShouldAppear", false);
        isGUIOpen = false;
    }



    //Fertilizer Warning

    public void ShowFertilizerWarningWindow()
    {
        StartCoroutine(PlaySoundDelayed(wrongSound, 0.5f));
        //check if any GUI is open
        if (!isGUIOpen)
        {
            if (GameObject.Find("FertilizerWarning").GetComponent<Animator>().GetBool("ShouldAppear") == false) //Avoid double click
            {
                GameObject.Find("FertilizerWarning").GetComponent<Animator>().SetBool("ShouldAppear", true);
            }
            isGUIOpen = true;
        }
    }

    public void CloseFertilizerWarningWindow()
    {
        StartCoroutine(PlaySoundDelayed(clickSound, 0.1f));

        GameObject.Find("FertilizerWarning").GetComponent<Animator>().SetBool("ShouldAppear", false);
        isGUIOpen = false;
    }

   




    //*************************************Show Success And Next Objective*******************************//

    IEnumerator ShowSuccess()
    {
        yield return new WaitForSeconds(3f);
        SuccessAnimator.SetBool("ShouldSuccessAppear", true);

        StartCoroutine(PlaySoundDelayed(levelCompleteSound, 0.4f));

        isGUIOpen = true;
    }


    public void PressOKtoNextObj()
    {
        StartCoroutine(PlaySoundDelayed(clickSound, 0.1f));

        SuccessAnimator.SetBool("ShouldSuccessAppear", false);

        if (PlayerPrefs.GetInt("CurrentObjective") == objectiveList.TotalNumberOfObjective)
        {
            NextObjAnimator.transform.Find("Text").GetComponent<Text>().text = "†Zvgvi me¸‡jv j¶¨ AwR©Z n‡q‡Q | \n bxo Gi mv‡_ _vKvi Rb¨ ab¨ev` |";
        }
        else {

                NextObjAnimator.transform.Find("Text").GetComponent<Text>().text = "<size=15>** cieZ©x j¶¨ **</size>\n" + objectiveList.GetCurrentBanglaObjective() + "\n";   

        }

        StartCoroutine("ShowNextObj");
    }


    IEnumerator ShowNextObj()
    {
        yield return new WaitForSeconds(3f);

        StartCoroutine(PlaySoundDelayed(rightSound, 0.4f));

        NextObjAnimator.SetBool("ShouldObjAppear", true);
        isGUIOpen = true;
    }


    public void PressOKtoStartPlay()
    {
        StartCoroutine(PlaySoundDelayed(clickSound, 0.1f));

        NextObjAnimator.SetBool("ShouldObjAppear", false);
        isGUIOpen = false;


        //check if previous 2 dialog(intro,cultivation) has completed and set current dialog value 1 so that i can go for next dialog(sell) after click thik ase button 
        if (PlayerPrefs.GetInt("DialogIntroIndex") == 4 && PlayerPrefs.GetInt("DialogCultivationIndex") == 3 && PlayerPrefs.GetInt("DialogSellIndex",0) == 0)
        {
            PlayerPrefs.SetInt("isCurrDialogCont", 1); //Since sell dialog is continuous dialog so set it to 1
            StartCoroutine(WaitForNextDialogAppear(1.5f, dialogList.GetDialogForSell(0)));
        }

        //check if previous 3 dialog(intro,cultivation,sell) has completed and set current dialog value 1 so that i can go for next dialog(parentsIntro) after click thik ase button
        else if (PlayerPrefs.GetInt("DialogIntroIndex") == 4 && PlayerPrefs.GetInt("DialogCultivationIndex") == 3 && PlayerPrefs.GetInt("DialogSellIndex") == 2 && PlayerPrefs.GetInt("DialogIntroParentsIndex",0) == 0)
        {
            PlayerPrefs.SetInt("isCurrDialogCont", 0); //As I run a single dialog "ghore jete ma o shishur icontite tap koro" so that it's not continuous dialog

            StartCoroutine(WaitForNextDialogAppear(1.5f, "N‡i †h‡Z gv I wkïi AvBKbwU‡Z U¨vc Ki |"));
        }

    }





    //***********************************************************Sell Crops start************************************************//

    public void SellCrops(string clickedItem)
    {
        StartCoroutine(PlaySoundDelayed(clickSound, 0.1f));

        if (objectiveList.GetCurrentObjectiveType().Equals("sell") && objectiveList.GetCurrentObjectiveItem().Equals(clickedItem))
        {
            int cropsAmount = PlayerPrefs.GetInt(objectiveList.GetCurrentObjectiveItem() + "Amount", 0) - objectiveList.GetSellAmountOfCrops();
            if (cropsAmount < 0)
            { cropsAmount = 0; }
            //Set Crops amount after sold
            PlayerPrefs.SetInt(objectiveList.GetCurrentObjectiveItem() + "Amount", cropsAmount);
            GameObject.Find("Sell").transform.Find(objectiveList.GetCurrentObjectiveItem()).transform.Find("Text").GetComponent<Text>().text = cropsAmount.ToString();
            //Set Earned Taka
            int previousAmount = PlayerPrefs.GetInt("TakaAmount", 1000);
            int increasedAmount = objectiveList.GetSellAmountOfCrops() * objectiveList.GetCropsPrice(objectiveList.GetCurrentObjectiveItem());
            int currentAmount = previousAmount + increasedAmount;
            PlayerPrefs.SetInt("TakaAmount", currentAmount);

            //Coin Animation , Close Storage & Market Window animation & show success window      
            StartCoroutine(CoinAnimation(GameObject.Find("Sell").transform.Find(objectiveList.GetCurrentObjectiveItem()).transform.Find("Coin").gameObject, clickedItem, previousAmount, increasedAmount / 6, currentAmount));

            //Set Next Objective
            objectiveList.SetNextObjective();
        }
    }


    IEnumerator CoinAnimation(GameObject Coin, string clickedItem, int previousAmount, int increasingAmount, int currentAmount)
    {
        Coin.GetComponent<Image>().enabled = true;
        if (Coin.GetComponent<Animator>().enabled)
        {
            Coin.GetComponent<Animator>().Play(clickedItem + "Coin", -1, 0f);
        }
        else {
            Coin.GetComponent<Animator>().enabled = true;
        }

        previousAmount += currentAmount / 6;

        yield return new WaitForSeconds(0.2f);
        Coin.GetComponent<Animator>().Play(clickedItem + "Coin", -1, 0f);
        previousAmount += increasingAmount;
        GameObject.Find("Taka").transform.Find("Text").GetComponent<Text>().text = previousAmount.ToString() + "$";
        StartCoroutine(PlaySoundDelayed(coinSound, 0.0f));

        yield return new WaitForSeconds(0.2f);
        Coin.GetComponent<Animator>().Play(clickedItem + "Coin", -1, 0f);
        previousAmount += increasingAmount;
        GameObject.Find("Taka").transform.Find("Text").GetComponent<Text>().text = previousAmount.ToString() + "$";
        StartCoroutine(PlaySoundDelayed(coinSound, 0.0f));

        yield return new WaitForSeconds(0.2f);
        Coin.GetComponent<Animator>().Play(clickedItem + "Coin", -1, 0f);
        previousAmount += increasingAmount;
        GameObject.Find("Taka").transform.Find("Text").GetComponent<Text>().text = previousAmount.ToString() + "$";
        StartCoroutine(PlaySoundDelayed(coinSound, 0.0f));

        yield return new WaitForSeconds(0.2f);
        Coin.GetComponent<Animator>().Play(clickedItem + "Coin", -1, 0f);
        previousAmount += increasingAmount;
        GameObject.Find("Taka").transform.Find("Text").GetComponent<Text>().text = previousAmount.ToString() + "$";
        StartCoroutine(PlaySoundDelayed(coinSound, 0.0f));

        yield return new WaitForSeconds(0.2f);
        Coin.GetComponent<Animator>().Play(clickedItem + "Coin", -1, 0f);
        previousAmount += increasingAmount;
        GameObject.Find("Taka").transform.Find("Text").GetComponent<Text>().text = previousAmount.ToString() + "$";
        StartCoroutine(PlaySoundDelayed(coinSound, 0.0f));

        yield return new WaitForSeconds(0.2f);
        GameObject.Find("Taka").transform.Find("Text").GetComponent<Text>().text = currentAmount.ToString() + "$";
        StartCoroutine(PlaySoundDelayed(coinSound, 0.0f));

        Coin.GetComponent<Image>().enabled = false;
        CloseMarketWindow();
        //Show Success window
        StartCoroutine("ShowSuccess");
    }

    //------------------------------------------------------------------Sell Crops End-------------------------------------------//









    //**********************************************Dialog Animation*************************************//

    public void ShowDialogWindow()
    {
        GameObject.Find("DialogBackground").GetComponent<Image>().enabled = true;

        if (objectiveList.GetCurrentObjectiveType() == "FoodCare" || objectiveList.GetCurrentObjectiveType() == "SafetyCare")
        {
            GameObject.Find("DialogDoctor").GetComponent<Animator>().SetBool("ShouldAppear", true);
        }
        else
        { //otherwise it a farm objective's dialog
            GameObject.Find("Dialog").GetComponent<Animator>().SetBool("ShouldAppear", true);
        }

        
        isGUIOpen = true;
    }

    public void CloseDialogWindow()
    {
        StartCoroutine(PlaySoundDelayed(clickSound, 0.0f));

        GameObject.Find("DialogBackground").GetComponent<Image>().enabled = false;


        if (objectiveList.GetCurrentObjectiveType() == "FoodCare" || objectiveList.GetCurrentObjectiveType() == "SafetyCare")
        {
            GameObject.Find("DialogDoctor").GetComponent<Animator>().SetBool("ShouldAppear", false);
        }
        else
        { //otherwise it a farm objective's dialog
            GameObject.Find("Dialog").GetComponent<Animator>().SetBool("ShouldAppear", false);
        }

        
        isGUIOpen = false;

        //Check if dialog type continous or not
        if (PlayerPrefs.GetInt("isCurrDialogCont") == 1)
        {
            PrepareAndShowContinuousDialogAnim();
        }

    }


    public void SetDialogText(string dialog)
    {
        if (objectiveList.GetCurrentObjectiveType() == "FoodCare" || objectiveList.GetCurrentObjectiveType() == "SafetyCare")
        {
            GameObject.Find("DialogDoctor").transform.Find("DialogBox").transform.Find("Text").GetComponent<Text>().text = dialog;
        }
        else { //otherwise it a farm objective
            GameObject.Find("Dialog").transform.Find("DialogBox").transform.Find("Text").GetComponent<Text>().text = dialog;
        }
    }




    public IEnumerator WaitForNextDialogAppear(float time, string dialog)
    {
        yield return new WaitForSeconds(time);
        SetDialogText(dialog);
        ShowDialogWindow();
    }





    void PrepareAndShowContinuousDialogAnim()
    {
        //continuous dialog serially arranged
        if (PlayerPrefs.GetInt("DialogIntroIndex") <= 3)//limit for 4 dialog
        {
            int dialogIndex = PlayerPrefs.GetInt("DialogIntroIndex");
            dialogIndex++;
            PlayerPrefs.SetInt("DialogIntroIndex", dialogIndex);

            //set current dialog 0(not continuous) sothat next continuous sell dialog will not appear automatically
            if (dialogIndex == 4)
            {
                PlayerPrefs.SetInt("isCurrDialogCont", 0);
            }            

             if(dialogIndex <= 3)
               StartCoroutine(WaitForNextDialogAppear(1.5f, dialogList.GetDialogIntro(dialogIndex))); //get next intro dialog and call for animation

           
            if (dialogIndex == 4)
                ShowObjectiveWindow();

        }
        else if (PlayerPrefs.GetInt("DialogSellIndex") <= 1) //limit for 2 dialog
        {
            int dialogIndex = PlayerPrefs.GetInt("DialogSellIndex");
            dialogIndex++;
            PlayerPrefs.SetInt("DialogSellIndex", dialogIndex);

            //set current dialog 0(not continuous) sothat next continuous (not set yet) dialog will not appear automatically
            if (dialogIndex == 2)
            {
                PlayerPrefs.SetInt("isCurrDialogCont", 0);
            }

            if (dialogIndex <= 1)
              StartCoroutine(WaitForNextDialogAppear(1.5f, dialogList.GetDialogForSell(dialogIndex))); //get next sell dialog and call for animation
        }
        else if (PlayerPrefs.GetInt("DialogIntroParentsIndex") <= 3) //limit for 4 dialog
        {
            int dialogIndex = PlayerPrefs.GetInt("DialogIntroParentsIndex");
            dialogIndex++;
            PlayerPrefs.SetInt("DialogIntroParentsIndex", dialogIndex);

            //set current dialog 0(not continuous) sothat next continuous (not set yet) dialog will not appear automatically
            if (dialogIndex == 4)
            {
                PlayerPrefs.SetInt("isCurrDialogCont", 0);
            }

            if (dialogIndex <= 3)
                StartCoroutine(WaitForNextDialogAppear(1.5f, dialogList.GetDialogForParentalCareIntro(dialogIndex))); //get next parentIntro dialog and call for animation

            //Show objective window after continus dialog finished
            if (dialogIndex == 4) 
                ShowObjectiveWindow();

        }
        else if (PlayerPrefs.GetInt("DialogParentsSafetyIndex") <= 2) //limit for 3 dialog
        {
            int dialogIndex = PlayerPrefs.GetInt("DialogParentsSafetyIndex");
            dialogIndex++;
            PlayerPrefs.SetInt("DialogParentsSafetyIndex", dialogIndex);

            //set current dialog 0(not continuous) sothat next continuous (not set yet) dialog will not appear automatically
            if (dialogIndex == 3)
            {
                PlayerPrefs.SetInt("isCurrDialogCont", 0);
            }

            if (dialogIndex <= 2)
                StartCoroutine(WaitForNextDialogAppear(1.5f, dialogList.GetDialogForParentalSafety(dialogIndex))); //get next parentIntro dialog and call for animation

            //Show objective window after continus dialog finished
            if (dialogIndex == 3)
                ShowObjectiveWindow();

        }

    }

    //----------------------------------------Dialog Anim End-------------------------------------//









    
    public void GoToFarm()
    {
        StartCoroutine(PlaySoundDelayed(clickSound, 0.1f));

        if (PlayerPrefs.GetInt("CurrentObjective") < objectiveList.TotalNumberOfObjective) //check if objectives reached in last index
        {
            //change text of objective window for farm
            GameObject.Find("ObjectiveWindow").transform.Find("ObjectiveText").GetComponent<Text>().text = objectiveList.GetCurrentBanglaObjective();
        }


        GetComponent<GameManager>().shouldFarmsCameraOpen = true;
        GetComponent<ParentsGameManager>().shouldParentalCameraOpen = false;

        cameraForParentalCare.SetActive(false);
        goToFarmButton.SetActive(false);
        goToParentsLivingAndFoodButton.SetActive(true);
        foodInstructionButton.SetActive(false);

        GameObject.Find("ObjectiveButton").GetComponent<Image>().sprite = SpriteOfObjButtonForFarm;

        foreach (GameObject obj in disableGameobjectForParentalCare)
        {
            obj.SetActive(true);
        }

        //hide kitchen icon
        goToParentsKitchenButton.SetActive(false);

    }




    public void GoToParentalLivingAndFoodCare()
    {
        StartCoroutine(PlaySoundDelayed(clickSound, 0.1f));

        if (PlayerPrefs.GetInt("CurrentObjective") < objectiveList.TotalNumberOfObjective) //check if objectives reached in last index
        {
            //change text of objective window for parents
            if (objectiveList.GetCurrentObjectiveType() == "FoodCare" || objectiveList.GetCurrentObjectiveType() == "SafetyCare")
            {
                GameObject.Find("ObjectiveWindow").transform.Find("ObjectiveText").GetComponent<Text>().text = objectiveList.GetCurrentObjectiveParentalCare();
            }
            else
            {
                GameObject.Find("ObjectiveWindow").transform.Find("ObjectiveText").GetComponent<Text>().text = "AvcvZZ GLv‡b †Kvb j¶¨ †bB |";
            }
        }

        //reset towel,cleaner position
        Towel.transform.localPosition = new Vector3(-4.55f, 4.59f, 0f);
        SmallCleaner.transform.localPosition = new Vector3(2.9f, -2.25f, 0f);
        BigCleaner.transform.localPosition = new Vector3(6.11f, 2.02f, 0f);


        GetComponent<GameManager>().shouldFarmsCameraOpen = false;
        GetComponent<ParentsGameManager>().shouldParentalCameraOpen = true;

        cameraForParentalCare.SetActive(true);
        goToFarmButton.SetActive(true);
        goToParentsLivingAndFoodButton.SetActive(false);
        foodInstructionButton.SetActive(true);

        GameObject.Find("ObjectiveButton").GetComponent<Image>().sprite = SpriteOfObjButtonForParents;

        foreach (GameObject obj in disableGameobjectForParentalCare)
        {
            obj.SetActive(false);
        }


        //Show 1st parentsIntro dialog
        if (PlayerPrefs.GetInt("DialogIntroIndex") == 4 && PlayerPrefs.GetInt("DialogCultivationIndex") == 3 && PlayerPrefs.GetInt("DialogSellIndex") == 2 && PlayerPrefs.GetInt("DialogIntroParentsIndex", 0) == 0)
        {
            PlayerPrefs.SetInt("isCurrDialogCont", 1); //Since ParentsIntro dialog is continuous so set it to 1

            StartCoroutine(WaitForNextDialogAppear(0.5f, dialogList.GetDialogForParentalCareIntro(0) ));
        }


        //visible kitchen icon
        goToParentsKitchenButton.SetActive(true);

        //livingandfood environment enable
        LivingAndFood.SetActive(true);

        //kitchen environment disable
        Kitchen.SetActive(false);

    }



    public void GoToParentalKitchen()
    {
        StartCoroutine(PlaySoundDelayed(clickSound, 0.1f));

        if (PlayerPrefs.GetInt("CurrentObjective") < objectiveList.TotalNumberOfObjective) //check if objectives reached in last index
        {
            //change text of objective window for parents
            if (objectiveList.GetCurrentObjectiveType() == "FoodCare" || objectiveList.GetCurrentObjectiveType() == "SafetyCare")
            {
                GameObject.Find("ObjectiveWindow").transform.Find("ObjectiveText").GetComponent<Text>().text = objectiveList.GetCurrentObjectiveParentalCare();
            }
            else
            {
                GameObject.Find("ObjectiveWindow").transform.Find("ObjectiveText").GetComponent<Text>().text = "AvcvZZ GLv‡b †Kvb j¶¨ †bB |";
            }
        }

        GetComponent<GameManager>().shouldFarmsCameraOpen = false;
        GetComponent<ParentsGameManager>().shouldParentalCameraOpen = true;


        goToFarmButton.SetActive(false);
        goToParentsLivingAndFoodButton.SetActive(true);
        goToParentsKitchenButton.SetActive(false);
        foodInstructionButton.SetActive(false);

        //livingandfood environment disable
        LivingAndFood.SetActive(false);

        //kitchen environment enable
        Kitchen.SetActive(true);


        //Show 1st parentsSafety dialog
        if (PlayerPrefs.GetInt("DialogIntroIndex") == 4 && PlayerPrefs.GetInt("DialogCultivationIndex") == 3 && PlayerPrefs.GetInt("DialogSellIndex") == 2 && PlayerPrefs.GetInt("DialogIntroParentsIndex", 0) == 4 && PlayerPrefs.GetInt("DialogParentsSafetyIndex",0) == 0 && objectiveList.GetCurrentObjectiveType() == "SafetyCare") //for avoiding safety dialog appear before 1st foocare complete
        {
            PlayerPrefs.SetInt("isCurrDialogCont", 1); //Since ParentsSafety dialog is continuous so set it to 1

            StartCoroutine( WaitForNextDialogAppear(0.5f, dialogList.GetDialogForParentalSafety(0)) );
        }

    }


    public void ShowFoodInstrution()
    {
        StartCoroutine(PlaySoundDelayed(clickSound, 0.1f));

        GameObject.Find("DialogBackground").GetComponent<Image>().enabled = true;
        GameObject.Find("FoodInstructions").GetComponent<Animator>().SetBool("ShouldAppear", true);
        isGUIOpen = true;

    }


    public void CloseFoodInstruction()
    {
        StartCoroutine(PlaySoundDelayed(clickSound, 0.1f));

        GameObject.Find("DialogBackground").GetComponent<Image>().enabled = false;
        GameObject.Find("FoodInstructions").GetComponent<Animator>().SetBool("ShouldAppear", false);
        isGUIOpen = false;

    }


}
