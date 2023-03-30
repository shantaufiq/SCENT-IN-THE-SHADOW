using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ScentInTheShadow.Global.Auth
{
    public class AuthView : MonoBehaviour
    {
        public List<TMP_Text> MessageAlert;

        [Header("Login Request")]
        public GameObject LoginPage;
        public TMP_InputField EmailLogin;
        public TMP_InputField PasswordLogin;

        [Header("Register Request")]
        public GameObject RegisterPage;
        public TMP_InputField UsernameRegis;
        public TMP_InputField EmailRegis;
        public TMP_InputField PasswordRegis;

        [Header("RecoveryPassword")]
        public GameObject RecoveryPage;
        public TMP_InputField EmailRecovery;


        public void ResetAlert()
        {
            foreach (var alert in MessageAlert)
            {
                alert.text = " ";
            }
        }
    }
}