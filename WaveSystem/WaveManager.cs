using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class WaveManager : MonoBehaviour
{
    [Header("References")]
    public GameManager gameManager;
    public EconomyManager economyManager;
    PlayerInputManager inputManager;
    public enum SpawnState { SPAWNING, WAITING, COUNTING, FINISHED, BOSS };
    [Header("Enemy Arrays")]
    public Enemy[] halloween;
    public Enemy[] christmas;
    public Boss boss;
    public Enemy errorEnemy;
    [Header("Spawn Locations")]
    public Transform[] naturalSpawns;
    [Header("Wave Info")]
    public SpawnState state = SpawnState.COUNTING;
    public static int currentWave = 0;
    public float timeBetweenWaves = 5f;
    public float nextWaveCountdown;
    public Wave[] waves;
    public int nextWave = 0;



    void Start()
    {
        inputManager = GameObject.Find("PlayerManager").GetComponent<PlayerInputManager>();
        nextWaveCountdown = timeBetweenWaves;
    }
    private void Update()
    {
        if (inputManager.playerCount == 2)
        {
            if (state == SpawnState.WAITING)
            {
                if (!EnemyIsAlive())
                {
                    WaveCompleted(waves[currentWave]);
                }
                else
                {
                    return;
                }
            }
            if (nextWaveCountdown <= 0 && state != SpawnState.FINISHED)
            {
                if (state != SpawnState.SPAWNING)
                {
                    StartCoroutine(SpawnWave(waves[nextWave]));
                }
            }
            else
            {
                if (state == SpawnState.COUNTING && inputManager.playerCount == 2)
                {
                    nextWaveCountdown -= Time.deltaTime;
                }
            }
        }

    }

    void WaveCompleted(Wave _wave)
    {
        //Debug.Log("Wave Completed");
        if (_wave.bossWave)
        {
            state = SpawnState.BOSS;
            int amount = _wave.bossAmmount;
            SpawnBoss(_wave, amount);
        }


        state = SpawnState.COUNTING;

        if (nextWave + 1 == waves.Length)
        {
            //Debug.Log("No more Waves to Spawn");
            state = SpawnState.FINISHED;
        }
        else
        {
            economyManager.ecoP1 += _wave.waveReward;
            economyManager.ecoP2 += _wave.waveReward;
            //Debug.Log("eco p1: " + economyManager.ecoP1);
            //Debug.Log("eco p2: " + economyManager.ecoP2);
            nextWaveCountdown = timeBetweenWaves;
            nextWave++;
        }
    }

    bool EnemyIsAlive()
    {
        timeBetweenWaves -= Time.deltaTime;

        if (nextWaveCountdown <= 0)
        {
            nextWaveCountdown = timeBetweenWaves;
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                return false;
            }
        }
        return true;
    }
    IEnumerator SpawnWave(Wave _wave)
    {
        currentWave = _wave.waveNumber;
        //Debug.Log("Spawning Wave : " + currentWave);
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.enemies.Length; i++)
        {
            int ammountSpawned = 0;
            //Debug.Log("Enemy number: " + (i + 1));
            for (ammountSpawned = 0; ammountSpawned < _wave.enemies[i].ammount; ammountSpawned++)
            {
                //Debug.Log("ammount of enemies spawned: " + (ammountSpawned + 1));
                SpawnEnemy(_wave.enemies[i].prefab, 1, 0);
                yield return new WaitForSeconds(1f / _wave.enemies[i].spawnRate);
            }

        }

        state = SpawnState.WAITING;
        yield break;
    }

    public void StopSpawnWave()
    {
        StopCoroutine(SpawnWave(waves[nextWave]));
    }

    public void StartSpawnWave()
    {
        StartCoroutine(SpawnWave(waves[nextWave]));
    }

    public void SpawnBoss(Wave _wave, int _amount)
    {
        for(int a = 0; a < _amount; a++)
        {
            for (int s = 0; s < naturalSpawns.Length; s++)
            {
                GameObject Obj = Instantiate(boss.bossPrefab, naturalSpawns[s].position, Quaternion.identity);
                Obj.GetComponent<EnemyModelManager>().SetModel(s);
            }
        }        
    }

    public void SpawnEnemy(GameObject enemyPrefab, int _ammount, int _playerID)
    {
        for (int s = 0; s < naturalSpawns.Length; s++)
        {
            GameObject Obj = Instantiate(enemyPrefab, naturalSpawns[s].position, Quaternion.identity);
            Obj.GetComponent<EnemyModelManager>().SetModel(s);
        }
    }
}