using ScentInTheShadow.Global.Manager;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

namespace ScentInTheShadow.Scene.MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        public static MainMenuManager instance;

        [Header("Text")]
        [SerializeField] private TMP_InputField usernamText, schoolText, countryText, genderText, programStudyText;

        [Header("Popups")]
        [SerializeField] private GameObject completeProfilePopup;

        [Header("InputField")]
        [SerializeField] private TMP_InputField schoolInput, countryInput, programStudyInput;

        [Header("Toggle")]
        [SerializeField] private Toggle MaleToggle;
        [SerializeField] private Toggle FamaleToggle;

        private enum gender
        {
            Male,
            Famale
        }

        private gender GenderState;
        private string GenderName;

        [Header("Main Menu Components")]
        [SerializeField] GameManager _gameManager;
        public UnityEvent beforeLogin;
        public UnityEvent afterLogin;
        bool _playerLoginState = false;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        #region EditUserProfile
        public void SelectMale()
        {
            GenderState = gender.Male;
            SelectGender();
        }

        public void SelectFamale()
        {
            GenderState = gender.Famale;
            SelectGender();
        }

        public void SelectGender()
        {
            switch (GenderState)
            {
                case gender.Male:
                    GenderName = "Male";
                    break;
                case gender.Famale:
                    GenderName = "Famale";
                    break;
            }
        }

        public void SetUserDataRequest()
        {
            StartCoroutine(SetUserDataRequest_Coroutine());
        }

        IEnumerator SetUserDataRequest_Coroutine()
        {
            var request = new UserInfo()
            {
                Username = GameManager.instance.User.Username,
                School = schoolInput.text,
                Country = countryInput.text,
                Program_Study = programStudyInput.text,
                Gender = GenderName,
            };

            GameManager.instance.SetUserData(request);

            yield return new WaitForSeconds(0.2f);
            GameManager.instance.GetUserData();

            yield return new WaitForSeconds(0.5f);
            LoadUserInfo();
            yield return new WaitForSeconds(1f);
            completeProfilePopup.gameObject.SetActive(false);
        }

        public void CompleteProfile()
        {
            var user = GameManager.instance.User;

            if (user.School == null || user.Country == null)
            {
                completeProfilePopup.SetActive(true);
            }
            else
            {
                completeProfilePopup.SetActive(false);
            }
        }

        public void EditProfile()
        {
            var Data = GameManager.instance.User;

            schoolInput.text = Data.School;
            countryInput.text = Data.Country;
            programStudyInput.text = Data.Program_Study;
            if (Data.Gender == "Male")
            {
                MaleToggle.isOn = true;
                SelectMale();
            }
            else
            {
                FamaleToggle.isOn = true;
                SelectFamale();
            }

            completeProfilePopup.SetActive(true);
        }
        #endregion

        private void LoadUserInfo()
        {
            var Data = GameManager.instance.User;

            usernamText.text = Data.Username;
            schoolText.text = Data.School;
            genderText.text = Data.Gender;
            countryText.text = Data.Country;
            programStudyText.text = Data.Program_Study;

            Debug.Log($"{Time.time} LoadUserInfo school : {Data.School}");
        }

        public void OnPlayerLoginState(bool state)
        {
            if (state == false)
            {
                _playerLoginState = state;
                StartCoroutine(TryToLoadUserInfo());
            }
            else
            {
                _playerLoginState = state;
            }

            Debug.Log($"{Time.time} OnPlayerLoginState : {state}");
        }

        IEnumerator TryToLoadUserInfo()
        {
            beforeLogin?.Invoke();
            yield return new WaitUntil(() => _playerLoginState == true);
            LoadUserInfo();
            afterLogin?.Invoke();
        }
    }
}