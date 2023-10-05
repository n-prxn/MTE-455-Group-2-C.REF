using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;


public class AronaTutorialCutscene : MonoBehaviour
{
    public static AronaTutorialCutscene Instance;
    public SkeletonAnimation skeletonAnimation;
    public Spine.AnimationState spineAnimationState;
    public Spine.Skeleton skeleton;

    [SerializeField] private AudioSource audioSource;
    public AudioClip[] studentReactVoices;

    [SerializeField] private float duration;

    private void Awake()
    {
        Instance = this;
        audioSource = GameObject.FindGameObjectWithTag("Voice Audio").GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        spineAnimationState = skeletonAnimation.AnimationState;
        skeleton = skeletonAnimation.Skeleton;
        spineAnimationState.SetAnimation(0, "Idle_01", true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeLive2DFace(int dialogueIndex)
    {
        switch (dialogueIndex)
        {
            case 0:
                spineAnimationState.SetAnimation(1, "01", true); //1
                PlayStudentVoice(0);
                break;
            case 1:
                spineAnimationState.SetAnimation(1, "10", true); //2
                PlayStudentVoice(1);
                break;
            case 2:
                spineAnimationState.SetAnimation(1, "11", true); //3
                PlayStudentVoice(2);
                break;
            case 3:
                spineAnimationState.SetAnimation(1, "12", true); //4
                PlayStudentVoice(3);
                break;
            case 4:
                StartCoroutine(ChangeLive2D("04", "25"));
                StartCoroutine(PlayStudentVoice(4, 5));
                break;
            default:
                break;
        }
    }

    IEnumerator ChangeLive2D(string firstFace, string secondFace)
    {
        spineAnimationState.SetAnimation(1, firstFace, true); //5.1
        yield return new WaitForSeconds(duration);
        spineAnimationState.SetAnimation(1, secondFace, true); //5.2
    }

    void PlayStudentVoice(int index)
    {
        if (studentReactVoices[index] != null)
        {
            audioSource.Stop();
            audioSource.clip = studentReactVoices[index];
            audioSource.Play();
        }
    }

    IEnumerator PlayStudentVoice(int index1, int index2)
    {
        if (studentReactVoices[index1] != null && studentReactVoices[index2] != null)
        {
            audioSource.Stop();
            audioSource.clip = studentReactVoices[index1];
            audioSource.Play();
            yield return new WaitForSeconds(duration);
            audioSource.Stop();
            audioSource.clip = studentReactVoices[index2];
            audioSource.Play();
        }
    }
}
