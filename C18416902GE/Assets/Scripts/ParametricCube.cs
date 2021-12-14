using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParametricCube : MonoBehaviour
{
    public int _band;
    public float _startScale, _scaleMultiplier;
    public bool _useBuffer;
    Material _material;
    // Start is called before the first frame update
    void Start()
    {
        _material = GetComponent<MeshRenderer>().materials[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (_useBuffer)
        {
            transform.localScale = new Vector3(transform.localScale.x, (AudioPlayer._bandBuffer[_band] * _scaleMultiplier) + _startScale, transform.localScale.z);
            Color _color = new Color(AudioPlayer._audioBandBuffer[_band], AudioPlayer._audioBandBuffer[_band], AudioPlayer._audioBandBuffer[_band]);
            _material.SetColor("_EmissionColor", _color);
        }

        if (!_useBuffer)
        {
            transform.localScale = new Vector3(transform.localScale.x, (AudioPlayer._frequencyBands[_band] * _scaleMultiplier) + _startScale, transform.localScale.z);
            Color _color = new Color(AudioPlayer._audioBand[_band], AudioPlayer._audioBand[_band], AudioPlayer._audioBand[_band]);
            _material.SetColor("_EmissionColor", _color);
        }
    }
}
