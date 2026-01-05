using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using UnityEngine;

public static class TSVLoader
{
    private static readonly CsvConfiguration TsvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
    {
        Delimiter = "\t",
        Mode = CsvMode.NoEscape,
        HasHeaderRecord = true,
        MissingFieldFound = null,
        HeaderValidated = null,
    };

    /// <summary>
    /// Application.persistentDataPath/Table 폴더에서 주어진 테이블 이름의 TSV 파일을 읽어 List<T>로 반환합니다.
    /// </summary>
    /// <typeparam name="T"> 매핑할 클래스 타입 (public getter/setter 필수)</typeparam>
    /// <param name="tableName"> 파일 이름 (확장자 제외)</param>
    /// <returns> 파싱된 데이터 리스트</returns>
    public static async Task<List<T>> LoadTableAsync<T>(string tableName, bool isStreamingAssetPath = false)
    {
        string basePath = isStreamingAssetPath ? Application.streamingAssetsPath : Application.persistentDataPath;
        string folderPath = Path.Combine(basePath, "Table");
        string filePath = Path.Combine(folderPath, tableName + ".tsv");

        if (File.Exists(filePath) == false)
        {
            Debug.LogError($"[TableLoader] 파일이 존재하지 않습니다: {filePath}");
            return null;
        }

        try
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, TsvConfig);

            var records = new List<T>();
            await foreach (var record in csv.GetRecordsAsync<T>())
            {
                records.Add(record);
            }

            return records;
        }
        catch (Exception ex)
        {
            Debug.LogError($"[TableLoader] {tableName}.tsv 로딩 실패: {ex.Message}");
            return null;
        }
    }
}