using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] private AudioSource BGMAudioSource;
    [SerializeField] private AudioSource studentAudioSource; 

    [Header("BGM")]
    [SerializeField] private AudioClip BGM;
    // Start is called before the first frame update
    void Start()
    {
        BGMAudioSource.clip = BGM;
        BGMAudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
