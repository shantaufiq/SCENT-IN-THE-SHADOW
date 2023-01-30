using ScentInTheShadow.Global.Manager;
using TMPro;
using UnityEngine;

namespace ScentInTheShadow.Scene.MainScene
{
    public class MainSceneManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text usernameText;
        [SerializeField] private TMP_Text emailText;

        private string username;
        private string email;

        private void Start()
        {
            LoadUserInfo();
        }

        private void LoadUserInfo()
        {
            username = GameManager.instance.user.UserName;
            usernameText.text = username;
        }
    }
}