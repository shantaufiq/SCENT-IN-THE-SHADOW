using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine.SceneManagement;

namespace ScentInTheShadow.Auth
{
    public class AuthController : MonoBehaviour
    {
        public enum AuthState
        {
            Login,
            Regis,
        }
        
        public AuthView View;
        private AuthState _currentState;


        public void RegisterUser()
        {
            var request = new RegisterPlayFabUserRequest
            {
                DisplayName = View.UsernameRegis.text,
                Email = View.EmailRegis.text,
                Password = View.PasswordRegis.text,

                RequireBothUsernameAndEmail = false
            };

            PlayFabClientAPI.RegisterPlayFabUser(request, OnregisterSuccess, OnError);
        }

        public void LoginUser()
        {
            var request = new LoginWithEmailAddressRequest
            {
                Email = View.EmailLogin.text,
                Password = View.PasswordLogin.text,
            };

            PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
        }

        private void OnLoginSuccess(LoginResult result)
        {
            View.MessageAlert.text = "Loggin In!";
            /*SceneManager.LoadScene("PlayerMoveTest");*/
        }

        private void OnError(PlayFabError error)
        {
            View.MessageAlert.text = error.ToString();
            Debug.Log(error.GenerateErrorReport());
        }

        private void OnregisterSuccess(RegisterPlayFabUserResult result)
        {
            View.MessageAlert.text = "Create New Account Successfuly!";
            _currentState = AuthState.Login;
            SetCurrentState();
        }

        public void SetCurrentState ()
        {
            switch (_currentState)
            {
                case AuthState.Login:
                    View.LoginPage.SetActive(true);
                    View.RegisterPage.SetActive(false);
                    break;
                case AuthState.Regis:
                    View.LoginPage.SetActive(false);
                    View.RegisterPage.SetActive(true);
                    break;
            }
        }
    }

}