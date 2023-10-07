using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;

public class MainMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator animator;
    public SceneManager sceneManager;
    [Header("Audio Clips")]
    [SerializeField] AudioClip selectSFX;
    [SerializeField] AudioClip pressSFX;

    [Header("Audio Source")]
    [SerializeField] AudioSource SFXSource;
    [Header("Sequence")]
    [SerializeField] GameObject newGameScene;
    [SerializeField] GameObject continueScene;
    [SerializeField] GameObject collectionScene;
    [SerializeField] GameObject settingScene;
    [SerializeField] GameObject quitScene;
    [SerializeField] private SettingSO setting;
    void Awake()
    {
        SFXSource = GameObject.FindGameObjectWithTag("FX Audio").GetComponent<AudioSource>();
        string pathSaveName = Path.Combine(Application.dataPath, "GameDataSave.json");

        if (!File.Exists(pathSaveName) && gameObject.name == "Button 2")
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetTrigger("OnMouseEnter");
        SFXSource.PlayOneShot(selectSFX);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetTrigger("OnMouseExit");
    }

    public void GoToNewGame()
    {
        string pathSaveName = Path.Combine(Application.dataPath, "GameDataSave.json");


        setting.hasPlayTutorial = false;

        DataManager.instance.gameplayElements.Clear();
        foreach (GameObject i in GetDontDestroyOnLoadObjects())
        {
            if (i.name == "AudioController" || i.name == "DataManager")
            {
                continue;
            }
            else
            {
                Destroy(i);
            }
        }


        if (File.Exists(pathSaveName))
        {
            File.Delete(pathSaveName);
            Debug.Log("Delete Old Save");
        }
        //DataManager.instance.StartNewGame();
        newGameScene.SetActive(true);
    }

    public void GoToContinue()
    {
        DataManager.instance.gameplayElements.Clear();
        foreach (GameObject i in GetDontDestroyOnLoadObjects())
        {
            if (i.name == "AudioController" || i.name == "DataManager")
            {
                continue;
            }
            else
            {
                Destroy(i);
            }
        }
        
        continueScene.SetActive(true);
    }

    public void GoToSetting()
    {
        settingScene.SetActive(true);
    }

    public void GoToCollections()
    {
        collectionScene.SetActive(true);
    }

    public void GoToQuit()
    {
        quitScene.SetActive(true);
    }

    public static GameObject[] GetDontDestroyOnLoadObjects()
    {
        GameObject temp = null;
        try
        {
            temp = new GameObject();
            Object.DontDestroyOnLoad(temp);
            UnityEngine.SceneManagement.Scene dontDestroyOnLoad = temp.scene;
            Object.DestroyImmediate(temp);
            temp = null;

            return dontDestroyOnLoad.GetRootGameObjects();
        }
        finally
        {
            if (temp != null)
                Object.DestroyImmediate(temp);
        }
    }
}
