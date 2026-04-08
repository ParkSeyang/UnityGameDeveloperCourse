using UnityEngine;

public class TSVReaderSample : MonoBehaviour
{
    public class SampleData
    {
        public string Category { get; set; }
        public string Key { get; set; }
        public string Kor { get; set; }
    }
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Unity의 StreamingAssets 경로, PersistentDataPath 경로 검색해서 스터디 할것.
        //아래 두 내용 주석 해제하면 어떤건지 나옴
        
        //Application.streamingAssetsPath
        //Application.persistentDataPath
        
        
        TSVReader.ReadTableAsync<SampleData>("DialogTable", true).ContinueWith(
                (taskResult) =>
            {
                var list = taskResult.Result;

                foreach (var sampleData in list)
                {
                    Debug.Log($"Category : {sampleData.Category}, Key : {sampleData.Key}, Kor : {sampleData.Kor}");
                }
            }
        );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
