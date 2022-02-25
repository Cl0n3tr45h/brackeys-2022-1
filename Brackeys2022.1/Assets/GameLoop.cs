using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public enum GameState
{
    FIGHT,
    LOOT,
    CRAFT
}
public class GameLoop : MonoBehaviour
{
    public List<GameObject> Levels;
    private List<GameObject> levelSequence = new List<GameObject>();
    private static int currentLevelIndex = 0;
    private GameObject currentLevel;
    public static int[] TargetKillCount = new []
    {
        5,
        6,
        7,
        10
    };
    private static int currentKillCount;
    public float[] SpawnRate;

    public GameObject[] Enemies;
    private static List<Transform> enemySpawnPoints = new List<Transform>();
    
    public GameObject Player;
    private GameState gameState;
    
    //Combo
    //

    private void Awake()
    {
        var random = new System.Random();
        levelSequence = Levels.OrderBy(item => random.Next()).ToList();
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnLevel();
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameState.FIGHT:
                FightBehaviour();
                break;
            case GameState.LOOT:
                LootBehaviour();
                break;
            case GameState.CRAFT:
                CraftBehaviour();
                break;
            default:
                FightBehaviour();
                break;
        }
    }

    public void FightBehaviour()
    {
        //Count down kill count
        //Combo stuff?
        //Points
        
    }

    public void LootBehaviour()
    {
        
    }

    public void CraftBehaviour()
    {
        
    }

    public void SpawnLevel()
    {
        currentLevel = Instantiate(levelSequence[currentLevelIndex], Vector3.zero, Quaternion.identity);
        //Get player spawn
        Player.transform.SetPositionAndRotation(GameObject.Find("PlayerSpawn").transform.position, Quaternion.identity);
        
        //Get enemy spawn points 
        var parent = GameObject.Find("EnemySpawns");
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            enemySpawnPoints.Add(parent.transform.GetChild(i));
        }

        //spawn enemy at every point
        
        foreach (var location in enemySpawnPoints)
        {
            var enemyToSpawn = 0;
            switch (currentLevelIndex)
            {
                /*case 0:
                    enemyToSpawn = 0;
                    break;
                case 1:
                    enemyToSpawn = 1;
                    break;
                case 2:
                    enemyToSpawn = 2;
                    break;
                case 3:
                    enemyToSpawn = Random.Range(0, 1);
                    break;
                case 4:
                    enemyToSpawn = Random.Range(1, 2);
                    break;
                case 5:
                    int[] fuck = new int[]{0, 2};
                    enemyToSpawn = fuck[Random.Range(0, 1)];
                    break;*/
                default:
                    enemyToSpawn = Random.Range(0, 2);
                    break;
            }

            Instantiate(Enemies[enemyToSpawn], location.position, Quaternion.identity);
        }
    }

    public void DespawnLevel()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel);
            currentLevelIndex++;
        }
        else
        {
            Debug.LogWarning("No Level To Despawn");
        }
    }

    public static void SubscribeToEnemyEvent(UnityEvent _event)
    {
        _event?.AddListener(EnemyDeath);
    }

    public static void EnemyDeath()
    {
        currentKillCount++;
        Debug.Log(currentKillCount);
        if (currentKillCount >= TargetKillCount[currentLevelIndex])
        {
            //Level geschafft
        }
        else
        {
            //spawn enemy at random spawn point
        }
    }
}
