using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteInEditMode]
public class NewGetFromAnimatonEvent : MonoBehaviour
{
    [SerializeField] Material myMaterial;

    [SerializeField]
    [Range(1, 8)]
    private int col_U = 1;

    [SerializeField]
    [Range(1, 8)]
    private int row_V = 1;


    private void Awake()
    {
        foreach (Transform t in this.gameObject.transform)
        {
            if (t.gameObject.GetComponent<Renderer>() != null)
            {
                // Debug.Log(t);
                foreach (Material i in t.gameObject.GetComponent<Renderer>().sharedMaterials)
                {
                    if (i.name.Contains("_EyeMouth"))
                    {
                        myMaterial = i;
                    }
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnValidate()
    {
        // Debug.Log($"{col_U}, {row_V}");
        SetMouthMaterial();
    }

    public void SetMouthTile(int tile)
    {
        // Debug.Log(tile);
        row_V = (tile / 100) + 1;
        col_U = (tile % 10) + 1;
        SetMouthMaterial();
    }

    void SetMouthMaterial()
    {
        if (myMaterial != null)
        {
            myMaterial.SetInt("_U", col_U);
            myMaterial.SetInt("_V", row_V);
        }
    }

    public void AniEvt_DisableChildRenderer(int childIndex)
    {
        // Debug.Log(this.gameObject.transform.GetChild(0).GetChild(childIndex));
        this.gameObject.transform.GetChild(0).GetChild(childIndex).gameObject.SetActive(false);
    }
    public void AniEvt_EnableChildRenderer(int childIndex)
    {
        // Debug.Log(this.gameObject.transform.GetChild(0));
        this.gameObject.transform.GetChild(0).GetChild(childIndex).gameObject.SetActive(true);
    }

    public void AniEvt_InstantiateFx(GameObject obj)
    {
        return;
        Instantiate(obj, this.transform.GetChild(0));
    }

}
