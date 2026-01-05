using System;
using System.Collections.Generic;
using System.IO;
using FileIO.JsonDataModule;
using UnityEngine;
using Newtonsoft.Json.Linq;
using Random = UnityEngine.Random;

public class Study_JObject : MonoBehaviour
{
    /* JObject란?
     * 구체적인 데이터 클래스(DTO, Data Transfer Object)를 정의 하지 않고도
     * JSON 데이터를 다루기 위한 도구.
     * Unity에서 JSON을 다루기 위해 JsonUtility와 JObject(Newtonsoft.Json)
     * 두가지 중 Newtonsoft.Json에서만 사용 가능한 도구.
     * JsonUtility가 정적인 데이터 구조를 처리하는데에 최적화 되어있다면,
     * JObject는 비정형 데이터나 복잡한 중첩 구조를 유연하게 처리하는데 특화 되어 있습니다.
     *
     * JObject의 특징 3가지
     * - 동적 접근이 가능하다.
     * - 유연성 : 런타임에 추가, 삭제, 수정이 가능하다.
     * - Linq 지원 : JToken, JArray와 함께 사용되며, 복잡한 JSON 쿼리가 가능하다.
     *
     * JObject 사용시 주의사항
     * Garbage Collection(GC) 이슈
     * - JObject 파싱 과정에서 많은 참조 타입 객체를 생성 합니다. Update Loop에서는
     * 쓰지마세요. 초기화에 한번만 사용하는거는 괜찮음. 런타임시에 가끔 사용하는것도 OK
     * - 기본적으로 C# 리플렉션 기능을 사용합니다.
     * 느릴 수 있다는것(성능을 잡아먹는다.)을 기억해두십시오.
     */
    
    public enum UserType
    {
        None = 0,
        User,
        Vip,
        Admin
    }

    [Serializable]
    public class EquipmentData
    {
        public string UniqueID { get; set; }
        public int ItemLevel { get; set; }
        public int Durability { get; set; }

        public EquipmentData SetRandom()
        {
            UniqueID = Guid.NewGuid().ToString();  // Guid = 전역적으로 고유한 식별자를 만들어 내는 클래스.
            ItemLevel = Random.Range(1, 101);
            Durability = Random.Range(1, 101);
            return this;
        }
        public override string ToString()
        {
            return $"UniqueID : {UniqueID},  ItemLevel : {ItemLevel}, Durability : {Durability}";
        }
        
    }

    public class PlayerPosition
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public PlayerPosition(Vector3 position)
        {
            X = position.x;
            Y = position.y;
            Z = position.z;
        }

    }

    
    
    public class QuestData
    {
        public int QuestID { get; set; }
        public string QuestName { get; set; }
        public bool isCompleted { get; set; }

        public QuestData(int questID, string questName, bool iscompleted)
        {
            QuestID = questID;
            QuestName = questName;
            // 퀘스트 등급 나눠보기.
            isCompleted = iscompleted;
        }

    }

    private IJsonDataModule[] UserDataModule { get; set; }

    
    List<EquipmentData> inventory = new List<EquipmentData>();
    Dictionary<string, EquipmentData> equipInventory = new Dictionary<string, EquipmentData>();
    HashSet<QuestData> questdata = new HashSet<QuestData>();
    
    private string savePath;
    
    private void Start()
    {
        savePath = Path.Combine(Application.persistentDataPath, "UserData");
        CreateUserDataModule();
        
        //Save();
        Load();
        
        
        PrintUserDataModule();
    }

    private void CreateUserDataModule()
    {
        var head = new EquipmentData().SetRandom();
        var rightHand = new EquipmentData().SetRandom();
        var body = new EquipmentData().SetRandom();
        var leg = new EquipmentData().SetRandom();
        var foot = new EquipmentData().SetRandom();
        
        // 유저 인벤토리에는 10개의 아이템이 있음
        inventory.Add(head);
        inventory.Add(rightHand);
        inventory.Add(body);
        inventory.Add(leg);
        inventory.Add(foot);
        
        inventory.Add(new EquipmentData().SetRandom());
        inventory.Add(new EquipmentData().SetRandom());
        inventory.Add(new EquipmentData().SetRandom());
        inventory.Add(new EquipmentData().SetRandom());
        
        // 장착한건 5개
        
        equipInventory.Add("head", head);
        equipInventory.Add("rightHand", rightHand);
        equipInventory.Add("body", body);
        equipInventory.Add("leg", leg);
        equipInventory.Add("foot", foot);
        
        var fristQuest = new QuestData(1,"Tutorial Quest",true);
        var secondQuest = new QuestData(2, "RepeatQuest", true);
        var thirdQuest = new QuestData(3, "MadClown need Item", Random.value > 0.5f ? true : false);
        var randomQuest = new QuestData( Random.Range(0, 101), "KillMonster(UrgentQuest)",Random.value > 0.5f ? true : false);
        var randomQuest2 = new QuestData( Random.Range(0, 101), "Helping the farm", Random.value > 0.5f ? true : false);
        var randomQuest3 = new QuestData(Random.Range(0, 101), "Take the bandit", Random.value > 0.5f ? true : false);
        var randomQuest4 = new QuestData(Random.Range(0, 101), "Catch a wanted criminal", Random.value > 0.5f ? true : false);
        var randomQuest5 = new QuestData(Random.Range(0, 101), "To kill the head of a bandit", Random.value > 0.5f ? true : false);
        var randomQuest6 = new QuestData(Random.Range(0, 101), "Portion delivery", Random.value > 0.5f ? true : false);
        var randomQuest7 = new QuestData(Random.Range(0, 101), "Food delivery", Random.value > 0.5f ? true : false);
        
        questdata.Add(fristQuest);
        questdata.Add(secondQuest);
        questdata.Add(thirdQuest);
        questdata.Add(randomQuest);
        questdata.Add(randomQuest2);
        questdata.Add(randomQuest3);
        questdata.Add(randomQuest4);
        questdata.Add(randomQuest5);
        questdata.Add(randomQuest6);
        questdata.Add(randomQuest7);
        
        
        UserDataModule = new IJsonDataModule[]
        {
            new JsonDataModule<string>("name", "Tester"),
            new JsonDataModule<int>("gold", 100),
            new JsonDataModule<uint>("exp", 98),
            new JsonDataModule<float>("float", 1.139f),
            new JsonDataModule<bool>("isFrist", true),
            new JsonDataModule<UserType>("userType", UserType.User),
            
            new JsonDataModule<PlayerPosition>("lastPos", new PlayerPosition(new Vector3(9,9,9))),
            
            new ListDataModule<EquipmentData>("Inventory", inventory),
            new DictionaryDataModule<string, EquipmentData>("equipInventory", equipInventory),
            new HashSetDataModule<QuestData>("QuestData", questdata),
        };
    }

    private void PrintUserDataModule()
    {
        foreach (var data in UserDataModule)
        {
            Debug.Log($"{data.Key}, {data.ToString()}");
        }
    }

    // 데이터를 세이브하고 로드하는 로직을 작성할때는 방어적으로 코드를 작성하는것이 맞습니다.
    private void Save()
    {
        JObject data = new JObject();

        foreach (IJsonDataModule userDataModule in UserDataModule)
        {
            if (userDataModule == null || string.IsNullOrEmpty(userDataModule.Key))
            {
                continue;
            }

            data[userDataModule.Key] = userDataModule.OnSave();
        }
        
        JsonWriter.Save(data, savePath);
        
        Debug.Log($"[UserDataManager] 사용자 데이터를 저장 했습니다. 경로 : {savePath}");
    }

    private void Load()
    {
        JObject data = JsonReader.Load<JObject>(savePath);

        foreach (IJsonDataModule userDataModule in UserDataModule)
        {
            if (userDataModule == null || string.IsNullOrEmpty(userDataModule.Key))
            {
                continue;
            }

            if (data.TryGetValue(userDataModule.Key, out JToken dataToken))
            {
                userDataModule.OnLoad(dataToken);
            }

            // userDataModule.Key
        }
        
        Debug.Log($"[UserDataManager] 사용자 데이터를 로드 했습니다. 경로 : {savePath}");
    }

    
}
