using ScentInTheShadow.Global.Manager;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ScentInTheShadow.Scene.MainScene
{
    public class MainSceneManager : MonoBehaviour
    {
        [Header("Text")]
        [SerializeField] private TMP_Text usernamText, schoolText, countryText, genderText, programStudyText;

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

        private void Start()
        {
            var manager = FindObjectOfType<GameManager>();
            if(manager == null)
            {
                SceneManager.LoadScene("AuthenticcationScene");
            }
            else
            {
                Invoke("CompleteProfile", 0.2f);
                Invoke("LoadUserInfo", 0.5f);
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
            switch(GenderState)
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

        private void CompleteProfile()
        {
            var user = GameManager.instance.User;

            if(user.School == null || user.Country == null)
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

            schoolInput.text =  Data.School;
            countryInput.text =  Data.Country;
            programStudyInput.text = Data.Program_Study;
            if(Data.Gender == "Male")
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

            usernamText.text = "Name : " + Data.Username;
            schoolText.text = "School : " + Data.School;
            genderText.text = "Gender : " + Data.Gender;
            countryText.text = "Country : " + Data.Country;
            programStudyText.text = "Program Study : " + Data.Program_Study;

        }
    }
}