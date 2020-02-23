using UnityEngine;
using System.Collections;

using UnityEngine.SocialPlatforms;

public class DisplayLeaderboard : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public void OnShowLeaderboard(){

		#if (UNITY_5 && UNITY_IOS) || UNITY_IOS
			if (Social.localUser.authenticated) {
				LeaderboardManagerIos.ShowLeaderboard ();
			} else {
				LeaderboardManagerIos.AuthenticateToGameCenter();
			}
		#elif UNITY_ANDROID

			if (Social.localUser.authenticated) {
				Social.ShowLeaderboardUI (); // Show all leaderboard
			}else{
				//GameObject.Find("Leaderboard").GetComponent<GPlayServices>().LogIn ();
			}
		#endif
		 

	}
	
	// Update is called once per frame
	void Update () {
	   
	}

}
