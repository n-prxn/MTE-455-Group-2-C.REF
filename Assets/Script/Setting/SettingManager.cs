using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [Header("Graphic Setting")]
    [Header("Dropdown")]
    [SerializeField] private TMP_Dropdown screenModeDropdown;
    [SerializeField] private TMP_Dropdown screenResDropdown;
    [Header("Audio Setting")]
    [Header("Slider")]
    [SerializeField] private Slider BGSlider;
    [SerializeField] private Slider voiceSlider;
    [SerializeField] private Slider FXSlider;
    [Header("Text")]
    [SerializeField] private TMP_Text BGValue;
    [SerializeField] private TMP_Text voiceValue;
    [SerializeField] private TMP_Text FXValue;
    [Header("Audio Source")]
    [SerializeField] private AudioSource BGSource;
    [SerializeField] private AudioSource voiceSource;
    [SerializeField] private AudioSource FXSource;
    private float backgroundMusic, voice, soundEffect;
    [SerializeField] SettingSO setting;
    [SerializeField] SceneManager sceneManager;

    void Awake()
    {
        BGSource = GameObject.FindGameObjectWithTag("Music Audio").GetComponent<AudioSource>();
        voiceSource = GameObject.FindGameObjectWithTag("Voice Audio").GetComponent<AudioSource>();
        FXSource = GameObject.FindGameObjectWithTag("FX Audio").GetComponent<AudioSource>();
        
        BGSlider.value = setting.backgroundMusic;
        voiceSlider.value = setting.voice;
        FXSlider.value = setting.soundEffect;

        screenModeDropdown.value = setting.screenMode ? 0 : 1;
        switch (setting.screenResLength)
        {
            case 1080:
                screenResDropdown.value = 0;
                break;
            case 720:
                screenResDropdown.value = 1;
                break;
            case 540:
                screenResDropdown.value = 2;
                break;
            case 360:
                screenResDropdown.value = 3;
                break;
        }
    }

    void Update()
    {
        setting.backgroundMusic = backgroundMusic;
        setting.voice = voice;
        setting.soundEffect = soundEffect;

        BGValue.text = (setting.backgroundMusic / 100).ToString("0%");
        voiceValue.text = (setting.voice / 100).ToString("0%");
        FXValue.text = (setting.soundEffect / 100).ToString("0%");
    }

    public void ChangeBackgroundValue()
    {
        backgroundMusic = BGSlider.value;
        BGSource.volume = backgroundMusic / 100f;
    }

    public void ChangeVoiceValue()
    {
        voice = voiceSlider.value;
        voiceSource.volume = voice / 100f;
    }

    public void ChangeEffectValue()
    {
        soundEffect = FXSlider.value;
        FXSource.volume = soundEffect / 100f;
    }

    public void ChangeScreenMode()
    {
        switch (screenModeDropdown.value)
        {
            case 0:
                Screen.fullScreen = true;
                setting.screenMode = true;
                break;
            case 1:
                Screen.fullScreen = false;
                setting.screenMode = false;
                break;
        }
    }

    public void ChangeScreenRes()
    {
        switch (screenResDropdown.value)
        {
            case 0:
                Screen.SetResolution(1920, 1080, setting.screenMode);
                setting.screenResWidth = 1080;
                setting.screenResLength = 1920;
                break;
            case 1:
                Screen.SetResolution(1280, 720, setting.screenMode);
                setting.screenResWidth = 720;
                setting.screenResLength = 1280;
                break;
            case 2:
                Screen.SetResolution(960, 540, setting.screenMode);
                setting.screenResWidth = 540;
                setting.screenResLength = 960;
                break;
            case 3:
                Screen.SetResolution(640, 360, setting.screenMode);
                setting.screenResWidth = 360;
                setting.screenResLength = 640;
                break;
        }
    }

    void OnDisable(){
        EditorUtility.SetDirty(setting);
    }

    public void Back(){
        sceneManager.LoadPreviousScene();
    }
}
