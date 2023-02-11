using ScentInTheShadow.Scene.LoadingScene;
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
        private string targetScene;
        private bool isUnlocked;


        private void Start()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(delegate { LoadingManager.instance.StartLoading(targetScene); });
        }

        public void LoadChapterButtonInfo(ChapterData.Chapter Data)
        {
            chapterName = Data.ChapterName;
            isUnlocked = Data.IsUnlocked;
            targetScene = Data.TargetScene;

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