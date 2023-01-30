using Newtonsoft.Json;
using PlayFab;
using PlayFab.ClientModels;
using ScentInTheShadow.Global.PlayerData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ScentInTheShadow.Global.Manager
{
    public class UserInfo
    {
        public string UserName;
        public string Email;
    }
    
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private SceneData sceneManager;

        public PlayerDataModel Player;
        public InventoryData Inventory;
        public UserInfo user = new UserInfo();
       
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
            SceneManager.LoadScene("LoadingScene");
        }

        #endregion

        #region Data Manager

        public void GetUserData(string myPlayfabId)
        {
            GetPlayerData(myPlayfabId);
        }

        public void SetPlayerData()
        {
            var playerData = new PlayerDataModel()
            {
                Health = 100,
                Experience = 0,
                Skor = 10,
                Level = -1,
                items = Inventory.items,
            };
            
            var request = new UpdateUserDataRequest()
            {
                Data = new Dictionary<string, string>()
                {
                    {"PlayerData", JsonConvert.SerializeObject(playerData) }
                }
            };
            PlayFabClientAPI.UpdateUserData(request, result => Debug.Log(result), error => Debug.Log(error));
        }

        public void GetPlayerData(string myPlayfabId)
        {
            var request = new GetUserDataRequest()
            {
                PlayFabId = myPlayfabId,
                Keys = null
            };

            PlayFabClientAPI.GetUserData(request, OnGetPlayerDataSuccess, error => Debug.Log(error));
        }

        private void OnGetPlayerDataSuccess(GetUserDataResult result)
        {
            if(result.Data.ContainsKey("PlayerData"))
            {
                PlayerDataModel data = JsonConvert.DeserializeObject<PlayerDataModel>(result.Data["PlayerData"].Value);
                Player = data;
            }
            else
            {
                SetPlayerData();
            }

        }
        #endregion
    }

}