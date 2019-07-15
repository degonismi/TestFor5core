using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[Serializable]
public class GamePlayController : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private Settings _settings;
    private Transform _playerTransform;
    private PlayerMover _playerMover;
    private int Score;
    
    
    [SerializeField] private float _timeToBuffSpawn;
    [SerializeField] private GameObject[] _buffs;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _timeText;
    
    private float _enemySpeed;
    private float _startTime;
    private int _timer;
    
    private void OnEnable()
    {
        StartGames();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void StartGames()
    {
        GameObject player = Instantiate(_player);
        _playerTransform = player.transform;
        _playerMover = player.GetComponent<PlayerMover>();
        _playerMover.Speed = _settings.Speed;
        _playerMover.ScoreCoef = 1;
        _playerMover.Def = false;
        _enemySpeed = _settings.EnemySpeed;
        StartCoroutine(SpawnEnemy());
        StartCoroutine(AddEnemySpeed());
        StartCoroutine(SpawnBuff());
        StartCoroutine(AddScore());
        _startTime = 0;
    }

    private void Update()
    {
        _startTime += Time.deltaTime;
        _timer = (int)(_startTime);
        _timeText.text = _timer/60+ ":" + _timer%60;
    }

    private Vector2 GetRandomPos()
    {
        Vector2 pos = new Vector2(Random.Range(-5f,5f), Random.Range(-5,5));
        while ((Vector2.Distance(_playerTransform.position, pos) < 3))
        {
            pos = new Vector2(Random.Range(-5f,5f), Random.Range(-5,5));
        }
        return pos;
    }

    private IEnumerator AddScore()
    {
        while (true)
        {
            yield return new WaitForSeconds(_settings.TimeToScore);
            Score += (_settings.Score * _playerMover.ScoreCoef);
            _scoreText.text = Score.ToString();
        }
    }
    
    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(_settings.SpawnDelay);
            int i = _settings.EnemyCount;
            while (i>0)
            {
                GameObject e = Instantiate(_enemy,GetRandomPos(), Quaternion.identity);
                e.GetComponent<EnemyMover>().Speed = _enemySpeed;
                i--;
            }
        }
    }

    private Vector2 GetBuffPos()
    {
        Vector2 pos = new Vector2(Random.Range(5f,5f), Random.Range(-5,5));
        while (Vector2.Distance(_playerTransform.position, pos) <1 || Vector2.Distance(_playerTransform.position, pos) >3)
        {
            pos = new Vector2(Random.Range(-3f,3f), Random.Range(-5,5));
        }

        return pos;
    }
    
    private IEnumerator SpawnBuff()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeToBuffSpawn);
            Instantiate(_buffs[Random.Range(0, 3)], GetBuffPos(), Quaternion.identity);
        }
    }
    
    private IEnumerator AddEnemySpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(_settings.AddSpeedDelay);
            _enemySpeed *= _settings.SpeedCoef;
        }
    }



}
