using ScentInTheShadow.Global.Manager;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ScentInTheShadow.Scene.LoadingScene
{
    public class LoadingManager : MonoBehaviour
    {
        public UnityEvent beforeLoad;
        public UnityEvent afterLoad;

        [SerializeField] private Image loadingProgress;
        [SerializeField] private Slider fillProgress;
        [SerializeField] private TMP_Text persenText;
        [SerializeField] private SceneData scene;

        public static LoadingManager instance;

        private void Awake()
        {
            instance = this;
        }

        public void StartLoading(string targetScene)
        {
            StartCoroutine(LoadScene_Coroutine(targetScene));
        }

        public IEnumerator LoadScene_Coroutine(string targetScene)
        {
            fillProgress.value = 0;
            loadingProgress.fillAmount = 0;

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(targetScene);
            asyncOperation.allowSceneActivation = false;
            beforeLoad?.Invoke();
            float progress = 0;
            while(!asyncOperation.isDone)
            {
                progress = Mathf.MoveTowards(progress, asyncOperation.progress, Time.deltaTime );
                fillProgress.value = progress;
                loadingProgress.fillAmount = progress;
                persenText.text =((int) (progress * 100)).ToString() + " %";
                
                if(progress >= 0.9f)
                {
                    fillProgress.value = 1;
                    loadingProgress.fillAmount = 1;
                    persenText.text = (int)100 + " %";
                    yield return new WaitForSeconds(2f);
                    afterLoad?.Invoke();
                    asyncOperation.allowSceneActivation = true;
                    
                }
                yield return new WaitForSeconds(0.05f);
            }
            
        }
    }
}