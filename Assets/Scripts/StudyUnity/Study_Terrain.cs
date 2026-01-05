using UnityEngine;

public class Study_Terrain : MonoBehaviour
{
    [SerializeField] private Terrain currentTerrain;

    [SerializeField] private Transform cube;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float height = currentTerrain.SampleHeight(transform.position);
        cube.transform.position = 
            new Vector3(transform.position.x, height, transform.position.z);
    }
}
