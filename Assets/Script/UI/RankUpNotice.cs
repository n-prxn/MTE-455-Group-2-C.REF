using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankUpNotice : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI rankText;
    [SerializeField] TextMeshProUGUI otherRewardText;
    [SerializeField] TextMeshProUGUI itemNameText;
    [SerializeField] Image itemImage;

    private void OnEnable()
    {

    }

    public void UpdateRewardUI(ItemSO itemReward)
    {
        rankText.text = "RANK " + GameManager.Instance.rank.ToString();
        if(GameManager.Instance.rank == 3 || GameManager.Instance.rank == 6){
            otherRewardText.text = "+1 Request Capacity\n+1 Request Per Day\n+1 Shop Capacity";
        }
        otherRewardText.text = "+1 Request Capacity\n+1 Shop Capacity";
        itemNameText.text = itemReward.name;
        itemImage.sprite = itemReward.sprite;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UIDisplay.instance.ToggleBlackBackground(false);
            Destroy(this.gameObject);
        }
    }
}
