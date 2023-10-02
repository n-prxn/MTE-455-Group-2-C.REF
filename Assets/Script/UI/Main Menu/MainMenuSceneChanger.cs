using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuSceneChanger : MonoBehaviour
{
    public GameObject loadingScreen;
    public Image loadingBar;
    public SceneManager sceneManager;
    public void LoadScene(int sceneID){
        StartCoroutine(LoadSceneAsync(sceneID));
    }

    public void LoadNormalScene(int sceneID){
        sceneManager.LoadScene(sceneID);
    }

    IEnumerator LoadSceneAsync(int sceneID){
        AsyncOperation operation = sceneManager.GetSceneAsync(sceneID);
        
        loadingScreen.SetActive(true);
        
        while(!operation.isDone){
            float progressValue = Mathf.Clamp01(operation.progress /0.9f);
            loadingBar.fillAmount = progressValue;
            yield return null;
        }
    }

    public void QuitGame(){
        Application.Quit();
    }
}
