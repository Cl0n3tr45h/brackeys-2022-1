using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public enum GameState
{
    FIGHT,
    UPGRADE,
    DEAD,
    WON
}
public class GameLoop : MonoBehaviour
{
    [Header("Player")]    
    public GameObject Player;
    //public Inventory InventoryObject;
    public PlayerInventoryManager InventoryObject;
    public static GameState gameState;
    [Space][Space] 
    [Header("Level Initialization")]
    public List<GameObject> Levels;
    private static List<GameObject> levelSequence = new List<GameObject>();
    private static int currentLevelIndex = 0;
    private static GameObject currentLevel;
    public int[] TargetKills;
    public static int[] TargetKillCount;
    private static int currentKillCount;
    private static int allTimeKillCount;
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

    public Canvas UpgradeCanvas;
    
    public LootManager LootManager;
    private bool lootGenerated = false;
    private bool upgradesGenerated = false;
    
    [Space][Space] [Header("Crafting")] public Canvas CraftingCanvas;
    [Space][Space] [Header("End")] 
    public Canvas GameOverCanvas;

    public TextMeshProUGUI Text;
    public String[] ReasonOfTerminationStrings;
    private static String[] ReasonOfTermination;
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
            case GameState.UPGRADE:
                UpgradeBehaviour();
                break;
            case GameState.DEAD:
                EndGame();
                break;
            case GameState.WON:
                WinGame();
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

    public void UpgradeBehaviour()
    {
        if (!upgradesGenerated)
        {
            UpgradeCanvas.gameObject.SetActive(true);
            UpgradeCanvas.GetComponent<UpgradeManager>().GenerateUpgrades();
            upgradesGenerated = true;
        }
        
    }

    public void FinishUpgrading()
    {
        UpgradeCanvas.gameObject.SetActive(false);
        Debug.Log("finished upgrading");
        upgradesGenerated = false;

        gameState = GameState.FIGHT;
    }

    public void LootBehaviour()
    {
        if (!lootGenerated)
        {
            LootCanvas.gameObject.SetActive(true);
            GenerateLoot();
            lootGenerated = true;
        }
    }

    public GameObject OperandPrefab;

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
            LootManager.Toggles[i] = loot[i].transform.GetComponentInChildren<Toggle>();
        }

        var operand = Instantiate(OperandPrefab, Vector3.zero, Quaternion.identity);
        operand.transform.SetParent(OperandPanel.transform);
        operand.transform.localPosition = Vector3.zero;
        var lootOperand = operand.GetComponent<LootOperand>();
        if (currentLevelIndex == 3 || currentLevelIndex == 6)
            lootOperand.Operand.isAdd = false;
        else
            lootOperand.Operand.isAdd = true;
        lootOperand.SetSprite();
        LootManager.OnSetActive();
        
        LootManager.Operand = lootOperand.Operand;
        Debug.Log(LootManager.Operand.isAdd);
    }

    /*public void CollectLoot()
    {
        InventoryObject.AddNumbers(LootManager.SelectedLoot);
        InventoryObject.AddOperand(LootManager.Operand);

        Debug.Log(InventoryObject.Print());
        LootCanvas.gameObject.SetActive(false);
        
        gameState = GameState.CRAFT;
        lootGenerated = false;
    }*/
    public void CraftBehaviour()
    {
        CraftingCanvas.gameObject.SetActive(true);
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
        }

        currentKillCount = 0;
    }

    public static void SpawnEnemy(int _spawnPoint)
    {
        var enemyToSpawn = 0;
        switch (currentLevelIndex)
        {
            case 0:
                enemyToSpawn = Random.Range(0, 3);
                //enemyToSpawn = 0;
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
                enemyToSpawn = Random.Range(1, 3);
                break;
            case 5:
                int[] fuck = new int[]{0, 2};
                enemyToSpawn = fuck[Random.Range(0, 2)];
                break;
            default:
                enemyToSpawn = Random.Range(0, 3);
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
            if (currentLevelIndex >= levelSequence.Count)
            {
                gameState = GameState.WON;
            }
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
        allTimeKillCount++;
        if (currentKillCount >= TargetKillCount[currentLevelIndex])
        {
            //Level geschafft
            ClearRemainingEnemies();
            DespawnLevel();
            gameState = GameState.UPGRADE;
            Debug.Log("Finished");
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

    public void WinGame()
    {
        GameOverCanvas.gameObject.SetActive(true);
        Text.text = "Skullbird Culling: successful.\nCongratulations.\n\nYou completed all 7 Levels";
    }
    
    public void EndGame()
    {
        GameOverCanvas.gameObject.SetActive(true);
        Text.text = "Skullbird Culling: terminated.\nReason of termination: " +
                    ReasonOfTerminationStrings[currentLevelIndex] + "\n\n Reached Level: " + (currentLevelIndex+1) + " of 7";
    }
}
