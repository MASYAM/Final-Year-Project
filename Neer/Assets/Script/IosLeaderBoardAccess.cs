using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class IosLeaderBoardAccess : MonoBehaviour {

	#region PRIVATE_VARIABLES

	string leaderBoardID = "com.pixitalegames.chickenscream1";
	string leaderBoardID2 = "";
	string leaderBoardID3 = "";
	string leaderBoardID4 = "";

	#endregion

	void Start()
	{
			#if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
		     LeaderboardManagerIos.AuthenticateToGameCenter();
			#endif

	}

	#region BUTTON_EVENT_HANDLER

	/// <summary>
	/// Raises the login event.
	/// </summary>
	/// <param name="id">Identifier.</param>
	public void OnLogin(string id){
		LeaderboardManagerIos.AuthenticateToGameCenter();
	}

	/// <summary>
	/// Raises the show leaderboard event.
	/// </summary>
	public void OnShowLeaderboard(){
		if (Social.localUser.authenticated) {
			LeaderboardManagerIos.ShowLeaderboard ();
		} else {
			LeaderboardManagerIos.AuthenticateToGameCenter();
		}
	}

	/// <summary>
	/// Raises the post score event.
	/// </summary>
	public void OnPostScore(long score,int choice){
		if (Social.localUser.authenticated) {
			
			if (choice == 1) {
				LeaderboardManagerIos.ReportScore (score, leaderBoardID);
			} else if(choice == 2) {
				LeaderboardManagerIos.ReportScore (score, leaderBoardID2);
			}else if(choice == 3) {
				LeaderboardManagerIos.ReportScore (score, leaderBoardID3);
			}else if(choice == 4) {
				LeaderboardManagerIos.ReportScore (score, leaderBoardID4);
			}
		}
	}

	#endregion
}
