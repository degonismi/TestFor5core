using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private Vector3 _targetPos;
    
    public float Speed;
    public int ScoreCoef;
    public bool Def;
    private float DefTime;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetTarget(Input.mousePosition);
        }
    }


    public void SetTarget(Vector3 pos)
    {
        pos = Camera.main.ScreenToWorldPoint(pos);
        pos.z = 0;
        _targetPos = pos;
        StopAllCoroutines();
        Vector3 diff = pos - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (transform.position != _targetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPos,Speed * Time.deltaTime );
            yield return null;
        }
    }

    public IEnumerator DefBuff()
    {
        //float i = 1;
        while (DefTime>0)
        {
            yield return null;
            DefTime -= Time.deltaTime;
            
        }

        Def = false;
            //yield return null;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<EnemyMover>())
        {
            Debug.Log("as");
        }

        if (other.GetComponent<DefBuff>())
        {
            Destroy(other.gameObject);
            Def = true;
            DefTime = 1;
            StartCoroutine(DefBuff());
            
        }
    }
}
