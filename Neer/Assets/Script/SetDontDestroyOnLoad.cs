using UnityEngine;
using System.Collections;

public class SetDontDestroyOnLoad : MonoBehaviour {



	private static SetDontDestroyOnLoad playerInstance;
	void Awake(){
		DontDestroyOnLoad (this);

		if (playerInstance == null) {
			playerInstance = this;
		} else {
			DestroyObject(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
