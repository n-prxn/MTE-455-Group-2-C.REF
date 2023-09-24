using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LoadingProcess : MonoBehaviour
{
    public GameObject loadingScreen;
    public Image loadingBar;
    
    public void LoadScene(int sceneID){
        StartCoroutine(LoadSceneAsync(sceneID));
    }

    IEnumerator LoadSceneAsync(int sceneID){
        AsyncOperation operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneID);
        
        loadingScreen.SetActive(true);
        
        while(!operation.isDone){
            float progressValue = Mathf.Clamp01(operation.progress /0.9f);
            loadingBar.fillAmount = progressValue;
            yield return null;
        }
    }
}
