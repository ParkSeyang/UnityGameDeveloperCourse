using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Study_PlanetSystem : MonoBehaviour
{
    public GameObject[] PlanetPrefabs;
    public Transform EarthTransform;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnPlanet();
        }
    }

    private void SpawnPlanet()
    {
        var trans = new GameObject("new Planet Axis").transform;
        trans.SetParent(EarthTransform);
        
        Vector3[] axisRange = {Vector3.up, Vector3.right, Vector3.forward};
        Vector3 randAxis = axisRange[Random.Range(0, axisRange.Length)];
        float randDistance = Random.Range(1.5f, 5);
        float randScale = Random.Range(0.1f, 0.5f);
      
        // 1. PlanetPrefabs 배열에서 임의의 개체 하나를 선택
        // 2. Instantiate() 함수를 이용해서 새 인스턴스(이하 새 행성)를 할당.
        // 3. 새 행성의 transform.parent를 위의 trans로 설정
        // 4. 새 행성의 localPosition에 randAxis * randDistance을 대입 
        // 5. 새 행성의 localScale에 randScale을 대입  

        GameObject target = PlanetPrefabs[Random.Range(0, PlanetPrefabs.Length)];
        GameObject instance = Instantiate(target, trans);
        instance.transform.localPosition = randAxis * randDistance;
        instance.transform.localScale = Vector3.one * randScale;
        
        // 6. trans 동적으로 Study_Euler 클래스를 주입 후 반환.
        // 7. 인스턴스(Study_Euler)의 회전 정보(RotationInfo)를 랜덤으로 설정해주기

        var rotator = trans.gameObject.AddComponent<Study_Euler>();
        rotator.rotationInfo.Speed = Random.Range(30f, 90f);
        rotator.rotationInfo.Axis = randAxis;
        rotator.rotationInfo.IsLocal = true;

    }
}