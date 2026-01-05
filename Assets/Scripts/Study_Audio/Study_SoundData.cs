using UnityEngine;

public class Study_SoundData : MonoBehaviour
{
    [SerializeField] private AudioSource bgm;
    [SerializeField] private Transform ball;

    [SerializeField] private float scaleFactor = 1.0f;
    
    [Range(0.0f, 1.0f)]
    [SerializeField] private float damping = 0.5f;
    
    private float[] spectrumBuffer = new float[512];
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private float goalScale = 0.0f;
    
    // Update is called once per frame
    void Update()
    {
        bgm.GetSpectrumData(spectrumBuffer, 0, FFTWindow.Blackman);

        // 저음역대의 데이터의 평균치로 ball 트랜스폼의 스케일을 바꿔봅시다

        int startIdx = 0;
        int endIdx = 64;

        float sum = 0;

        for (int i = startIdx; i < endIdx; i++)
        {
            sum += spectrumBuffer[i];
        }
            
        goalScale = sum / (endIdx - startIdx);
        
        Vector3 goalScaleVector = Vector3.one * goalScale * scaleFactor;

        ball.localScale = Vector3.Lerp(ball.localScale, goalScaleVector, damping);

    }
}
