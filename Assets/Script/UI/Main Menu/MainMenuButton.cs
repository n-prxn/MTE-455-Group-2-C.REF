using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator animator;
    public SceneManager sceneManager;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetTrigger("OnMouseEnter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetTrigger("OnMouseExit");
    }

    public void NewGame(){
        StartCoroutine(WaitForLoadScene("Gameplay"));
    }

    IEnumerator WaitForLoadScene(string sceneName){
        yield return new WaitForSeconds(1);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
