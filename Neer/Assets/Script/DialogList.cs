using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogList : MonoBehaviour {

    //neer game e tomake sagotom \n \n neer ekti home simulation social network game \n cholo ebar neer somporke ektu jani
    //ei game e prothome amra shikhbo kivabe khaddo shossho chas korte hoy ebong taka uparjon korte hoy
    //erpor amra shikhbo kivabe ekjon gorvoboti mayer jotno nite hoy ebong gorvopat rodh kora jae
    //ebong shikhbo ar o onek kisu. \n tahole cholo shuru kora jak

    string[] dialogIntro = {

            "!!! bxo †MBg G †Zvgv‡K ¯^vMZg !!! \n \n bxo GKwU †nvg wmgy‡jkb †mvk¨vj †bUIqvK© †MBg | \n Pj Gevi bxo m¤c‡K© GKUz Rvwb |",
            "GB †MBg G cÖ_‡g Avgiv wkLe wKfv‡e Lv`¨-km¨ Pvl Ki‡Z nq Ges UvKv DcvR©b Ki‡Z nq |",
            "Gici Avgiv wkLe wKfv‡e GKRb Mf©eZx gv‡qi hZœ wb‡Z nq Ges Mf©cvZ †iva Kiv hvq |",
            "Ges wkLe Avi I A‡bK wKQz | \n Zvn‡j Pj Gevi ïi“ Kiv hvK |"
    };

    
    //prothome j khaddo sossho chas korte bola hoyese sei jomite tap koro
    //jomir pashe j panir patroti dekha jachse setate tap koro
    //chaser jonno projoniyo somoy ghorite dekhte pabe
    //chaser somoy ses hoyese. jomite abar tap kore sossho gulo gudam gore songroho koro

    string[] dialogForCultivation = {

            "cÖ_‡g †h Lv`¨-km¨ Pvl Ki‡Z ejv n‡q‡Q †mB Rwg‡Z U¨vc Ki |",
            "Rwgi cv‡k †h cvwbi cvÎwU †`Lv hv‡”Q †mUv‡Z U¨vc Ki |",
            "Pv‡li Rb¨ cÖ‡qvRbxq mgq Nwo‡Z †`L‡Z cv‡e |",
            "Pv‡li mgq †kl n‡q‡Q | Rwg‡Z Avevi U¨vc K‡i km¨¸‡jv ¸`vg N‡i msMÖn Ki |"
    };


    //Tmr songrihito khaddo sossho bikri kore taka uparjon korte paro.
    //ghorer icontite tap kore khaddo sosso bikri koro


    string[] dialogForSell = {

            "†Zvgvi msM„nxZ Lv`¨km¨ wewµ K‡i UvKv DcvR©b Ki‡Z cvi |",
            "N‡ii AvBKbwU‡Z U¨vc K‡i Lv`¨km¨ wewµ Ki |"
    };


    //gorvabostae ekjon mayer jotno neya khubi gruttopuro
    //esomoy niyomito sushomo khaddo grohon kora uchit
    //eki sathe j sob karone gorvopat ghotte pare se bosoy gulo kheyal rakhte hobe
    //ekhn khabarer nirdeshonabolir icontite tap koro ebong jotnosohokare poro


    string[] dialogForParentalCareIntro = {

            "Mf©ve¯—vq GKRb gv‡qi hZœ †bqv LyeB ¸i“Z¡c~Y© |",
            "Gmgq wbqwgZ mylg Lv`¨ MÖnY Kiv DwPZ",
            "GKB mv‡_ †h me Kvi‡b Mf©cvZ NU‡Z cv‡i †m welq¸‡jv †Lqvj ivL‡Z n‡e |",
            "GLb Lvev‡ii wb‡`©kbvejxi AvBKbwU‡Z U¨vc Ki Ges hZœmnKv‡i co |"
    };


    //besir vag khtre durghotona jonito karone gorvopat ghote thake
    //Tai durghotona erate sotorko thakte hobe. jemon mejhe pichchil na rakha, vari bostu na uthono ittadi.
    //ekhane kisu soronjam deya ase. segulo babohar kore lokkho arjon koro
   

    string[] dialogForParentalSafety = {

            "†ewki fvM †¶‡Î `yN©Ubv RwbZ Kvi‡Y Mf©cvZ N‡U _v‡K |",
            "ZvB `yN©Ubv Gov‡Z mZK© _vK‡Z n‡e | †hgb †g‡S wcw”Qj bv ivLv, fvix e¯‘ bv DVv‡bv BZ¨vw` |",
            "GLv‡b wKQz miÄvg †`qv Av‡Q | †m¸‡jv e¨envi K‡i j¶¨ AR©b Ki |"
    };




    public string GetDialogIntro(int index)
    {
        return dialogIntro[index];
    }


    public string GetDialogForCultivation(int index)
    {
        return dialogForCultivation[index];
    }


    public string GetDialogForSell(int index)
    {
        return dialogForSell[index];
    }


    public string GetDialogForParentalCareIntro(int index)
    {
        return dialogForParentalCareIntro[index];
    }


    public string GetDialogForParentalSafety(int index)
    {
        return dialogForParentalSafety[index];
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
