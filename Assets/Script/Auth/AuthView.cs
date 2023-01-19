using TMPro;
using UnityEngine;

namespace ScentInTheShadow.Auth
{
    public class AuthView : MonoBehaviour
    {
        public TMP_Text MessageAlert;

        [Header("Login Request")]
        public GameObject LoginPage;
        public TMP_InputField EmailLogin;
        public TMP_InputField PasswordLogin;

        [Header("Register Request")]
        public GameObject RegisterPage;
        public TMP_InputField UsernameRegis;
        public TMP_InputField EmailRegis;
        public TMP_InputField PasswordRegis;

        
    }
}