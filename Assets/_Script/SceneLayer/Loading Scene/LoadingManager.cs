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

        [SerializeField] private Slider fillProgress;

        // [SerializeField] private Image loadingProgress;
        // [SerializeField] private TMP_Text persenText;

        public static LoadingManager instance;

        private void Awake()
        {
            instance = this;
        }

        public void StartLoading(string targetScene)
        {
            // StartCoroutine(LoadScene_Coroutine(targetScene));
            StartCoroutine(LoadScene(targetScene));
        }

        public IEnumerator LoadScene_Coroutine(string targetScene)
        {
            fillProgress.value = 0;
            // loadingProgress.fillAmount = 0;

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(targetScene);
            asyncOperation.allowSceneActivation = false;
            beforeLoad?.Invoke();
            float progress = 0;
            while (!asyncOperation.isDone)
            {
                progress = Mathf.MoveTowards(progress, asyncOperation.progress, Time.deltaTime);
                fillProgress.value = progress;
                // loadingProgress.fillAmount = progress;
                // persenText.text = ((int)(progress * 100)).ToString() + " %";

                if (progress >= 0.9f)
                {
                    fillProgress.value = 1;
                    // loadingProgress.fillAmount = 1;
                    // persenText.text = (int)100 + " %";
                    afterLoad?.Invoke();
                    yield return new WaitForSeconds(2f);

                    asyncOperation.allowSceneActivation = true;

                }
                // yield return new WaitForSeconds(0.05f);
            }
        }

        IEnumerator LoadScene(string sceneName)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            beforeLoad?.Invoke();

            while (!operation.isDone)
            {
                float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
                fillProgress.value = progressValue;

                yield return null;
            }

            // yield return new WaitUntil(() => SceneManager.GetActiveScene() == SceneManager.GetSceneByName(sceneName));
            // afterLoad?.Invoke();
        }
    }
}