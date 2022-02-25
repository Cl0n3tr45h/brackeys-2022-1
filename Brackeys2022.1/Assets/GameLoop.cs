using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

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
        
        //spawn enemy at every point
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
