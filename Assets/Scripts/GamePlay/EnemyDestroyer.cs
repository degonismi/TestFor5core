using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroyer : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        Debug.Log("1");
        Destroy(gameObject);
    }
}
