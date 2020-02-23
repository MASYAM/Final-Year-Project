using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ObjectiveList : MonoBehaviour {


    //List<string> objectives = new List<string>();

    //misty kumra chas koro \n dokkhota orjon: 10 point \n khoroch: 300 taka \n bikroy: 500 taka \n luv: 200 taka //dialog task
    //misty kumra bikri koro //dialog task
    //khabar ghor poridorshon koro //dialog task
    //ranna ghor poridorshon koro //dialog task
    //khabar ghor poridorshon koro
    //tormuz chas koro \n dokkhota orjon: 10 point \n khoroch: 300 taka \n bikroy: 500 taka \n luv: 200 taka
    //tormuz bikri koro
    //vutta chas koro \n dokkhota orjon: 10 point \n khoroch: 100 taka \n bikroy: 400 taka \n luv: 300 taka
    //badhakopi chas koro \n dokkhota orjon: 10 point \n khoroch: 90 taka \n bikroy: 200 taka \n luv: 110 taka
    //vutta bikri koro
    //tormuz bikri koro
    //gazor chas koro \n dokkhota orjon: 10 point \n khoroch: 20 taka \n bikroy: 100 taka \n luv: 80 taka
    //gazor bikri koro


    string[] ObjectiveBangla = {
                          "wgwó Kzgov Pvl Ki | \n`¶Zv AR©b: 10 c‡q›U \nLiP: 300 UvKv \nweµq: 500 UvKv \njvf: 200 UvKv",
                          "wgwó Kzgov wewµ Ki |",
                          "Lvevi Ni cwi`k©b Ki",
                          "ivbœvNi cwi`k©b Ki |",
                          "Lvevi Ni cwi`k©b Ki",
                          "ZigyR Pvl Ki | \n`¶Zv AR©b: 10 c‡q›U \nLiP: 300 UvKv \nweµq: 500 UvKv \njvf: 200 UvKv",
                          "ZigyR wewµ Ki |",
                          "fzÆv Pvl Ki | \n`¶Zv AR©b: 10 c‡q›U \nLiP: 100 UvKv \nweµq: 400 UvKv \njvf: 300 UvKv",
                          "evuavKwc Pvl Ki | \n`¶Zv AR©b: 10 c‡q›U \nLiP: 90 UvKv \nweµq: 200 UvKv \njvf: 110 UvKv", 
                          "fzÆv wewµ Ki |",
                          "ZigyR wewµ Ki |",
                          "MvRi Pvl Ki | \n`¶Zv AR©b: 10 c‡q›U \nLiP: 20 UvKv \nweµq: 100 UvKv \njvf: 80 UvKv",
                          "MvRi wewµ Ki |"
    };

    public int TotalNumberOfObjective;

    //Sorted according to ObjectiveBangla
    string[] ObjectiveType = { "cultivate", "sell", "FoodCare", "SafetyCare","FoodCare", "cultivate", "sell", "cultivate", "cultivate", "sell", "sell", "cultivate", "sell" };

    //sorted according to ObjectiveBangla
    string[] ObjectiveItem = { "Pumpkin","Pumpkin", "Milk", "Towel", "Fish" ,"Watermelon","Watermelon","Corn","Cabbage","Corn","Watermelon","Carrot","Carrot"};

    //above all array must maintain same dimension


    // ""
    //table e kon khabarti ekjon govoboti mayer opusti rodh korbe ?
    //Ranna ghorer mejhe porishkar koro. //water
    //nicher kon khabarti amisher chahida puron korbe ?
    //gorvoboti make Kon kajti kokhonoi korte deya jabe na ? //kolos
    //eder moddhe kon khabar doihik briddhi o khoy purone sohayota kore ?
    //gorvoboti make pore jawa theke rokkha korte ki korbe ? //glass

    string[] ObjectiveBanglaParentalCare = {
            "", //Leave this element empty because index 0 never run. only ObjectiveBanglaParentalCare access from SetNextObjective()
            "†Uwe‡j †Kvb LveviwU GKRb Mf©eZx gv‡qi Acywó †iva Ki‡e ?",
            "ivbœvN‡ii †g‡S cwi®‹vi Ki |",
            "wb‡Pi †Kvb LveviwU Avwg‡li Pvwn`v c~iY Ki‡e ?",
            "Ges wkLe Avi I A‡bK wKQz | \n Zvn‡j Pj Gevi ïi“ Kiv hvK |"
    };


    public GameObject[] foodObjectivesList, safetyObjectivesList;



    //serially for pumpkin,watermelon,carrot,cabbage,corn
    int[] fertilizerAmount = { 3, 2, 3, 5, 7 };


    //Level wise sell amount of crops (amount of crops will sell by each tap)
    //level-1 -> 20, level-1 -> 30, level-3 -> 50
    int[] SellAmountOfCrops = { 20, 30, 50 };

    //crops price level wise 1,2,3
    int[] pumpkinPrice    = { 25, 30, 40 };
    int[] watermelonPrice = { 40, 60, 100 };
    int[] carrotPrice     = { 5, 15, 20 };
    int[] cabbagePrice    = { 10, 20, 40 };
    int[] cornPrice       = { 20, 30, 40 };


    //crops cultivation time in seconds level wise 1,2,3
    int[] pumpkinTime = { 20, 60, 120 };
    int[] watermelonTime = { 25, 110, 150 };
    int[] carrotTime = { 15, 50, 130 };
    int[] cabbageTime = { 25, 90, 140 };
    int[] cornTime = { 30, 120, 180 };


    //crops Expense level wise 1,2,3
    int[] pumpkinExpense    = { 300, 500, 800 };  //sell price will be 500, 900, 2000
    int[] watermelonExpense = { 200, 500, 1200 }; //sell price will be 800, 1800, 4000
    int[] carrotExpense     = { 20, 100, 300 };   //sell price will be 100, 450, 1000
    int[] cabbageExpense    = { 90, 150, 500 };   //sell price will be 200, 600, 2000
    int[] cornExpense       = { 100, 300, 700 };  //sell price will be 400, 900, 2000


    //Amount of crops will add to storage level wise 1,2,3
    int[] CropsAmountAddToStorage    = { 50, 100, 300 };  //remember above SellAmountOfCrops is 20,30,50 (Level-1,2,3) so here min value should 20,30,50


    // Use this for initialization
    void Start () {

        //PlayerPrefs.DeleteAll();


        TotalNumberOfObjective = ObjectiveBangla.Length;

        if (PlayerPrefs.GetInt("CurrentObjective") < TotalNumberOfObjective) //check if objectives reached in last index
        {
            GameObject.Find("GUI").transform.Find("ObjectiveWindow").transform.Find("ObjectiveText").GetComponent<Text>().text = ObjectiveBangla[PlayerPrefs.GetInt("CurrentObjective", 0)];
        }
        else {
            GameObject.Find("GUI").transform.Find("ObjectiveWindow").transform.Find("ObjectiveText").GetComponent<Text>().text = "†Zvgvi me¸‡jv j¶¨ AwR©Z n‡q‡Q | \n bxo Gi mv‡_ _vKvi Rb¨ ab¨ev` |";
        }
     
	}





    //This method should call once
    public void SetNextObjective()
    {
        PlayerPrefs.SetInt("CurrentObjective", PlayerPrefs.GetInt("CurrentObjective")+1);

        if (PlayerPrefs.GetInt("CurrentObjective") == TotalNumberOfObjective) //Reached last objective so show final congrates
        {
            GameObject.Find("GUI").transform.Find("ObjectiveWindow").transform.Find("ObjectiveText").GetComponent<Text>().text = "†Zvgvi me¸‡jv j¶¨ AwR©Z n‡q†Q | \n bxo Gi mv†_ _vKvi Rb¨ ab¨ev` |";
        }
        else if(PlayerPrefs.GetInt("CurrentObjective") < TotalNumberOfObjective)
        {
            GameObject.Find("GUI").transform.Find("ObjectiveWindow").transform.Find("ObjectiveText").GetComponent<Text>().text = ObjectiveBangla[PlayerPrefs.GetInt("CurrentObjective", 0)];

            //Set Next parental care objective if parental care type found
            if (GetCurrentObjectiveType() == "FoodCare" || GetCurrentObjectiveType() == "SafetyCare")
            {
                PlayerPrefs.SetInt("CurrentObjectiveParentalCare", PlayerPrefs.GetInt("CurrentObjectiveParentalCare") + 1);


                //visible parental care objecive
                if (GetCurrentObjectiveType() == "FoodCare")
                {
                        foreach (GameObject foodObj in foodObjectivesList)
                        {
                            if (foodObj.name == PlayerPrefs.GetInt("CurrentObjectiveParentalCare").ToString())
                                foodObj.SetActive(true);
                        }

                }
                else {

                        foreach (GameObject safetyObj in safetyObjectivesList)
                        {
                                  if (safetyObj.name == PlayerPrefs.GetInt("CurrentObjectiveParentalCare").ToString())
                                       safetyObj.SetActive(true);
                        }
                }
            }
        }
    }





    //This method shoud call once after some objective done like ObjectiveBangla(0-20) then level 2 start from ObjectiveBangla(21-40)
    public void SetNextLevel()
    {
        PlayerPrefs.SetInt("CurrentLevel", PlayerPrefs.GetInt("CurrentLevel") + 1);
    }


    public string GetCurrentBanglaObjective()
    {      
       return ObjectiveBangla[PlayerPrefs.GetInt("CurrentObjective", 0)];       
    }


    public string GetCurrentObjectiveType()
    {
            return ObjectiveType[PlayerPrefs.GetInt("CurrentObjective", 0)];       
    }


    public string GetCurrentObjectiveItem()
    {
        return ObjectiveItem[PlayerPrefs.GetInt("CurrentObjective", 0)];
    }


    public int GetSellAmountOfCrops()
    {
        return SellAmountOfCrops[PlayerPrefs.GetInt("CurrentLevel", 0)];
    }


    public int GetCropsPrice(string CropsItem)
    {
        switch (CropsItem)
        {
            case "Pumpkin":
                return pumpkinPrice[PlayerPrefs.GetInt("CurrentLevel", 0)];
            case "Watermelon":
                return watermelonPrice[PlayerPrefs.GetInt("CurrentLevel", 0)];
            case "Carrot":
                return carrotPrice[PlayerPrefs.GetInt("CurrentLevel", 0)];
            case "Cabbage":
                return cabbagePrice[PlayerPrefs.GetInt("CurrentLevel", 0)];
            case "Corn":
                return cornPrice[PlayerPrefs.GetInt("CurrentLevel", 0)];
            default:
                return pumpkinPrice[PlayerPrefs.GetInt("CurrentLevel", 0)];
        }
    }


    public int GetCropsCultivationTime(string CropsItem)
    {
        switch (CropsItem)
        {
            case "Pumpkin":
                return pumpkinTime[PlayerPrefs.GetInt("CurrentLevel", 0)];
            case "Watermelon":
                return watermelonTime[PlayerPrefs.GetInt("CurrentLevel", 0)];
            case "Carrot":
                return carrotTime[PlayerPrefs.GetInt("CurrentLevel", 0)];
            case "Cabbage":
                return cabbageTime[PlayerPrefs.GetInt("CurrentLevel", 0)];
            case "Corn":
                return cornTime[PlayerPrefs.GetInt("CurrentLevel", 0)];
            default:
                return pumpkinTime[PlayerPrefs.GetInt("CurrentLevel", 0)];
        }
    }


    public int GetFertilizerAmount(string CropsItem)
    {
        switch (CropsItem)
        {
            case "Pumpkin":
                return fertilizerAmount[0];
            case "Watermelon":
                return fertilizerAmount[1];
            case "Carrot":
                return fertilizerAmount[2];
            case "Cabbage":
                return fertilizerAmount[3];
            case "Corn":
                return fertilizerAmount[4];
            default:
                return fertilizerAmount[0];
        }
    }


    public int GetCropsExpense(string CropsItem)
    {
        switch (CropsItem)
        {
            case "Pumpkin":
                return pumpkinExpense[PlayerPrefs.GetInt("CurrentLevel", 0)];
            case "Watermelon":
                return watermelonExpense[PlayerPrefs.GetInt("CurrentLevel", 0)];
            case "Carrot":
                return carrotExpense[PlayerPrefs.GetInt("CurrentLevel", 0)];
            case "Cabbage":
                return cabbageExpense[PlayerPrefs.GetInt("CurrentLevel", 0)];
            case "Corn":
                return cornExpense[PlayerPrefs.GetInt("CurrentLevel", 0)];
            default:
                return pumpkinExpense[PlayerPrefs.GetInt("CurrentLevel", 0)];
        }
    }



    public int GetCropsAmountToStore()
    {
        return CropsAmountAddToStorage[PlayerPrefs.GetInt("CurrentLevel", 0)];
        
    }




    //Parental Care

    public string GetCurrentObjectiveParentalCare()
    {
        return ObjectiveBanglaParentalCare[PlayerPrefs.GetInt("CurrentObjectiveParentalCare", 0)];
    }




    // Update is called once per frame
    void Update () {
        
	}
}
