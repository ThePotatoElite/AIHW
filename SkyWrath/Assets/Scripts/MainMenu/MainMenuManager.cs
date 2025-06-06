using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        private const string CLICK_ANY_HINT = "Click Anywhere To Continue";
   
        
        [SerializeField] private Image progressBar;
        [SerializeField] private TextMeshProUGUI loadingText;

        private AsyncOperation _loadSceneAsyncOperation;

        private bool _finishedLoading = false;
        private float _stopLoadingSceneAtProgress = 0.9f;

        public void LoadSceneAsync()
        {
            _loadSceneAsyncOperation = SceneManager.LoadSceneAsync(1);
            _loadSceneAsyncOperation.allowSceneActivation = false;
            loadingText.gameObject.SetActive(true);
            StartCoroutine(CheckLoadingProgressRoutine());
        }

        private void Update()
        {
            if (_loadSceneAsyncOperation != null && !_finishedLoading)
                progressBar.fillAmount = _loadSceneAsyncOperation.progress;
            else if (_finishedLoading)
                progressBar.fillAmount = 1;

            if (_finishedLoading && Input.anyKeyDown)
                _loadSceneAsyncOperation.allowSceneActivation = true;
        }
        
        IEnumerator CheckLoadingProgressRoutine()
        {
            yield return new WaitUntil(() => _loadSceneAsyncOperation.progress >= _stopLoadingSceneAtProgress);
            loadingText.text = CLICK_ANY_HINT;
            _finishedLoading = true;
        }
    }
}
