using System;
using System.Collections.Generic;
using System.IO;
using Jay;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class Study_TSV : MonoBehaviour 
{
    /* TSV란?
     *  탭(Tab, \t)을 구분자로 사용하여 데이터를 저장하는 텍스트 포맷.
     * 주로 기획데이터(각종 테이블), 다국어 텍스트(Localization, StringTable)
     * 등을 관리할때 CSV보다 훨씬 안정적이고(테이블에서 ','를 사용가능함),
     * 파싱이 용이하여 사용함. (, 이면 CSV, 탭이면 TSV)
     *
     * 특징
     * - 행(Row)와 열(Column)로 구분되어 구조가 단순하다.
     * - 거의 모든 관계형DB들과 별도의 처리 없이 호환가능하다.
     * - 닷넷 기준으로 CSVHelper.dll 을 이용해서 많이 사용함.
     * - 제공해드린 TSV Reader, Writer는 내부의 프로퍼티로만 자동 지원 됩니다.
     */
    
    private string savePath;
    
    [System.Serializable]
    public class SampleData
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Exp { get; set; }
        
        public SampleData SetRandom()
        {
            Name = Guid.NewGuid().ToString();
            Level = Random.Range(0, 101);
            Exp = Random.Range(0, 101);
            return this;
        }

    }


    private void Start()
    {
        savePath = Path.Combine(Application.persistentDataPath, "Study_TSV.tsv");
        Save();
        Load();
    }

    private void Save()
    {
        List<SampleData> list = new List<SampleData>();
        list.Add(new SampleData().SetRandom());
        list.Add(new SampleData().SetRandom());
        list.Add(new SampleData().SetRandom());
        
        TSVWriter.SaveTable(list, savePath);

    }

    private void Load()
    {
        List<SampleData> list;
        list = TSVReader.ReadTable<SampleData>(savePath);

        foreach (var data in list)
        {
            Debug.Log(data.Name);
        }
    }

}
