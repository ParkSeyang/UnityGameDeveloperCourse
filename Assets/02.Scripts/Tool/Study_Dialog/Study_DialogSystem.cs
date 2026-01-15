using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections;

public class Study_DialogSystem : MonoBehaviour
{
    public class DialogData
    {
        public string Category { get; set; }
        public string Key { get; set; }
        public string Kor { get; set; }
    }

    [SerializeField] private TMP_Text nameText; // 현재 구조에서는 카테고리 값이 들어갑니다.
    [SerializeField] private TMP_Text contentText; // 현재 구조에서는 Kor

    // 캐릭터 이름이나 화자의 고유 key값을 사용하지만, study 용도로 임의의 값을 key로 사용합니다,
    private string[] category = new[] { "방어적 프로그래밍", "멀티 쓰레드", "문서화" };
    
    private List<DialogData> allDialogs;
    
    
    void Awake()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "DialogTable.tsv");
        allDialogs = TSVReader.ReadTable<DialogData>(filePath);
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //
            int randomIndex = Random.Range(0, category.Length);
            // 해당 카테고리 키값의 dialogs data를 추출하고
            DialogData[] dialogs = GetDialogsOrNull(category[randomIndex]);
            
            StartCoroutine(PlayDialog(dialogs));
        }
        
    }

    // 메모리에 있는 데이터를 검색해서 key값을 만족하는 DialogData를 찾아서 반환합니다.
    private DialogData[] GetDialogsOrNull(string key)
    {
        // Linq란?
        // LINQ(Language - Intergrated Query) 다양한 데이터, 여러 묶음의
        // 목록화된 데이터를 조작하기 위해 사용하는 C#의 질의어 확장 집합입니다.
        // 전체적으로 DB를 사용하는 방식과 유사합니다.
        // 어떠한 객체, 컬렉션, 데이터베이스, XML 등의 다양한 데이터 집합들을
        // 쿼리하여 원하는 데이터만 추출해낼 수 있습니다.
        // + 정렬도 가능하다.
        
        // 생각외로 가비지가 많이 발생합니다. 따라서 자주쓰면 안됩니다.
        
        // 1번 예시
        var searchList = 
            from dialog in allDialogs 
            where dialog.Category.Equals(key)
            select dialog;

        return searchList.ToArray();
        
        // 아래도 같은 Linq입니다.(함수형 & 람다식 사용) 실제로는 이방식을 많이 사용합니다.
        // 2번 예시
        return allDialogs.Where(dialog => dialog.Key.Equals(key)).ToArray();
        
        // 3번 예시
        { // 위의 Linq와 구문은 아래의 내용과 동일합니다,
            List<DialogData> selectList = new List<DialogData>();
            for (int i = 0; i < allDialogs.Count; i++)
            {
                if (allDialogs[i].Kor.Equals(key))
                {
                    selectList.Add(allDialogs[i]);
                }
            }

            return selectList.ToArray();
        }

    }

    private IEnumerator PlayDialog(DialogData[] dialogs)
    {
        for (int i = 0; i < dialogs.Length; i++)
        {
            nameText.SetText(dialogs[i].Category);
            yield return StartCoroutine(PlayText(dialogs[i]));
        }
    }
    

    private IEnumerator PlayText(DialogData dialogData)
    {
        
        contentText.SetText(dialogData.Kor);
        contentText.maxVisibleCharacters = 0;
        
        for (int i = 0; i < dialogData.Kor.Length; i++)
        {
            contentText.maxVisibleCharacters += 1;
            yield return new WaitForSeconds(0.05f);
        }

        
        yield return new WaitForSeconds(3.0f);
        
    }


}
