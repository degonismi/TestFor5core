using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    private Transform _target;
    public float Speed;
    
    private void Start()
    {
        _target = FindObjectOfType<PlayerMover>().transform;
    }

    private void Update()
    {
        if(_target != null)
        transform.position = Vector3.MoveTowards(transform.position, _target.position, Speed * Time.deltaTime);
    }
}
