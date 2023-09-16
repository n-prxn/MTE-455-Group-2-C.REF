using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Music", menuName = "Radio Music")]
public class RadioMusic : ScriptableObject
{
    [SerializeField] string audioName;
    public string AudioName { get => audioName; set => audioName = value; }
    [SerializeField] AudioClip radioAudio;
    public AudioClip RadioAudio { get => radioAudio; set => radioAudio = value; }
    
}
