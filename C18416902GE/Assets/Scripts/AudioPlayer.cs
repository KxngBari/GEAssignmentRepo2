using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]

public class AudioPlayer : MonoBehaviour
{
    AudioSource _audioSource;
    public static float[] _samples = new float[512];
    public static float[] _frequencyBands = new float[8];
    public static float[] _bandBuffer = new float[8];
    float[] _bufferDecrease = new float[8];

    float[] _frequencyBandHighest = new float[8];
    public static float[] _audioBand = new float[8];
    public static float[] _audioBandBuffer = new float[8];
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource();
        CreateFreqBands();
        BandBuffer();
        CreateAudioBands();
    }

    void GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }

    void CreateAudioBands()
    {
        for (int i = 0; i < 8; i++)
        {
            if (_frequencyBands[i] > _frequencyBandHighest[i])
            {
                _frequencyBandHighest[i] = _frequencyBands[i];
            }
            _audioBand[i] = (_frequencyBands[i] / _frequencyBandHighest[i]);
            _audioBandBuffer[i] = (_bandBuffer[i] / _frequencyBandHighest[i]);
        }
    }

    void BandBuffer()
    {
        for (int g = 0; g < 8; ++g)
        {
            if (_frequencyBands[g] > _bandBuffer[g])
            {
                _bandBuffer[g] = _frequencyBands[g];
                _bufferDecrease[g] = 0.005f;
            }

            if (_frequencyBands[g] < _bandBuffer[g])
            {
                _bandBuffer[g] -= _bufferDecrease[g];
                _bufferDecrease[g] = _bufferDecrease[g] * 1.2f;
            }
        }
    }

    void CreateFreqBands()
    {
        //44.1khz / 512samples = 86hz per sample

        int count = 0;

        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int) Mathf.Pow(2, i) * 2;

            if (i == 7)
            {
                sampleCount += 2;
            }
            for (int j = 0; j < sampleCount; j++)
            {
                average += _samples[count] * (count + 1);
                    count++;
            }

            average /= count;

            _frequencyBands[i] = average * 10;
        }
    }
}
