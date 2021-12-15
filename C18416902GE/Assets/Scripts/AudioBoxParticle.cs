using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBoxParticle : MonoBehaviour
{
    public float _moveSpeed;
    public int _audioBand;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += transform.forward * _moveSpeed * Time.deltaTime;
    }

    public void ApplyRotation(Vector3 _rotation, float _rotateSpeed)
    {
        Quaternion _targetRotation = Quaternion.LookRotation(_rotation.normalized);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, _rotateSpeed * Time.deltaTime);
    }
}
