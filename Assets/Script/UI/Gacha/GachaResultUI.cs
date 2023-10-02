using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class GachaResultUI : MonoBehaviour
{
    [SerializeField] GachaPool gachaPool;
    [SerializeField] GameObject resultPrefab;
    [SerializeField] GameObject resultParent;
    [SerializeField] Image rarityIcon;
    [SerializeField] Button Roll10Button;
    [SerializeField] Button Roll1Button;
    [SerializeField] PlayableDirector gachaSceneDirector;

    [Header("Data")]
    [SerializeField] Sprite[] emojiRarityIcons;

    public void InitializeResult(List<Student> students, List<bool> isNewList)
    {
        ResetResult();
        for (int i = 0; i < students.Count; i++)
        {
            GameObject gachaResultCard = Instantiate(resultPrefab, resultParent.transform);
            GachaResultCardData gachaResultCardData = gachaResultCard.GetComponent<GachaResultCardData>();
            gachaResultCardData.SetDetail(students[i], isNewList[i], i);
            if (i == 0)
            {
                gachaResultCard.SetActive(true);
            }
            else
            {
                gachaResultCard.SetActive(false);
            }
        }
        resultParent.SetActive(false);
    }

    public void SetGachaHint(bool hasRare)
    {
        if (hasRare)
            rarityIcon.sprite = emojiRarityIcons[1];
        else
            rarityIcon.sprite = emojiRarityIcons[0];
    }

    public void ResetResult()
    {
        foreach (Transform result in resultParent.transform)
        {
            Destroy(result.gameObject);
        }
    }

    public void ShowGachaResult()
    {
        Roll1Button.interactable = false;
        Roll10Button.interactable = false;
        gachaSceneDirector.Play();

        List<Student> pulledStudents = gachaPool.PulledStudents;
        List<bool> isNewList = gachaPool.IsNewList;
        bool hasRare = gachaPool.HasRare;

        InitializeResult(pulledStudents, isNewList);
        isNewList.Clear();
        pulledStudents.Clear();
        hasRare = false;
    }

    public void SetResultButton(int pullAmount){
        if(pullAmount <= 1){
            Roll10Button.gameObject.SetActive(false);
            Roll1Button.gameObject.SetActive(true);
        }else{
            Roll10Button.gameObject.SetActive(true);
            Roll1Button.gameObject.SetActive(false);
        }
    }
}
