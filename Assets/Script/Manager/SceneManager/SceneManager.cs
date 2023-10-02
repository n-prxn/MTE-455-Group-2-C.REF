using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "SceneManager")]
public class SceneManager : ScriptableObject
{
    private Stack<int> loadedLevels;

    [NonSerialized] private bool initialized;

    private void Init()
    {
        loadedLevels = new Stack<int>();
        initialized = true;
    }

    public UnityEngine.SceneManagement.Scene GetActiveScene()
    {
        return UnityEngine.SceneManagement.SceneManager.GetActiveScene();
    }

    public void LoadSceneNormal(int buildIndex)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(buildIndex);
    }

    public void LoadSceneNormal(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void LoadScene(int buildIndex)
    {
        if (!initialized) Init();
        loadedLevels.Push(GetActiveScene().buildIndex);
        UnityEngine.SceneManagement.SceneManager.LoadScene(buildIndex);
    }

    public void LoadScene(string sceneName)
    {
        if (!initialized) Init();
        loadedLevels.Push(GetActiveScene().buildIndex);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneAsync(string sceneName)
    {
        if (!initialized) Init();
        loadedLevels.Push(GetActiveScene().buildIndex);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
    }

    public void LoadSceneAsync(int buildIndex)
    {
        if (!initialized) Init();
        loadedLevels.Push(GetActiveScene().buildIndex);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(buildIndex);
    }

    public AsyncOperation GetSceneAsync(string sceneName)
    {
        if (!initialized) Init();
        loadedLevels.Push(GetActiveScene().buildIndex);
        return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
    }

    public AsyncOperation GetSceneAsync(int buildIndex)
    {
        if (!initialized) Init();
        loadedLevels.Push(GetActiveScene().buildIndex);
        return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(buildIndex);
    }

    public AsyncOperation LoadAsyncPreviousScene()
    {
        if (!initialized)
        {
            Debug.LogError("You haven't used the LoadScene functions of the scriptable object. Use them instead of the LoadScene functions of Unity's SceneManager.");
            return null;
        }

        if (loadedLevels.Count > 0)
        {
            return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(loadedLevels.Pop());
            /*DataManager.instance.SaveGame();
            DataManager.instance.LoadGame();*/
        }
        else
        {
            Debug.LogError("No previous scene loaded");
            return null;
            //Application.Quit();
        }
    }

    public void LoadPreviousScene()
    {
        if (!initialized)
        {
            Debug.LogError("You haven't used the LoadScene functions of the scriptable object. Use them instead of the LoadScene functions of Unity's SceneManager.");
        }
        if (loadedLevels.Count > 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(loadedLevels.Pop());
            /*DataManager.instance.SaveGame();
            DataManager.instance.LoadGame();*/
        }
        else
        {
            Debug.LogError("No previous scene loaded");
            //Application.Quit();
        }
    }
}
