using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaResultUI : MonoBehaviour
{
    [SerializeField] GachaPool gachaPool;
    [SerializeField] GameObject resultPrefab;
    [SerializeField] GameObject resultParent;
    [SerializeField] Image rarityIcon;

    [Header("Data")]
    [SerializeField] Sprite[] emojiRarityIcons;

    private int currentResultIndex = 0;

    public int CurrentResultIndex { get => currentResultIndex; set => currentResultIndex = value; }

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

    public void SetGachaHint(bool hasRare){
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
}
