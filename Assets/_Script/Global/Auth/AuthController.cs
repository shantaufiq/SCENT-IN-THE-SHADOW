using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using ScentInTheShadow.Global.Manager;
using ScentInTheShadow.Scene.LoadingScene;
using ScentInTheShadow.Scene.MainMenu;

namespace ScentInTheShadow.Global.Auth
{
    public class AuthController : MonoBehaviour
    {
        public enum AuthState
        {
            Login,
            Regis,
            Recovery,
        }

        public AuthView View;
        private AuthState _currentState;

        #region Recovery Password
        public void RecoveryPassword()
        {
            var request = new SendAccountRecoveryEmailRequest
            {
                Email = View.EmailRecovery.text,
                TitleId = "4FFD8",
            };

            PlayFabClientAPI.SendAccountRecoveryEmail(request, OnRecoverySuccess, OnRecoveryError);
        }

        private void OnRecoverySuccess(SendAccountRecoveryEmailResult obj)
        {
            foreach (var alert in View.MessageAlert)
            {
                alert.text = "Check You're email!";
            }
            _currentState = AuthState.Login;
            SetCurrentState();
        }

        private void OnRecoveryError(PlayFabError obj)
        {
            foreach (var alert in View.MessageAlert)
            {
                alert.text = "Email not match any record";
            }
        }

        #endregion

        #region Register User
        public void RegisterUser()
        {
            var request = new RegisterPlayFabUserRequest
            {
                DisplayName = View.UsernameRegis.text,
                Email = View.EmailRegis.text,
                Password = View.PasswordRegis.text,

                RequireBothUsernameAndEmail = false
            };

            PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterError);
        }

        private void OnRegisterSuccess(RegisterPlayFabUserResult result)
        {
            foreach (var alert in View.MessageAlert)
            {
                alert.text = "Create New Account Successfuly!";
            }
            _currentState = AuthState.Login;
            SetCurrentState();
        }
        private void OnRegisterError(PlayFabError error)
        {
            foreach (var alert in View.MessageAlert)
            {
                alert.text = error.ErrorMessage;
            }
            Debug.Log(error.GenerateErrorReport());
        }
        #endregion

        #region Login User
        public void LoginUser()
        {
            var request = new LoginWithEmailAddressRequest
            {
                Email = View.EmailLogin.text,
                Password = View.PasswordLogin.text,

                InfoRequestParameters = new GetPlayerCombinedInfoRequestParams()
                {
                    GetPlayerProfile = true
                }
            };

            PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginError);

            MainMenuManager.instance.OnPlayerLoginState(false);
        }

        private void OnLoginSuccess(LoginResult result)
        {
            foreach (var alert in View.MessageAlert)
            {
                alert.text = "Loggin In!";
            }
            GameManager.instance.LoginResult = result;
            GameManager.instance.GetPlayerData();
            GameManager.instance.GetUserData();

            // LoadingManager.instance.StartLoading("MainScene");
        }

        private void OnLoginError(PlayFabError error)
        {
            foreach (var alert in View.MessageAlert)
            {
                alert.text = error.ErrorMessage;
            }
            Debug.Log(error.GenerateErrorReport());
        }
        #endregion


        public void SetCurrentState()
        {
            switch (_currentState)
            {
                case AuthState.Login:
                    View.LoginPage.SetActive(true);
                    View.RegisterPage.SetActive(false);
                    View.RecoveryPage.SetActive(false);
                    break;
                case AuthState.Regis:
                    View.LoginPage.SetActive(false);
                    View.RegisterPage.SetActive(true);
                    View.RecoveryPage.SetActive(false);
                    break;
                case AuthState.Recovery:
                    View.LoginPage.SetActive(false);
                    View.RegisterPage.SetActive(false);
                    View.RecoveryPage.SetActive(true);
                    break;
            }
        }
    }

}