
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class PlayFabManager 
{
    private string playFabId;

    public static PlayFabManager Instance = null;

    public void Login (string titleId) 
    {
        playFabId = titleId;
        LoginWithCustomIDRequest request = new LoginWithCustomIDRequest () 
        {
            TitleId = titleId,
            CreateAccount = true,
            CustomId = SystemInfo.deviceUniqueIdentifier
        };

        PlayFabClientAPI.LoginWithCustomID (request, (result) => 
        {
            playFabId = result.PlayFabId;
            Debug.Log ("Got PlayFabID: " + playFabId);

            if (result.NewlyCreated) 
            {
                Debug.Log ("(new account)");
            } else 
            {
                Debug.Log ("(existing account)");
            }
        },
            (error) => {
                Debug.Log (error.ErrorMessage);
            });
    }

    public void UpdateName (string userName, Action<string> Callback)
    {
        PlayFab.ClientModels.UpdateUserTitleDisplayNameRequest request = new PlayFab.ClientModels.UpdateUserTitleDisplayNameRequest () 
        {
            DisplayName = userName
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName (request, (result) => 
        {

        },
        (error) =>
        {
            Debug.Log (error.ErrorMessage);
        });

    }

    public bool IsYou (string playFabId) 
    {
        if (playFabId == this.playFabId)
        {
            return true;
        }
        return false;
    }

    public  void SetHighScore (int value) 
    {
			List<StatisticUpdate> stUpdateList = new List<StatisticUpdate> ();
			StatisticUpdate stUpd = new StatisticUpdate ();
			stUpd.StatisticName = "HighScore";
			stUpd.Value = value;
			stUpdateList.Add (stUpd);

			UpdatePlayerStatisticsRequest request = new UpdatePlayerStatisticsRequest () {
				Statistics = stUpdateList
			};

			PlayFabClientAPI.UpdatePlayerStatistics (request, (result) => 
            {
                PlayerPrefs.SetInt ("HighScore", value);
				Debug.Log ("Successfully updated user score");
			}, (error) => {
				Debug.Log (error.ErrorDetails);
			});

    }

    public void GetHighScore ()
    {
        GetPlayerStatisticsRequest request = new GetPlayerStatisticsRequest () {
				StatisticNames = new List<string> () { "Level" }
			};

			PlayFabClientAPI.GetPlayerStatistics (request, (result) => {
				if ((result.Statistics == null)) {
					Debug.Log ("No user data available");
				} else {
					// foreach (var item in result.Statistics) {
					// 	if (item.StatisticName == "Level")
					// 		//Callback (item.Value);
					// 	//Debug.Log("    " + item.StatisticName + " == " + item.Value);
					// }

				}
			}, (error) => {
				Debug.Log (error.ErrorDetails);
			});

    }

    public void GetHighScoreLeaderboard (Action<List<LeadboardPlayerData>> Callback)
    {
        PlayFab.ClientModels.GetLeaderboardRequest request = new PlayFab.ClientModels.GetLeaderboardRequest () {
				StatisticName = "HighScore",
				MaxResultsCount = 5,
                StartPosition = 0
				//PlayFabId = playFabId,
				//IncludeFacebookFriends = true
			};

			PlayFabClientAPI.GetLeaderboard (request, (result) => 
            {
				List<LeadboardPlayerData> list = new List<LeadboardPlayerData> ();
				foreach (var item in result.Leaderboard) {
					LeadboardPlayerData pl = new LeadboardPlayerData ();
					pl.Name = item.DisplayName;
					pl.userID = item.PlayFabId;
					pl.position = item.Position;
					pl.score = item.StatValue;

					list.Add (pl);
					Debug.Log (item.DisplayName + " " + item.PlayFabId + " " + item.Position + " " + item.StatValue);
				}
				Callback (list);

			}, (error) => {
				Debug.Log (error.ErrorDetails);
			});
    }

}
