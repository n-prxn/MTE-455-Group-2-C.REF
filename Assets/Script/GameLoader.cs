using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    DataManager dataManager;
    StudentSpawner studentSpawner;
    [SerializeField] SettingSO setting;
    // Start is called before the first frame update

    void Start()
    {
        dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
        studentSpawner = GameObject.FindGameObjectWithTag("Student Parent").GetComponent<StudentSpawner>();

        /*AudioSource audioSource = GameObject.FindGameObjectWithTag("Music Audio").GetComponent<AudioSource>();
        audioSource.loop = false;
        if(!audioSource.isPlaying)
        {
            audioSource.Play();
            StartCoroutine(AudioFade.StartFade(audioSource,2f, setting.backgroundMusic / 100f));
        }*/

        dataManager.InitializeGame();
        studentSpawner.InitializeStudents();
        Debug.Log("Load Game");
    }
}
