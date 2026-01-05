using System.IO;
using UnityEngine;



public class SampleJsonIO : MonoBehaviour
{
    [System.Serializable]
    public class SampleData
    {
        public string Name;
        public int Level;
        public float HP;
    }
    
    private string savePath;

    private void Start()
    {
        savePath = Path.Combine(Application.persistentDataPath, "sample_json");

        // 샘플 데이터 생성
        var data = new SampleData
        {
            Name = "Jae",
            Level = 10,
            HP = 76.3f
        };

        // 저장
        JsonWriter.Save(data, savePath);

        // 로드
        var loaded = JsonReader.Load<SampleData>(savePath);

        // 출력
        if (loaded != null)
        {
            Debug.Log($"[Json Load Success] Name: {loaded.Name}, Level: {loaded.Level}, HP: {loaded.HP}");
        }
        else
        {
            Debug.LogWarning("[Json Load Failed]");
        }
    }
}