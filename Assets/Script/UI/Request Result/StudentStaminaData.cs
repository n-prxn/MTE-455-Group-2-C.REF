using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StudentStaminaData : MonoBehaviour
{
    [SerializeField] private Image portraitImage;
    [SerializeField] private Image staminaBar;
    [SerializeField] private TextMeshProUGUI staminaText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetData(Student student){
        staminaText.text = student.CurrentStamina.ToString() + " / " + student.stamina.ToString();
        staminaBar.fillAmount = (float)student.CurrentStamina / (float)student.stamina;
        portraitImage.sprite = student.portrait;
    }
}
