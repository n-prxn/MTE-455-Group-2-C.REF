using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RadioManager : MonoBehaviour
{
    [Header("Radio")]
    public RadioMusic[] radioMusics;
    private AudioSource audioSource;
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI musicNameText;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject pauseButton;
    int currentMusicIndex = 0;
    public static RadioManager instance;

    void Awake()
    {
        audioSource = GameObject.FindGameObjectWithTag("Music Audio").GetComponent<AudioSource>();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void OnEnable()
    {
        audioSource.Stop();
        currentMusicIndex = Random.Range(0, radioMusics.Length);
        audioSource.clip = radioMusics[currentMusicIndex].RadioAudio;
        audioSource.Play();

        playButton.SetActive(false);
        pauseButton.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (audioSource.clip.length <= audioSource.time)
            NextMusic();

        musicNameText.text = radioMusics[currentMusicIndex].AudioName;
    }

    public void ToggleRadio()
    {
        if (audioSource.isPlaying)
        {
            Debug.Log("Pause");
            audioSource.Pause();
            playButton.SetActive(true);
            pauseButton.SetActive(false);
        }
        else
        {
            Debug.Log("Unpause");
            audioSource.UnPause();
            playButton.SetActive(false);
            pauseButton.SetActive(true);
        }
    }

    public void NextMusic()
    {
        audioSource.Stop();
        currentMusicIndex++;

        if (currentMusicIndex > radioMusics.Length - 1)
        {
            currentMusicIndex = 0;
        }

        audioSource.clip = radioMusics[currentMusicIndex].RadioAudio;
        audioSource.Play();
    }

    public void ShuffleMusic()
    {
        audioSource.Stop();
        int newMusicIndex;
        do
        {
            newMusicIndex = Random.Range(0, radioMusics.Length);
        } while (newMusicIndex == currentMusicIndex);
        currentMusicIndex = newMusicIndex;

        audioSource.clip = radioMusics[currentMusicIndex].RadioAudio;
        audioSource.Play();

        playButton.SetActive(false);
        pauseButton.SetActive(true);
    }
}
