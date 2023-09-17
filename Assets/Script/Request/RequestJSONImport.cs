using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

public class RequestJSON
{
    public string id;
    public string name;
    public string requester;
    public string description;
    public string difficulty;
    public string isRepeatable;
    public string phyStat;
    public string intStat;
    public string comStat;
    public string total;
    public string credit;
    public string xp;
    public string happiness;
    public string crimeRate;
    public string pyroxene;
    public string duration;
    public string expiration;
    public string deHappiness;
    public string deCrimeRate;
    public string portrait;
    public string statusText;
}

public class RequestJSONImport : MonoBehaviour
{
    [SerializeField] TextAsset requestFile;
    List<RequestJSON> requestJSON;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReadJSON()
    {
        requestJSON = JsonConvert.DeserializeObject<List<RequestJSON>>(requestFile.text);
    }

    public void CreateSOfromJSON()
    {
        ReadJSON();
        foreach (RequestJSON requestJSON in requestJSON)
        {
            RequestSO requestSO = ScriptableObject.CreateInstance<RequestSO>();

            JSONtoRequestSO(requestSO, requestJSON);

            string path = "Assets/Script/Request/RequestSO/" + int.Parse(requestJSON.id).ToString("00") + ".asset";
            #if UNITY_EDITOR
            AssetDatabase.CreateAsset(requestSO, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            #endif
        }
    }

    public void JSONtoRequestSO(RequestSO requestSO, RequestJSON data)
    {
        requestSO.id = byte.Parse(data.id);
        requestSO.name = data.name;
        requestSO.description = data.description;
        requestSO.requesterName = data.requester;
        #if UNITY_EDITOR
        requestSO.portrait = AssetDatabase.LoadAssetAtPath<Sprite>(data.portrait);
        #endif
        requestSO.duration = data.duration == null ? 0 : int.Parse(data.duration);

        switch (data.difficulty)
        {
            case "Easy":
                requestSO.difficulty = Difficulty.Easy;
                requestSO.stamina = 10;
                break;
            case "Extreme":
                requestSO.difficulty = Difficulty.Hardcore;
                requestSO.stamina = 20;
                break;
            case "Hardcore":
                requestSO.difficulty = Difficulty.Extreme;
                requestSO.stamina = 30;
                break;
            case "Insane":
                requestSO.difficulty = Difficulty.Insane;
                requestSO.stamina = 40;
                break;
            case "Emergency":
                requestSO.difficulty = Difficulty.Emergency;
                requestSO.stamina = 50;
                break;
        }

        requestSO.isRepeatable = data.isRepeatable == "Yes" ? true : false;
        requestSO.phyStat = data.phyStat == null ? 0 : int.Parse(data.phyStat);
        requestSO.intStat = data.intStat == null ? 0 : int.Parse(data.intStat);
        requestSO.comStat = data.comStat == null ? 0 : int.Parse(data.comStat);
        requestSO.credit = data.credit == null ? 0 : int.Parse(data.credit);
        requestSO.xp = data.xp == null ? 0 : int.Parse(data.xp);
        requestSO.happiness = data.happiness == null ? 0 : int.Parse(data.happiness);
        requestSO.crimeRate = data.crimeRate == null ? 0 : int.Parse(data.crimeRate);
        requestSO.credit = 0;
        requestSO.pyroxene = data.pyroxene == null ? 0 : int.Parse(data.pyroxene);
        requestSO.availableDuration = data.duration == null ? 0 : int.Parse(data.duration);
        requestSO.duration = data.expiration == null ? 0 : int.Parse(data.expiration);
        requestSO.demeritHappiness = data.deHappiness == null ? 0 : int.Parse(data.deHappiness);
        requestSO.demeritCrimeRate = data.deCrimeRate == null ? 0 : int.Parse(data.deCrimeRate);
        requestSO.chatStatus = data.statusText;
    }

}

