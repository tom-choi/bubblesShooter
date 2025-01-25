using UnityEditor.Rendering.Universal;
using UnityEngine;

public class PlayerMicInput : MonoBehaviour
{
    // 声明一个音频剪辑变量
    private AudioClip audioClip;
    // 定义一个常量，表示采样窗口的大小
    private const int sampleWindow = 128;

    void Start()
    {
        for (int i = 0; i < Microphone.devices.Length; i++)
        {
            // Debug.Log(Microphone.devices[i]);
        }
        // 獲取默認麥克風
        string mic = Microphone.devices[0];
        audioClip = Microphone.Start(mic, true, 10, 44100);
    }

    void Update()
    {
        
    }

    public bool isBlowing()
    {
        float loudness = GetAveragedVolume();
        return loudness > 0.01f;
    }

    float GetCurrentVolume()
    {
        float loudness = GetAveragedVolume();
        Debug.Log("當前音量：" + loudness);
        return loudness; // 返回當前音量
    }

    float GetAveragedVolume()
    {
        float[] data = new float[sampleWindow];
        int offset = Microphone.GetPosition(null) - sampleWindow + 1;
        if (offset < 0) return 0;

        audioClip.GetData(data, offset);
        float sum = 0;
        foreach (float sample in data)
        {
            sum += Mathf.Abs(sample);
        }
        return sum / sampleWindow;
    }
    
}
