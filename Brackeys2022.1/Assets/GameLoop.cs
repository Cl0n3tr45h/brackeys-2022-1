using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public enum GameState
{
    FIGHT,
    LOOT,
    CRAFT
}
public class GameLoop : MonoBehaviour
{
    [Header("Player")]    
    public GameObject Player;
    public Inventory InventoryObject;
    public static Inventory Inventory;
    public static GameState gameState;
    [Space][Space] 
    [Header("Level Initialization")]
    public List<GameObject> Levels;
    private List<GameObject> levelSequence = new List<GameObject>();
    private static int currentLevelIndex = 0;
    private static GameObject currentLevel;
    public int[] TargetKills;
    public static int[] TargetKillCount;
    private static int currentKillCount;
    public float[] SpawnRate;
    private static float currentSpawnTimer;

    public GameObject[] EnemiePrefabs;
    private static GameObject[] Enemies;
    private static List<GameObject> activeEnemies = new List<GameObject>();
    private static List<Transform> enemySpawnPoints = new List<Transform>();
    private static float enemyCount;
    private static float enemyInReal;
    public static bool NextEnemyReal;
    [Space] [Space] 
    [Header("Loot")] 
    public Canvas LootCanvas;
    public GameObject ComplexNumberPanel;
    public GameObject OperandPanel;
    public GameObject LootPrefab;
    private bool lootGenerated = false;
    
    [Space][Space] [Header("Crafting")] public Canvas CraftingCanvas;
    //Combo
    //

    private void Awake()
    {
        var random = new System.Random();
        levelSequence = Levels.OrderBy(item => random.Next()).ToList();
        Inventory = InventoryObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        Enemies = new GameObject[EnemiePrefabs.Length];
        for(int i = 0; i < EnemiePrefabs.Length; i++)
            Enemies[i] = EnemiePrefabs[i];
        SpawnLevel();
        TargetKillCount = TargetKills;
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
                DespawnLevel();
                SpawnLevel();
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
        if (currentSpawnTimer >= SpawnRate[currentLevelIndex])
        {   //then we spawn
                     
            SpawnEnemy(Random.Range(0, enemySpawnPoints.Count - 1));
            currentSpawnTimer = 0;
        }
        else
        {
            currentSpawnTimer += Time.deltaTime;
        }
    }

    public void LootBehaviour()
    {
        if (!lootGenerated)
        {
            LootCanvas.gameObject.SetActive(true);
            GenerateLoot();
            lootGenerated = true;
        }
        // StartCoroutine("CollectLoot");
    }

    public void GenerateLoot()
    {
        //Combo stuff <v <;;;;;
        var amountOfLoot = 3; //3 by default, can get higher the higher combos you get??? maybe?
        GameObject[] loot = new GameObject[amountOfLoot];
        for (int i = 0; i < loot.Length; i++)
        {
            loot[i] = Instantiate(LootPrefab, Vector3.zero, Quaternion.identity);
            loot[i].transform.SetParent(ComplexNumberPanel.transform);
            loot[i].transform.localPosition = Vector3.zero;
            
        }
    }

    public IEnumerator CollectLoot()
    {
        yield return new WaitForSeconds(1f);
    }
    public void CraftBehaviour()
    {
        
    }

    public void SpawnLevel()
    {
        //Generate current Level
        currentLevel = Instantiate(levelSequence[currentLevelIndex], Vector3.zero, Quaternion.identity);
        //Set Player active
        if (!Player.activeSelf)
        {
            Player.SetActive(true);
        }
        //Get player spawn
        Player.transform.SetPositionAndRotation(GameObject.Find("PlayerSpawn").transform.position, Quaternion.identity);
        
        //Get enemy spawn points 
        enemySpawnPoints = new List<Transform>();
        var parent = GameObject.Find("EnemySpawns");
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            enemySpawnPoints.Add(parent.transform.GetChild(i));
        }

        //spawn enemy at every point
        activeEnemies = new List<GameObject>();
        for (int i = 0; i < enemySpawnPoints.Count; i++)
        {
            SpawnEnemy(i);   
            FindObjectOfType<AudioManager>().Play("EnemySpawn");
        }
        
    }

    public static void SpawnEnemy(int _spawnPoint)
    {
        var enemyToSpawn = 0;
        switch (currentLevelIndex)
        {
            case 0:
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
                break;
            default:
                enemyToSpawn = Random.Range(0, 2);
                break;
        }
        NextEnemyReal = !NextEnemyReal;
        
        activeEnemies.Add(Instantiate(Enemies[enemyToSpawn], enemySpawnPoints[_spawnPoint].position, Quaternion.identity));

    }
    
    public static void DespawnLevel()
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
    public static void UnSubscribeToEnemyEvent(UnityEvent _event)
    {
        _event?.RemoveListener(EnemyDeath);
    }

    public static void EnemyDeath()
    {
        currentKillCount++;
        Debug.Log(currentKillCount);
        if (currentKillCount >= TargetKillCount[currentLevelIndex])
        {
            //Level geschafft
            ClearRemainingEnemies();
            DespawnLevel();
            gameState = GameState.LOOT;
        }
        else
        {
            //spawn enemy at random spawn point
            
            SpawnEnemy(Random.Range(0, enemySpawnPoints.Count - 1));
            currentSpawnTimer = 0;
        }
    }

    public static void ClearRemainingEnemies()
    {
        foreach (var enemy in activeEnemies)
        {
            Destroy(enemy);
        }
    }
}
