using ScentInTheShadow.Global.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ScentInTheShadow.Scene.MainScene
{
    public class MainSceneManager : MonoBehaviour
    {
        [SerializeField] private Button playButton;

        [SerializeField] private TMP_Text schoolText, countryText, genderText, programStudyText;
        [SerializeField] private GameObject completeProfilePopup;

        [SerializeField] private TMP_InputField schoolInput, countryInput, programStudyInput;

        private enum gender
        {
            Male,
            Famale
        }

        private gender GenderState;
        private string GenderName;

        private void Start()
        {
            playButton.onClick.AddListener(Play);
            CompleteProfile();
            SelectFamale();
            Invoke("LoadUserInfo", 0.5f);

        }

        private void Play()
        {
            GameManager.instance.Loadscene("ChapterSelectionScene");
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
            var request = new UserInfo()
            {
                School = schoolInput.text,
                Country = countryInput.text,
                Program_Study = programStudyInput.text,
                Gender = GenderName,
            };

            GameManager.instance.SetUserData(request);

            GameManager.instance.GetUserData();
            LoadUserInfo();

            completeProfilePopup.gameObject.SetActive(false);
        }

        private void CompleteProfile()
        {
            var user = GameManager.instance.User;

            if(user.School == "" || user.Country == "")
            {
                completeProfilePopup.SetActive(true);
                Debug.Log("open");
                Debug.Log(user.School);
            }
            else 
            {
                completeProfilePopup.SetActive(false);
                Debug.Log("close");
                Debug.Log(user.School);
            }
        }
        #endregion

        private void LoadUserInfo()
        {
            var Data = GameManager.instance.User;

            schoolText.text = Data.School;
            genderText.text = Data.Gender;
            countryText.text = Data.Country;
            programStudyText.text = Data.Program_Study;

        }
    }
}