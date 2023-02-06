using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ScentInTheShadow.Scene.ChapterSelection
{
    public class ChapterButtonController : MonoBehaviour
    {
        public Button button;

        [SerializeField] private GameObject lockObject;
        [SerializeField] private TMP_Text chapterNameText;

        private string chapterName;
        private bool isUnlocked;


        private void Start()
        {
            button = GetComponent<Button>();
        }

        public void LoadChapterButtonInfo(string _chapterName, bool _isUnlocked)
        {
            Debug.Log(_chapterName + _isUnlocked);
            chapterName = _chapterName;
            isUnlocked = _isUnlocked;

            chapterNameText.text = chapterName;
            if(isUnlocked)
            {
                button.interactable = true;
                lockObject.gameObject.SetActive(false);
            }
            else
            {
                button.interactable = false;   
                lockObject.gameObject.SetActive(true);
            }
        }
    }
}