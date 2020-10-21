using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SpawnPoint
{
    [Range(0.0f, 1.0f)]
    public float x;
    [Range(0.0f, 1.0f)]
    public float y;
}

[System.Serializable]
public struct EnemyPath
{
    [SerializeField] public SpawnPoint[] points;
}

[CreateAssetMenu(fileName = "GameSettings", menuName = "Game Settings/Game", order = 1)]
public class GameSettings : ScriptableObject
{
    public float shieldTime;

    public float gunPower;
    public float gunCooldownTime;

    public int astroidsStartCount;
    public int addedAstroidsPerLevel;

    public SpawnPoint[] spawnPositions;
    public EnemyPath[] enemyPaths;
    public float spawnEnemyShipTimer;

    public int[] astroidScores;
    public int enemyShipScore;
}
