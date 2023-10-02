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
    public void SetInactive(GameObject scene)
    {
        scene.SetActive(false);
        Roll10Button.interactable = false;
        Roll1Button.interactable = false;
    }

    public void PauseAnim()
    {
        playableDirector.Pause();
        Roll10Button.interactable = true;
        Roll1Button.interactable = true;
    }

    public void SetSelfInactive()
    {
        gameObject.SetActive(false);
    }
}
