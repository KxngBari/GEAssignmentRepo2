using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NoiseAudioBox))]

public class AudioBoxSound : MonoBehaviour
{
    NoiseAudioBox _noiseAudioBox;
    public AudioPlayer _audioPlayer;
    [Header("Speed")]
    public bool _useSpeed;

    void Start()
    {
        _noiseAudioBox = GetComponent<NoiseAudioBox>();
        int _countBand = 0;
        for (int i = 0; i < _noiseAudioBox._numberOfParticles; i++)
        {
            //the audio is split across 8 frequency bands
            int band = _countBand % 8;
            _noiseAudioBox._particles[i]._audioBand = band;
            _countBand++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
