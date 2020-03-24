
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;


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

    public void UpdateName (string userName)
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

    public  void GetFriends () 
    {
        PlayFab.ClientModels.GetFriendsListRequest request = new PlayFab.ClientModels.GetFriendsListRequest ()
        {
            IncludeFacebookFriends = true
        };

        PlayFabClientAPI.GetFriendsList (request, (result) => 
        {
            Dictionary<string,string> dic = new Dictionary<string, string> ();
            foreach (var item in result.Friends) 
            {
                dic.Add (item.FacebookInfo.FacebookId, item.FriendPlayFabId);
            }
        }, (error) => 
        {
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
            foreach (var item in result.Leaderboard) 
            {
                dic.Add (item.PlayFabId, item.StatValue);
            }
        }, (error) => {
            Debug.Log (error.ErrorDetails);
        });

        return dic;
    }

    public  void SetScore (int value) 
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic["Score"] = value.ToString();
        
        UpdateUserDataRequest request = new UpdateUserDataRequest () {
            Data = dic
        };

        PlayFabClientAPI.UpdateUserData (request, (result) => 
        {
            Debug.Log ("Successfully updated user data");
        }, (error) => 
            {
                Debug.Log (error.ErrorDetails);
            });

    }

    public  void SetHighScore (int value) 
    {
        
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

        int score = 0;
        GetUserDataRequest request = new GetUserDataRequest () 
        {
            PlayFabId = playFabId,
            Keys = null
        };

        PlayFabClientAPI.GetUserData (request, (result) => 
        {
            if ((result.Data == null) || (result.Data.Count == 0)) 
            {
                Debug.Log ("No user data available");
            } else 
            {
                Dictionary<string,int> starsDic = new Dictionary<string, int> ();

                if (starsDic.ContainsKey("HighScore"))					
                score = starsDic["HighScore"];
            }
        }, (error) => 
            {
                Debug.Log ("Got error retrieving user data:");
                Debug.Log (error.ErrorMessage);
            });

        return score;
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
