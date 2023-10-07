using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    [SerializeField] private GameObject imggBG;
    [SerializeField] public EndingDialogue endDialog;
    [SerializeField] public int myIndex;

    public static BackgroundMove instant;
    private void Awake()
    {
        instant = this;
    }

    private void Update()
    {
        switch (myIndex)
        {
            case 0:

                break;
            case 1:

                break;
            case 2:
                if (imggBG.transform.localPosition.x <= 2040f)
                    imggBG.transform.localPosition += new Vector3(2800f * Time.deltaTime, 0, 0);

                if (imggBG.transform.localPosition.y >= -135f)
                    imggBG.transform.localPosition -= new Vector3(0, 2500f * Time.deltaTime, 0);

                if (imggBG.transform.localScale.x <= 3.13000011f)
                    imggBG.transform.localScale += new Vector3(3f * Time.deltaTime, 0, 0);

                if (imggBG.transform.localScale.y <= 3.13000011f)
                    imggBG.transform.localScale += new Vector3(0, 3f * Time.deltaTime, 0);

                break;
            case 3:

                break;
            case 4:
                if (imggBG.transform.localPosition.x <= 2057f)
                    imggBG.transform.localPosition += new Vector3(5000f * Time.deltaTime, 0, 0);

                if (imggBG.transform.localPosition.y >= -491f)
                    imggBG.transform.localPosition -= new Vector3(0, 2500f * Time.deltaTime, 0);

                if (imggBG.transform.localScale.x <= 4f)
                    imggBG.transform.localScale += new Vector3(3f * Time.deltaTime, 0, 0);

                if (imggBG.transform.localScale.y <= 4f)
                    imggBG.transform.localScale += new Vector3(0, 3f * Time.deltaTime, 0);

                //iroha Vector3(2057.3501,-491,0) Vector3(4,4,1.27788055)
                break;
            case 5:
                if (imggBG.transform.localPosition.x >= -1762.5f)
                    imggBG.transform.localPosition -= new Vector3(8000f * Time.deltaTime, 0, 0);

                if (imggBG.transform.localPosition.y >= -933)
                    imggBG.transform.localPosition -= new Vector3(0, 2500f * Time.deltaTime, 0);

                // if (imggBG.transform.localScale.x <= 4f)
                //     imggBG.transform.localScale += new Vector3(3f * Time.deltaTime, 0, 0);

                // if (imggBG.transform.localScale.y <= 4f)
                //     imggBG.transform.localScale += new Vector3(0, 3f * Time.deltaTime, 0);

                //rabu Vector3(-1762.5,-933,0)
                break;
            case 6:
                if (imggBG.transform.localPosition.x <= -67.5f)
                    imggBG.transform.localPosition += new Vector3(6000f * Time.deltaTime, 0, 0);

                if (imggBG.transform.localPosition.y <= -314)
                    imggBG.transform.localPosition += new Vector3(0, 2500f * Time.deltaTime, 0);

                // if (imggBG.transform.localScale.x <= 4f)
                //     imggBG.transform.localScale += new Vector3(3f * Time.deltaTime, 0, 0);

                // if (imggBG.transform.localScale.y <= 4f)
                //     imggBG.transform.localScale += new Vector3(0, 3f * Time.deltaTime, 0);

                //iroha Vector3(-67.5,-314,0)
                break;
            case 7:
                if (imggBG.transform.localPosition.x >= -1995f)
                    imggBG.transform.localPosition -= new Vector3(8000f * Time.deltaTime, 0, 0);

                if (imggBG.transform.localPosition.y <= -133)
                    imggBG.transform.localPosition += new Vector3(0, 2500f * Time.deltaTime, 0);

                // if (imggBG.transform.localScale.x <= 4f)
                //     imggBG.transform.localScale += new Vector3(3f * Time.deltaTime, 0, 0);

                // if (imggBG.transform.localScale.y <= 4f)
                //     imggBG.transform.localScale += new Vector3(0, 3f * Time.deltaTime, 0);
                //aru Vector3(-1995.09998,-133,0)
                break;
            case 8:

                break;
            case 9:

                break;
            case 10:

                break;
            case 11:
                if (imggBG.transform.localPosition.x <= -1087.5f)
                    imggBG.transform.localPosition += new Vector3(5000f * Time.deltaTime, 0, 0);

                if (imggBG.transform.localPosition.y <= -85f)
                    imggBG.transform.localPosition += new Vector3(0, 2500f * Time.deltaTime, 0);

                // if (imggBG.transform.localScale.x <= 4f)
                //     imggBG.transform.localScale += new Vector3(3f * Time.deltaTime, 0, 0);

                // if (imggBG.transform.localScale.y <= 4f)
                //     imggBG.transform.localScale += new Vector3(0, 3f * Time.deltaTime, 0);
                //ichika Vector3(-1087.59998,-85,0)
                break;
            case 12:
                if (imggBG.transform.localPosition.x <= 0f)
                    imggBG.transform.localPosition += new Vector3(2500f * Time.deltaTime, 0, 0);

                if (imggBG.transform.localPosition.y <= 7.54931116f)
                    imggBG.transform.localPosition += new Vector3(0, 2500f * Time.deltaTime, 0);

                if (imggBG.transform.localScale.x >= 1f)
                    imggBG.transform.localScale -= new Vector3(3f * Time.deltaTime, 0, 0);

                if (imggBG.transform.localScale.y >= 1f)
                    imggBG.transform.localScale -= new Vector3(0, 3f * Time.deltaTime, 0);
                //sensei Vector3(0,-7.54931116,0) Vector3(1,1,1)
                break;
            default:
                break;
        }
    }
}
