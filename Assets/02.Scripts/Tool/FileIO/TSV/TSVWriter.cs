using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using UnityEngine;

namespace Jay
{
    public static class TSVWriter
    {
        private static readonly CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = "\t",
            HasHeaderRecord = true
        };

        /// <summary>
        /// 지정된 경로에 TSV 파일로 데이터를 저장합니다.
        /// </summary>
        /// <typeparam name="T">레코드 타입</typeparam>
        /// <param name="records">저장할 데이터 목록</param>
        /// <param name="filePath">파일 저장 경로</param>
        public static void SaveTable<T>(List<T> records, string filePath)
        {
            try
            {
                var directory = Path.GetDirectoryName(filePath);
                if (string.IsNullOrEmpty(directory) == false && Directory.Exists(directory) == false)
                {
                    Directory.CreateDirectory(directory);
                }

                using var writer = new StreamWriter(filePath);
                using var csv = new CsvWriter(writer, config);

                csv.WriteRecords(records);
                Debug.Log($"TSV 저장 완료: {filePath}");
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"TSVWriter 저장 실패: {filePath}\n{ex}");
            }
        }
    }

}