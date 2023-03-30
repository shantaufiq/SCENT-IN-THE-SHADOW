using Newtonsoft.Json;
using PlayFab;
using PlayFab.ClientModels;
using ScentInTheShadow.Global.PlayerData;
using System;
using System.Collections.Generic;
using UnityEngine;
using ScentInTheShadow.Scene.MainMenu;

namespace ScentInTheShadow.Global.Manager
{
    [Serializable]
    public class UserInfo
    {
        public string Username;
        public string Gender;
        public string School;
        public string Program_Study;
        public string Country;
    }

    public class GameManager : MonoBehaviour
    {
        [HideInInspector] public LoginResult LoginResult;

        [SerializeField] private SceneData sceneManager;

        [Header("Player Data & User Data")]
        public InventoryData Inventory;
        public PlayerDataModel Player;
        public UserInfo User;

        public static GameManager instance;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
            }
        }

        #region Scene Manager

        public void Loadscene(String targetScene)
        {
            sceneManager.TargetScene = targetScene;
        }

        #endregion

        #region Data Manager

        public void SetUserData(UserInfo userRequest)
        {
            var request = new UpdateUserDataRequest()
            {
                Data = new Dictionary<string, string>()
                {
                    {"UserData", JsonConvert.SerializeObject(userRequest)}
                }
            };
            PlayFabClientAPI.UpdateUserData(request, result => Debug.Log(result), error => Debug.Log(error));
        }

        public void GetUserData()
        {
            var request = new GetUserDataRequest()
            {
                PlayFabId = LoginResult.PlayFabId,
                Keys = null
            };

            PlayFabClientAPI.GetUserData(request, OnGetUserDataSuccess, error => Debug.Log(error));
        }

        private void OnGetUserDataSuccess(GetUserDataResult result)
        {
            if (result.Data.ContainsKey("UserData"))
            {
                UserInfo data = JsonConvert.DeserializeObject<UserInfo>(result.Data["UserData"].Value);
                User = data;
                User.Username = LoginResult.InfoResultPayload.PlayerProfile.DisplayName;
                MainMenuManager.instance.OnPlayerLoginState(true);
            }
            else
            {
                var request = new UserInfo()
                {
                    Username = LoginResult.InfoResultPayload.PlayerProfile.DisplayName
                };

                SetUserData(request);
                GetUserData();
            }
        }

        public void SetPlayerData(PlayerDataModel playerRequest)
        {
            var request = new UpdateUserDataRequest()
            {
                Data = new Dictionary<string, string>()
                {
                    {"PlayerData", JsonConvert.SerializeObject(playerRequest)}
                }
            };
            PlayFabClientAPI.UpdateUserData(request, result => Debug.Log(result), error => Debug.Log(error));
        }

        public void GetPlayerData()
        {
            var request = new GetUserDataRequest()
            {
                PlayFabId = LoginResult.PlayFabId,
                Keys = null
            };

            PlayFabClientAPI.GetUserData(request, OnGetPlayerDataSuccess, error => Debug.Log(error));
        }

        private void OnGetPlayerDataSuccess(GetUserDataResult result)
        {
            if (result.Data.ContainsKey("PlayerData"))
            {
                PlayerDataModel data = JsonConvert.DeserializeObject<PlayerDataModel>(result.Data["PlayerData"].Value);
                Player = data;
            }
            else
            {
                var playerData = new PlayerDataModel()
                {
                    Health = 50,
                    Experience = 0,
                    Skor = 10,
                    Level = -1,
                    items = Inventory.items,
                };

                SetPlayerData(playerData);
                GetPlayerData();
            }

        }
        #endregion
    }

}