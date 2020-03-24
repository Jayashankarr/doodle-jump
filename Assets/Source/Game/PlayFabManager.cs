
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;


	public class PlayFabManager : MonoBehaviour
	{
        public string PlayFabId = "D5C65";

        public static PlayFabManager Instance = null;

        void Awake ()
        {
            Instance = this;

            Login (PlayFabId);
        }

        
        void Login (string titleId) {
			LoginWithCustomIDRequest request = new LoginWithCustomIDRequest () {
				TitleId = titleId,
				CreateAccount = true,
				CustomId = SystemInfo.deviceUniqueIdentifier
			};

			PlayFabClientAPI.LoginWithCustomID (request, (result) => {
				PlayFabId = result.PlayFabId;
				Debug.Log ("Got PlayFabID: " + PlayFabId);

				if (result.NewlyCreated) {
					Debug.Log ("(new account)");
				} else {
					Debug.Log ("(existing account)");
				}
			},
				(error) => {
					Debug.Log (error.ErrorMessage);
				});
		}

		public void UpdateName (string userName) {
			PlayFab.ClientModels.UpdateUserTitleDisplayNameRequest request = new PlayFab.ClientModels.UpdateUserTitleDisplayNameRequest () {
				DisplayName = userName
			};

			PlayFabClientAPI.UpdateUserTitleDisplayName (request, (result) => {
			},
				(error) => {
					Debug.Log (error.ErrorMessage);
				});

		}

		public bool IsYou (string playFabId) {
			if (playFabId == PlayFabId)
				return true;
			return false;
		}
		public void Logout () 
        {

		}

		public  void GetFriends () 
        {
			PlayFab.ClientModels.GetFriendsListRequest request = new PlayFab.ClientModels.GetFriendsListRequest ()
            {
				IncludeFacebookFriends = true
			};

			PlayFabClientAPI.GetFriendsList (request, (result) => {
				Dictionary<string,string> dic = new Dictionary<string, string> ();
				foreach (var item in result.Friends) {
					dic.Add (item.FacebookInfo.FacebookId, item.FriendPlayFabId);
				}
				//Callback (dic);
			}, (error) => {
				Debug.Log (error.ErrorDetails);
			});
		}

		public  Dictionary<string,int> GetLeaderboardData () 
        {
            Dictionary<string,int> dic = new Dictionary<string, int> ();
			PlayFab.ClientModels.GetLeaderboardRequest request = new PlayFab.ClientModels.GetLeaderboardRequest () 
            {
				StatisticName = "Score",
                StartPosition = 0
			};

			PlayFabClientAPI.GetLeaderboard (request, (result) => 
            {
				//Dictionary<string,int> dic = new Dictionary<string, int> ();
				foreach (var item in result.Leaderboard) {
					dic.Add (item.PlayFabId, item.StatValue);
				}
				//Callback (dic);
                //return dic;
			}, (error) => {
				Debug.Log (error.ErrorDetails);
			});

            return dic;
		}

		public  void SetScore (int value) {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Score"] = value.ToString();
			

			UpdateUserDataRequest request = new UpdateUserDataRequest () {
				Data = dic
			};

			PlayFabClientAPI.UpdateUserData (request, (result) => {
				Debug.Log ("Successfully updated user data");
			}, (error) => {
				Debug.Log (error.ErrorDetails);
			});

		}

        public  void SetHighScore (int value) {
			
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["HighScore"] = value.ToString();

			UpdateUserDataRequest request = new UpdateUserDataRequest () {
				Data = dic
			};

			PlayFabClientAPI.UpdateUserData (request, (result) => {
				Debug.Log ("Successfully updated user data");
			}, (error) => {
				Debug.Log (error.ErrorDetails);
			});

		}

        public  int GetHighScore () {

            int value = 0;

			//tring PlayFabId = NetworkManager.UserID;

			GetUserDataRequest request = new GetUserDataRequest () {
				PlayFabId = PlayFabId,
				Keys = null
			};

			PlayFabClientAPI.GetUserData (request, (result) => {
				if ((result.Data == null) || (result.Data.Count == 0)) {
					Debug.Log ("No user data available");
				} else {
					Dictionary<string,int> starsDic = new Dictionary<string, int> ();

                    if (starsDic.ContainsKey("HighScore"))					
					value = starsDic["HighScore"];

				}
			}, (error) => {
				Debug.Log ("Got error retrieving user data:");
				Debug.Log (error.ErrorMessage);
			});

            return value;
		}

        // public  void SetPlayerScore (int score) {

		// 	List<StatisticUpdate> stUpdateList = new List<StatisticUpdate> ();
		// 	StatisticUpdate stUpd = new StatisticUpdate ();
		// 	stUpd.StatisticName = "Score";
		// 	stUpd.Value = score;
		// 	stUpdateList.Add (stUpd);

		// 	UpdatePlayerStatisticsRequest request = new UpdatePlayerStatisticsRequest () {
		// 		Statistics = stUpdateList
		// 	};

		// 	PlayFabClientAPI.UpdatePlayerStatistics (request, (result) => {
		// 		Debug.Log ("Successfully updated user score");
		// 	}, (error) => {
		// 		Debug.Log (error.ErrorDetails);
		// 	});
		// }

        // public  void SetPlayerHighScore (int score) {

		// 	List<StatisticUpdate> stUpdateList = new List<StatisticUpdate> ();
		// 	StatisticUpdate stUpd = new StatisticUpdate ();
		// 	stUpd.StatisticName = "HighScore";
		// 	stUpd.Value = score;
		// 	stUpdateList.Add (stUpd);

		// 	UpdatePlayerStatisticsRequest request = new UpdatePlayerStatisticsRequest () {
		// 		Statistics = stUpdateList
		// 	};

		// 	PlayFabClientAPI.UpdatePlayerStatistics (request, (result) => {
		// 		Debug.Log ("Successfully updated user score");
		// 	}, (error) => {
		// 		Debug.Log (error.ErrorDetails);
		// 	});
		// }

        public   List<StatisticValue>  GetPlayerScore () {
            List<StatisticValue> list = new List<StatisticValue>();
			GetPlayerStatisticsRequest request = new GetPlayerStatisticsRequest () {
				StatisticNames = new List<string> () { "Score" }
			};
					
			PlayFabClientAPI.GetPlayerStatistics (request, (result) => {
				if ((result.Statistics == null)) {
					Debug.Log ("No user data available");
				} else {
					foreach (var item in result.Statistics) {
						// if (item.StatisticName == "Level_" + levelManager.currentLevel) {
						// 	//Callback (item.Value);
						// }
	//					Debug.Log ("    " + item.StatisticName + " == " + item.Value);
                        list = result.Statistics;
					}
				}
			}, (error) => {
				Debug.Log (error.ErrorDetails);
			});
            return list;

		}

        public   List<StatisticValue>  GetPlayerHighScore () {
            List<StatisticValue> list = new List<StatisticValue>();
			GetPlayerStatisticsRequest request = new GetPlayerStatisticsRequest () {
				StatisticNames = new List<string> () { "HighScore" }
			};
					
			PlayFabClientAPI.GetPlayerStatistics (request, (result) => {
				if ((result.Statistics == null)) {
					Debug.Log ("No user data available");
				} else {
					foreach (var item in result.Statistics) {
						// if (item.StatisticName == "Level_" + levelManager.currentLevel) {
						// 	//Callback (item.Value);
						// }
	//					Debug.Log ("    " + item.StatisticName + " == " + item.Value);
                        list = result.Statistics;
					}
				}
			}, (error) => {
				Debug.Log (error.ErrorDetails);
			});
            return list;

		}

    public class LeadboardPlayerData
	{
		public string Name;
		public string userID;
		public int position;
		public int score;
		public Sprite picture;
		public FriendData friendData;
	}

    public class FriendData
	{
		public string userID;
		public string FacebookID;
		public Sprite picture;
		public int level;
		public GameObject avatar;
	}



	}
