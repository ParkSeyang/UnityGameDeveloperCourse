using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerPrimaryAttributes : MonoBehaviour, IPlayerDataManager
{
    // 스킬 및 장비 착용에 필요한 스탯들
    // str(힘), dex(민첩), INT(지능), WIS(지혜), CON(체력)
    public int STR;
    public int DEX;
    public int INT;
    public int WIS;
    public int CON;

    private string playerAttributesDataPath;
    
    private void Start()
    {
        playerAttributesDataPath = Path.Combine(Application.persistentDataPath, "PlayerAttributes");
        SaveData(playerAttributesDataPath); 
        LoadData(playerAttributesDataPath);
    }
    
    public void SaveData(string path)
    {
        // PlayerPrimaryAttributes attributes = new PlayerPrimaryAttributes();
        var attributes = new PlayerPrimaryAttributes();
        attributes.STR = 2;
        attributes.DEX = 2;
        attributes.INT = 2;
        attributes.WIS = 2;
        attributes.CON = 2;
        // List<PlayerPrimaryAttributes> attributeList = new List<PlayerPrimaryAttributes>();
        var attributeList = new List<PlayerPrimaryAttributes>();
        
        attributeList.Add(attributes);
        
        JsonWriter.Save(attributeList, path);
        
        Debug.Log($"기본능력치 저장 : {path}");

    }

    public void LoadData(string path)
    {
        // List<PlayerPrimaryAttributes> loadedData = JsonReader.Load<List<PlayerPrimaryAttributes>>(path);
        var loadedData = JsonReader.Load<List<PlayerPrimaryAttributes>>(path);

        foreach (var attributeList in loadedData)
        {
            Debug.Log($"Player STR : {attributeList.STR}, Player DEX : {attributeList.DEX}" +
                      $"Player INT : {attributeList.INT}, Player WIS : {attributeList.WIS}" +
                      $"Player CON : {attributeList.CON}");
        }
    }
}
