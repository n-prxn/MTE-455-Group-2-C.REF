using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class ToggleActiveAnim : MonoBehaviour
{
    [SerializeField] PlayableDirector playableDirector;
    [SerializeField] Button Roll10Button;
    [SerializeField] Button Roll1Button;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Button skipButton;
    [SerializeField] GameObject nextSymbol;
    [SerializeField] Image loadingBar;
    [SerializeField] SettingSO setting;
    public SceneManager sceneManager;

    public void SetInactive(GameObject scene)
    {
        scene.SetActive(false);
        Roll10Button.interactable = false;
        Roll1Button.interactable = false;
    }

    public void SkipTutorial(){
        playableDirector.time = 9.15f;
        playableDirector.Pause();
        playableDirector.Play();
        skipButton.gameObject.SetActive(false);
        Destroy(nextSymbol);
    }

    public void PauseAnim()
    {
        playableDirector.Pause();
        if (Roll10Button != null)
            Roll10Button.interactable = true;
        if (Roll1Button != null)
            Roll1Button.interactable = true;
    }

    public void SetSelfInactive()
    {
        gameObject.SetActive(false);
    }

    public void LoadScene(int sceneID)
    {
        StartCoroutine(LoadSceneAsync(sceneID));
    }

    IEnumerator LoadSceneAsync(int sceneID)
    {
        AsyncOperation operation = sceneManager.GetSceneAsync(sceneID);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBar.fillAmount = progressValue;
            yield return null;
        }
    }

    public void FadeOutMusic()
    {
        AudioSource audioSource = GameObject.FindGameObjectWithTag("Music Audio").GetComponent<AudioSource>();
        StartCoroutine(AudioFade.StartFade(audioSource, 2, 0));
    }

    public void FadeInMusic()
    {
        AudioSource audioSource = GameObject.FindGameObjectWithTag("Music Audio").GetComponent<AudioSource>();
        StartCoroutine(AudioFade.StartFade(audioSource, 2, setting.backgroundMusic / 100));
    }

    public void PlayMusic(RadioMusic music)
    {
        AudioSource audioSource = GameObject.FindGameObjectWithTag("Music Audio").GetComponent<AudioSource>();
        audioSource.clip = music.RadioAudio;
        audioSource.loop = true;
        audioSource.Play();
        StartCoroutine(AudioFade.StartFade(audioSource, 2, setting.backgroundMusic / 100));
    }
}
