using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;

public class GameData_Json : MonoBehaviour
{
  [Serializable]
  public class PlayerStats
  {
    public string Name;
    public float HP;
    public float MP;
    public float Atk;
    public float Def;
    public int Exp;
    public int Level;
    public float MoveSpeed;
    public int SkillPoint;
  }

  [Serializable]
  public class PlayerAttributes
  {
    public int STR;
    public int DEX;
    public int INT;
    public int WIS;
    public int CON;

  }

  private string statsSavePath;
  private string attributesSavePath;
  

  private void Start()
  {
    statsSavePath = Path.Combine(Application.persistentDataPath, "PlayerStats");
    attributesSavePath = Path.Combine(Application.persistentDataPath, "PlayerAttributes");
    
    SaveStats(statsSavePath);
    SaveAttributes(attributesSavePath);
    
    LoadStats(statsSavePath);
    LoadAttributes(attributesSavePath);


  }


  private void SaveStats(string path)
  {
    // PlayerRawStat stat = new PlayerRawStat();
    var stat = new PlayerStats();
    stat.Name = "ZeroDarkMos";
    stat.HP = 100.0f;
    stat.MP = 100.0f;
    stat.Atk = 5.0f;
    stat.Def = 1.0f;
    stat.Exp = 0;
    stat.Level = 1;
    stat.MoveSpeed = 5.0f;
    stat.SkillPoint = 0;
        
    // List<PlayerStats> statList = new List<PlayerStats>();
    var statList = new List<PlayerStats>();
    statList.Add(stat);
    JsonWriter.Save(statList, path);
        
    Debug.Log($"기본정보 저장 완료 : {path}");
  }

  private void SaveAttributes(string path)
  {
    // PlayerPrimaryAttributes attributes = new PlayerPrimaryAttributes();
    var attributes = new PlayerAttributes();
    attributes.STR = 2;
    attributes.DEX = 2;
    attributes.INT = 2;
    attributes.WIS = 2;
    attributes.CON = 2;
    // List<PlayerPrimaryAttributes> attributeList = new List<PlayerPrimaryAttributes>();
    var attributeList = new List<PlayerAttributes>();
        
    attributeList.Add(attributes);
        
    JsonWriter.Save(attributeList, path);
        
    Debug.Log($"기본능력치 저장 : {path}");
  }

  private void LoadStats(string path)
  {
    // List<PlayerRawStat> loadedData = JsonReader.Load<List<PlayerRawStat>>(path);
    var loadedData = JsonReader.Load<List<PlayerStats>>(path);

    foreach (var statList in loadedData)
    {
      Debug.Log($"RawStat.Name : {statList.Name}, RawStat.HP : {statList.HP}, RawStat.MP : {statList.MP}, " +
                $"RawStat.Atk : {statList.Atk}, RawStat.Def : {statList.Def}, RawStat.Exp : {statList.Exp}, " +
                $"RawStat.Level : {statList.Level}, RawStat.MoveSpeed : {statList.MoveSpeed}, " +
                $"RawStat.SkillPoint : {statList.SkillPoint}");
    }
  }

  private void LoadAttributes(string path)
  {
    // List<PlayerPrimaryAttributes> loadedData = JsonReader.Load<List<PlayerPrimaryAttributes>>(path);
    var loadedData = JsonReader.Load<List<PlayerAttributes>>(path);

    foreach (var attributeList in loadedData)
    {
      Debug.Log($"Player STR : {attributeList.STR}, Player DEX : {attributeList.DEX}, " +
                $"Player INT : {attributeList.INT}, Player WIS : {attributeList.WIS}, " +
                $"Player CON : {attributeList.CON}");
    }
  }

}

