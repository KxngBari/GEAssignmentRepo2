using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseAudioBox : MonoBehaviour
{
    FastNoise _fastNoise;
    public Vector3Int _gridSize;
    public float _cellSize;
    public float _increment;
    public Vector3 _offset, _offsetSpeed;

    //particles
    public GameObject _particlePrefab;
    public int _numberOfParticles;
    [HideInInspector]
    public List<AudioBoxParticle> _particles;
    public List<MeshRenderer> _particleMeshRenderer;

    public float _particleScale, _particleMoveSpeed, _particleRotateSpeed;
    public float _spawnRadius;
    bool _particleSpawnChecker(Vector3 position)
    {
        bool valid = true;
        foreach (AudioBoxParticle particle in _particles)
        {
            if (Vector3.Distance(position, particle.transform.position) < _spawnRadius)
            {
                valid = false;
                break;
            }
        }
        if (valid)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //",," = 3-dimensional vector
    public Vector3[,,] _audioBoxDirection;
    void Awake()
    {
        //using brackets rather than parentheses because this is a multi-dimensional vector
        _audioBoxDirection = new Vector3[_gridSize.x, _gridSize.y, _gridSize.z];
        _fastNoise = new FastNoise();
        _particles = new List<AudioBoxParticle>();
        _particleMeshRenderer = new List<MeshRenderer>();

        for (int i = 0; i < _numberOfParticles; i++)
        {
            int attempt = 0;

            while(attempt < 100)
            {
                Vector3 randomPos = new Vector3(
                    Random.Range(this.transform.position.x, this.transform.position.x + _gridSize.x * _cellSize),
                    Random.Range(this.transform.position.y, this.transform.position.y + _gridSize.y * _cellSize),
                    Random.Range(this.transform.position.z, this.transform.position.z + _gridSize.z * _cellSize));
                bool isValid = _particleSpawnChecker(randomPos);

                if (isValid)
                {
                    GameObject _particleInstance = (GameObject)Instantiate(_particlePrefab);
                    _particleInstance.transform.position = randomPos;
                    _particleInstance.transform.parent = this.transform;
                    _particleInstance.transform.localScale = new Vector3(_particleScale, _particleScale, _particleScale);
                    _particles.Add(_particleInstance.GetComponent<AudioBoxParticle>());
                    _particleMeshRenderer.Add(_particleInstance.GetComponent<MeshRenderer>());
                    break;
                }
                if (!isValid)
                {
                    attempt++;
                }
            }
        }
        Debug.Log(_particles.Count);
    }

    void Update()
    {
        CalculateAudioBoxDirections();
        ParticleBehaviour();
    }

    void CalculateAudioBoxDirections()
    {
        _offset = new Vector3(_offset.x + (_offsetSpeed.x * Time.deltaTime), _offset.y + (_offsetSpeed.y * Time.deltaTime), _offset.z + (_offsetSpeed.z * Time.deltaTime));
        float xOff = 0f;
        for (int x = 0; x < _gridSize.x; x++)
        {
            float yOff = 0f;
            for (int y = 0; y < _gridSize.y; y++)
            {
                float zOff = 0f;
                for (int z = 0; z < _gridSize.z; z++)
                {
                    float noise = _fastNoise.GetSimplex(xOff + _offset.x, yOff + _offset.y, zOff + _offset.z) + 1;
                    Vector3 noiseDirection = new Vector3(Mathf.Cos(noise * Mathf.PI), Mathf.Sin(noise * Mathf.PI), Mathf.Cos(noise * Mathf.PI));
                    _audioBoxDirection[x, y, z] = Vector3.Normalize(noiseDirection);

                    zOff = zOff + _increment;
                }
                yOff = yOff + _increment;
            }
            xOff = xOff + _increment;
        }
    }

    void ParticleBehaviour()
    {
        foreach(AudioBoxParticle p in _particles)
        {
            // check x edges of particles
            if (p.transform.position.x > this.transform.position.x + (_gridSize.x * _cellSize))
            {
                p.transform.position = new Vector3(this.transform.position.x, p.transform.position.y, p.transform.position.z);
            }
            if (p.transform.position.x < this.transform.position.x)
            {
                p.transform.position = new Vector3(this.transform.position.x + (_gridSize.x * _cellSize), p.transform.position.y, p.transform.position.z);
            }
            // check y edges of particles
            if (p.transform.position.y > this.transform.position.y + (_gridSize.y * _cellSize))
            {
                p.transform.position = new Vector3(p.transform.position.x, this.transform.position.y, p.transform.position.z);
            }
            if (p.transform.position.y < this.transform.position.y)
            {
                p.transform.position = new Vector3(p.transform.position.x, this.transform.position.y + (_gridSize.y * _cellSize), p.transform.position.z);
            }
            // check z edges of particles
            if (p.transform.position.z > this.transform.position.z + (_gridSize.z * _cellSize))
            {
                p.transform.position = new Vector3(p.transform.position.x, p.transform.position.y, this.transform.position.z);
            }
            if (p.transform.position.z < this.transform.position.z)
            {
                p.transform.position = new Vector3(p.transform.position.x, p.transform.position.y, this.transform.position.z + (_gridSize.z * _cellSize));
            }

            Vector3Int _particlePos = new Vector3Int(
                Mathf.FloorToInt(Mathf.Clamp((p.transform.position.x - this.transform.position.x) / _cellSize, 0, _gridSize.x - 1)),
                Mathf.FloorToInt(Mathf.Clamp((p.transform.position.y - this.transform.position.y) / _cellSize, 0, _gridSize.y - 1)),
                Mathf.FloorToInt(Mathf.Clamp((p.transform.position.z - this.transform.position.z) / _cellSize, 0, _gridSize.z - 1))
                );
            p.ApplyRotation(_audioBoxDirection[_particlePos.x, _particlePos.y, _particlePos.z], _particleRotateSpeed);
            p._moveSpeed = _particleMoveSpeed;
            //p.transform.localScale = new Vector3(_particleScale, _particleScale, _particleScale);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(this.transform.position + new Vector3((_gridSize.x * _cellSize) * 0.5f, (_gridSize.y * _cellSize) * 0.5f, (_gridSize.z * _cellSize) * 0.5f),
            new Vector3(_gridSize.x * _cellSize, _gridSize.y * _cellSize, _gridSize.z * _cellSize));
    }
}
