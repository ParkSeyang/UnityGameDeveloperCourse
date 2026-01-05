using System.IO;
using UnityEngine;
using UnityEditor;

public class HeightmapGenerator : EditorWindow
{
    private int resolution = 513; // 유니티 터레인 권장 해상도 (2^n + 1)
    private string fileName = "SampleHeightmap.raw";

    [MenuItem("Tools/KooHoo/Generate Sample Heightmap")]
    public static void ShowWindow() => GetWindow<HeightmapGenerator>("Heightmap Gen");

    private void OnGUI()
    {
        resolution = EditorGUILayout.IntField("Resolution", resolution);
        fileName = EditorGUILayout.TextField("File Name", fileName);

        if (GUILayout.Button("Generate RAW File"))
        {
            GenerateRawFile();
        }
    }

    private void GenerateRawFile()
    {
        string path = Path.Combine(Application.streamingAssetsPath, fileName);
        int size = resolution * resolution;
        byte[] rawData = new byte[size * 2]; // 16-bit (2 bytes per pixel)

        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                // Perlin Noise를 이용한 샘플 지형 데이터 생성
                float xCoord = (float)x / resolution * 5.0f;
                float yCoord = (float)y / resolution * 5.0f;
                float sample = Mathf.PerlinNoise(xCoord, yCoord);

                // 0.0 ~ 1.0의 값을 16비트 정수(0 ~ 65535)로 변환
                ushort heightValue = (ushort)(sample * ushort.MaxValue);

                // Little Endian 방식으로 바이트 배열에 저장
                int index = (y * resolution + x) * 2;
                rawData[index] = (byte)(heightValue & 0xFF);
                rawData[index + 1] = (byte)((heightValue >> 8) & 0xFF);
            }
        }

        File.WriteAllBytes(path, rawData);
        AssetDatabase.Refresh();
        Debug.Log($"[HeightmapGenerator] 생성 완료: {path}");
    }
}