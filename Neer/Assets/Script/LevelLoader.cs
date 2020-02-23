using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelLoader : MonoBehaviour {


    public GameObject loadingBar;
    public Slider slider;
    public Text progressText;
    AsyncOperation operation;

    // Use this for initialization
    void Start () {
        StartCoroutine("LoadAsynchronously");
	}


    IEnumerator LoadAsynchronously()
    {
        StartCoroutine(WaitForLoadAnim());

        operation = SceneManager.LoadSceneAsync("Main");
        operation.allowSceneActivation = false;
        loadingBar.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
           // slider.value = progress;
           // progressText.text = progress * 100f + "%";
            yield return null;
        }

    }



    IEnumerator WaitForLoadAnim()
    {
        float value = UnityEngine.Random.Range(0.0f, 0.0f);
        slider.value = value;
        progressText.text = Mathf.Round(value * 100f) + "%";

        yield return new WaitForSeconds(1f);

        value = UnityEngine.Random.Range(0.1f, 0.3f);
        slider.value = value;
        progressText.text = Mathf.Round(value * 100f) + "%";

        yield return new WaitForSeconds(0.5f);

        value = UnityEngine.Random.Range(0.4f, 0.6f);
        slider.value = value;
        progressText.text = Mathf.Round(value * 100f) + "%";

        yield return new WaitForSeconds(1.7f);

        value = UnityEngine.Random.Range(0.7f, 0.9f);
        slider.value = value;
        progressText.text = Mathf.Round(value * 100f) + "%";

        yield return new WaitForSeconds(1f);

        slider.value = 1.0f;
        progressText.text = "100%";

        yield return new WaitForSeconds(.4f);
         operation.allowSceneActivation = true;

    }


	
	// Update is called once per frame
	void Update () {
		
	}
}
