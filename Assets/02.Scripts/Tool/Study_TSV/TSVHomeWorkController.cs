using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Jay;
using Random = UnityEngine.Random;

public class TSVHomeWorkController : MonoBehaviour
{
    [SerializeField] private GameObject SampleCube;
    
    private Camera mainCamera;
    private string saveCubePosPath;
    private string saveCubeRotPath;
    
    private List<CubePosition> cubeposList = new List<CubePosition>();
    private List<CubeRotation> cubeRotList = new List<CubeRotation>();
   // public Dictionary<Vector3, Quaternion> cubeposDic = new Dictionary<Vector3, Quaternion>();
    
   [Serializable]
   public class CubePosition
   {
       public float PosX { get; set; }
       public float PosY { get; set; }
       public float PosZ { get; set; }
       
       // ??? 이걸만드니까 인식을했다 하단에 생성자가있는데 이유는모르겠다.
       // 기본생성자를 만들어봤더니 가능해졌다 TSV의 동작원리를 제대로 익혀야겠다.
       public CubePosition() { }
       
       public CubePosition(Vector3 position)
       {
           PosX = position.x;
           PosY = position.y;
           PosZ = position.z;
       }
       
   }
   [Serializable]
   public class CubeRotation
   {
       public float RotX { get; set; }
       public float RotY { get; set; }
       public float RotZ { get; set; }
       
       public CubeRotation(Quaternion rotation)
       {
           RotX = rotation.eulerAngles.x;
           RotY = rotation.eulerAngles.y;
           RotZ = rotation.eulerAngles.z;
       }
   }
   
   private void Start()
    {
        mainCamera = Camera.main;
        saveCubePosPath = Path.Combine(Application.persistentDataPath, "CubePositions.tsv");
        saveCubeRotPath = Path.Combine(Application.persistentDataPath, "CubeRotations.tsv");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100, LayerMask.GetMask("Ground")))
            {
                var cube = Instantiate(SampleCube);
                cube.transform.position = hit.point;
                cube.transform.rotation =
                    Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
                cubeposList.Add(new CubePosition(cube.transform.position));
                cubeRotList.Add(new CubeRotation(cube.transform.rotation));
                cube.SetActive(true);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100, LayerMask.GetMask("Cube")))
            {
                Destroy(hit.collider.gameObject);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SaveTransform();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            LoadTransform();
            // 로드를했으니 그전에 담아둿던 위치정보를 담고있는 cubeposList를 비워준다.
            // 이렇게하면 지우고 다른것들을 추가로 만들고 세이브를할경우 그전에 저장된것과 합쳐지는것을 방지할수있었다.
            cubeposList.Clear();
        }
    }

     private void SaveTransform()
     {
         Debug.Log($"위치 저장됨 " + saveCubePosPath);
         Debug.Log($"회전 저장됨 " + saveCubeRotPath);
         
        TSVWriter.SaveTable(cubeposList, saveCubePosPath);
        TSVWriter.SaveTable(cubeRotList, saveCubeRotPath);
        
     }

     private void LoadTransform()
     {
         List<CubePosition> cubePosData;
         // List<CubeRotation> cubeRotData;
         // 이유는 모르겠지만 Vector3는 저장이되는데
         // 쿼터니엄을 TSV로 저장하면 
         // 스택오버플로우에러가 발생했다 이유를모르겠다;
        
         cubePosData = TSVReader.ReadTable<CubePosition>(saveCubePosPath); 
         // cubeRotData = TSVReader.ReadTable<CubeRotation>(saveCubeRotPath);
         
         foreach (var cubePosition in cubePosData)
         {
             var cube = Instantiate(SampleCube);
             Vector3 transformPos = new Vector3(cubePosition.PosX, cubePosition.PosY, cubePosition.PosZ);
             cube.transform.position = transformPos;
         }
         
     }
     

}
