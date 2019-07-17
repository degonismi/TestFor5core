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
    public bool Spd;
    public bool Scr;
    public float BuffTime;

    private Coroutine _move;
    private Coroutine _def;
    private Coroutine _score;
    private Coroutine _speed;

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
        
        if(_move!=null)
        StopCoroutine(_move);
        
        Vector3 diff = pos - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        _move = StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (transform.position != _targetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPos,Speed * Time.deltaTime );
            yield return null;
        }
    }

    public IEnumerator DefBuff(float i)
    {
        yield return new WaitForSeconds(i);
        Def = false;
    }

    public IEnumerator SpeedBuff(float i)
    {
        yield return new WaitForSeconds(i);
        Spd = false;
        Speed /= 3;
    }

    public IEnumerator ScoreBuff(float i)
    {
        yield return new WaitForSeconds(i);
        ScoreCoef = 1;
        Scr = false;
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<EnemyMover>())
        {
            if (Def)
            {
                Destroy(other.gameObject);
            }
            else
            {
                Destroy(gameObject);
                
            }
        }

        if (other.GetComponent<DefBuff>())
        {
            if (_def != null)
            {
                _def = null;
            }
            Destroy(other.gameObject); 
            Def = true;
            _def = StartCoroutine(DefBuff(BuffTime));
        }
        
        if (other.GetComponent<SpeedBuff>())
        {
            if (_speed != null)
            {
                _speed  = null;
            }
            Destroy(other.gameObject); 
            Spd = true;
            Speed *= 3;
            _speed  = StartCoroutine(SpeedBuff(BuffTime));
        }
        
        if (other.GetComponent<ScoreBuff>())
        {
            if (_score != null)
            {
                _score = null;
            }
            Destroy(other.gameObject);
            ScoreCoef = 10;
            Scr = true;
            _score = StartCoroutine(ScoreBuff(BuffTime));
        }
        
        
    }
}
