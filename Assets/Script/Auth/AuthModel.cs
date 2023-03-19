using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScentInTheShadow.Auth
{
    public class AuthModel : MonoBehaviour
    {
        public string Username;
        public string Email;
        public string Password;
        public string ConfirmPassword;

        public void LoginRequest(string emailRequest, string passwordRequest)
        {
            Email = emailRequest;
            Password = passwordRequest;
        }

        public void RegisterRequest(string usernameRequest, string emailRequest, string passwordRequest)
        {
            Username = usernameRequest;
            Email = emailRequest;
            Password = passwordRequest;
        }
    }
}