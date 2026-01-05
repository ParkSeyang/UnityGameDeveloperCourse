using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;


// 유저의 기본 스탯들
[Serializable]
public class PlayerRawStat : MonoBehaviour, IPlayerDataManager
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
    public Vector3 Position;

    private string playerStatDataPath;

    private void Start()
    {
        playerStatDataPath = Path.Combine(Application.persistentDataPath, "UserStatData");

    }
    

    public void SaveData(string path)
    {
        // PlayerRawStat stat = new PlayerRawStat();
        var stat = new PlayerRawStat();
        stat.Name = "ZeroDarkMos";
        stat.HP = 100.0f;
        stat.MP = 100.0f;
        stat.Atk = 5.0f;
        stat.Def = 1.0f;
        stat.Exp = 0;
        stat.Level = 1;
        stat.MoveSpeed = 5.0f;
        stat.SkillPoint = 0;
        stat.Position = transform.position;
        
        // List<PlayerRawStat> statList = new List<PlayerRawStat>();
        var statList = new List<PlayerRawStat>();
    
        statList.Add(stat);
        
        JsonWriter.Save(statList, path);
        
        Debug.Log($"기본정보 저장 완료 : {path}");

    }

    public void LoadData(string path)
    {
        // List<PlayerRawStat> loadedData = JsonReader.Load<List<PlayerRawStat>>(path);
        var loadedData = JsonReader.Load<List<PlayerRawStat>>(path);

        foreach (var statList in loadedData)
        {
            Debug.Log($"RawStat.Name : {statList.Name}, RawStat.HP : {statList.HP}, RawStat.MP : {statList.MP}" +
                      $"RawStat.Atk  : {statList.Atk}, RawStat.Def : {statList.Def}, RawStat.Exp : {statList.Exp}" +
                      $"RawStat.Level  : {statList.Level}, RawStat.MoveSpeed  : {statList.MoveSpeed}" +
                      $"RawStat.SkillPoint : {statList.SkillPoint}, RawStat.Position : {statList.Position}");
        }
    }
}