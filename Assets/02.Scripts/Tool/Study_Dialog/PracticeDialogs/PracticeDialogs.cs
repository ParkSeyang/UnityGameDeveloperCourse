using System;
using System.IO;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class PracticeDialogs : MonoBehaviour
{
    public class UserDialogData
    {
        public string Category { get; set; }
        public string Key { get; set; }
        public string Kor { get; set; }
    }

    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text contentText;
    
    private string[] studyCategory = new[] { "민수와 지원", "레온과 루나" };
    private List<UserDialogData> userDialogs;
    
    private void Awake()
    {
        string filePath2 = Path.Combine(Application.streamingAssetsPath, "Dialog_Study.tsv");
        userDialogs = TSVReader.ReadTable<UserDialogData>(filePath2);
        
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
          
        }
    }

    private UserDialogData[] GetDialogsOrNull(string key)
    {
        // return allDialogs.Where(dialog => dialog.Key.Equals(key)).ToArray();
        return userDialogs.Where(dialog => dialog.Key.Equals(key)).ToArray();
        
        
    }


   //private IEnumerator PlayDialogs(UserDialogData[] dialogs)
   //{
   //    for (int i = 0; i < dialogs.Length; i++)
   //    {
   //        
   //    }
   //}

   //private IEnumerator PlayText(UserDialogData dialogs)
   //{
   //    
   //}


}
