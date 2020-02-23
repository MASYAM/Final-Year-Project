using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.GameCenter;

public class LeaderboardManagerIos : MonoBehaviour {

		#region GAME_CENTER    
		public static void  AuthenticateToGameCenter()
		{
		    #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
			Social.localUser.Authenticate(success =>
			{
			if (success)
			{
			Debug.Log("Authentication successful");
			}
			else
			{
			Debug.Log("Authentication failed");
			}
			});
			#endif
		}

		public static void ReportScore(long score, string leaderboardID)
		{
		    #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
			//Debug.Log("Reporting score " + score + " on leaderboard " + leaderboardID);
			Social.ReportScore(score, leaderboardID, success =>
			{
			if (success)
			{
			Debug.Log("Reported score successfully");
			}
			else
			{
			Debug.Log("Failed to report score");
			}

			Debug.Log(success ? "Reported score successfully" : "Failed to report score"); Debug.Log("New Score:"+score);  
			});
			#endif
		}

		public static void ShowLeaderboard()
		{
		    #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
			Social.ShowLeaderboardUI();
			#endif
		}
		#endregion

}
