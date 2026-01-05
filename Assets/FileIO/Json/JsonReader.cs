using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public static class JsonReader
{
    /// <summary>
    /// 지정된 경로의 Json 파일을 읽어 T 타입으로 역직렬화합니다.
    /// </summary>
    public static T Load<T>(string filePath)
    {
        string fullPath = filePath.EndsWith(".json") ? filePath : $"{filePath}.json";

        if (File.Exists(fullPath) == false)
        {
            Debug.LogWarning($"[JsonLoader] 파일이 존재하지 않음: {fullPath}");
            return default;
        }

        try
        {
            string json = File.ReadAllText(fullPath);
            T data = JsonConvert.DeserializeObject<T>(json);
            Debug.Log($"[JsonLoader] 로드 완료: {fullPath}");
            return data;
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"[JsonLoader] 로드 실패: {fullPath}\n{ex}");
            return default;
        }
    }
}