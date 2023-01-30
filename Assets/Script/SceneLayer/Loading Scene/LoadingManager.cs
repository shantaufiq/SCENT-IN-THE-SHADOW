using ScentInTheShadow.Global.Manager;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ScentInTheShadow.Scene.LoadingScene
{
    public class LoadingManager : MonoBehaviour
    {
        [SerializeField] private Image loadingProgress;
        [SerializeField] private Slider fillProgress;
        [SerializeField] private TMP_Text persenText;
        [SerializeField] private SceneData scene;

        private void Start()
        {
            StartCoroutine(LoadScene_Coroutine(scene.TargetScene));
        }

        public IEnumerator LoadScene_Coroutine(string targetScene)
        {
            fillProgress.value = 0;
            loadingProgress.fillAmount = 0;

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(targetScene);
            asyncOperation.allowSceneActivation = false; 
            float progress = 0;
            while(!asyncOperation.isDone)
            {
                progress = Mathf.MoveTowards(progress, asyncOperation.progress, Time.deltaTime );
                fillProgress.value = progress;
                loadingProgress.fillAmount = progress;
                persenText.text =((int) (progress * 100)).ToString() + " %";
                yield return new WaitForSeconds(0.05f);
                if(progress >= 0.9f)
                {
                    fillProgress.value = 1;
                    asyncOperation.allowSceneActivation = true;
                }
            }
        }
    }
}