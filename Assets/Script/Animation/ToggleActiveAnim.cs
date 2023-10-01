using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class ToggleActiveAnim : MonoBehaviour
{
    [SerializeField] PlayableDirector playableDirector;
    [SerializeField] Button resultButton;
    public void SetInactive(GameObject scene){
        scene.SetActive(false);
        resultButton.interactable = false;
    }

    public void PauseAnim(){
        playableDirector.Pause();
        resultButton.interactable = true;
    }
}
