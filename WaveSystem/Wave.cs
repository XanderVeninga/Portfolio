using UnityEditor;
using UnityEngine;

[System.Serializable]
public class Wave
{
    [Header("Wave Info")]
    public int waveNumber;
    public int waveReward;
    [Header("Boss Stuff")]
    public bool bossWave;
    public int bossAmmount;
    [Header("Enemies in This Wave")]
    public Enemy[] enemies;
}