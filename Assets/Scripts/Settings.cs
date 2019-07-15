using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings", fileName = "Settings")]
public class Settings : ScriptableObject
{
    
    
    [Header("Скорость персонажа")] public float Speed;
    [Header("Задержка получения очков")] public float TimeToScore;
    [Header("Колличество очков")] public int Score;

    
    
    [Header("Колличесто врагов")] [Space(50)] public int EnemyCount;
    [Header("Скорость врагов")] public float EnemySpeed;
    [Header("Задержка спавна врагов")] public float SpawnDelay;
    [Header("Задержка роста скорости врагов")] public float AddSpeedDelay;
    [Header("Коэфициент роста скорости врагов")] public float SpeedCoef;

    
    
}
