using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RadioManager : MonoBehaviour
{
    [Header("Radio")]
    public RadioMusic[] radioMusics;
    [SerializeField] private AudioSource audioSource;
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI musicNameText;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject pauseButton;
    int currentMusicIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentMusicIndex = Random.Range(0, radioMusics.Length - 1);
        if (audioSource.clip == null)
        {
            audioSource.clip = radioMusics[currentMusicIndex].RadioAudio;
            audioSource.Play();

            playButton.SetActive(false);
            pauseButton.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (audioSource.clip.length == audioSource.time)
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

        if (currentMusicIndex > radioMusics.Length)
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
            newMusicIndex = Random.Range(0, radioMusics.Length - 1);
        } while (newMusicIndex == currentMusicIndex);
        currentMusicIndex = newMusicIndex;

        audioSource.clip = radioMusics[currentMusicIndex].RadioAudio;
        audioSource.Play();

        playButton.SetActive(false);
        pauseButton.SetActive(true);
    }
}
