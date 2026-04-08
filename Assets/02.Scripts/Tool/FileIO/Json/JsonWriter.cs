using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public static class JsonWriter
{
    /// <summary>
    /// 데이터를 Json으로 저장합니다. 파일명은 자동으로 .json 확장자가 붙습니다.
    /// </summary>
    public static void Save<T>(T data, string filePath, bool prettyPrint = true)
    {
        string fullPath = filePath.EndsWith(".json") ? filePath : $"{filePath}.json";

        try
        {
            var directory = Path.GetDirectoryName(fullPath);
            if (string.IsNullOrEmpty(directory) == false && Directory.Exists(directory) == false)
            {
                Directory.CreateDirectory(directory);
            }
                
            var formatting = prettyPrint ? Formatting.Indented : Formatting.None;
            string json = JsonConvert.SerializeObject(data, formatting);
            File.WriteAllText(fullPath, json);

            Debug.Log($"[JsonWriter] 저장 완료: {fullPath}");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"[JsonWriter] 저장 실패: {fullPath}\n{ex}");
        }
    }
}