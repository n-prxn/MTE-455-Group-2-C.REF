using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("Setting")]
    [Range(1, 3)] public float fadeInTime = 1.5f;
    [Range(0, 1)] public float volume = 0.5f;
    [Header("Audio Source")]
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource voiceAudioSource;
    [SerializeField] private AudioSource FXAudioSource;
    [Header("Audio List")]
    [SerializeField] private AudioClip[] titleVoice;
    [Header("Setting")]
    public SettingSO setting;
    public static AudioController instance;
    
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 1)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
        musicAudioSource.volume = 0;
        StartCoroutine(AudioFade.StartFade(musicAudioSource, fadeInTime, setting.backgroundMusic / 100f));

        musicAudioSource.volume = setting.backgroundMusic / 100f;
        voiceAudioSource.volume = setting.voice / 100f;
        FXAudioSource.volume = setting.soundEffect / 100f;
    }

    void Start()
    {
        if (!setting.isTitleVoicePlay)
        {
            setting.isTitleVoicePlay = true;
            voiceAudioSource.clip = titleVoice[Random.Range(0, titleVoice.Length - 1)];
            StartCoroutine(WaitForFade());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 0){
            musicAudioSource.loop = true;
        }else{
            musicAudioSource.loop = false;
        }
    }

    IEnumerator WaitForFade()
    {
        yield return new WaitForSeconds(fadeInTime - 0.5f);
        voiceAudioSource.Play();
    }

    private void OnApplicationQuit()
    {
        setting.isTitleVoicePlay = false;
    }
}
