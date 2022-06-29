using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    const int CHUNKWIDTH = 16;
    const int CHUNKHEIGHT = 9;
    const int BLOCKSIZE = 60;

    // Each block will have a sprite for real and imaginary that it toggled between
    public List<GameObject> BlockPrefabs;
    private List<GameObject> BlocksInstanced = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        GenerateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateLevel()
    {
        DeleteLevel();

        // generate for each chunk
        for (int chunkCount = 0; chunkCount < 1; chunkCount++)
        {
            List<int> chunkData = GetChunkArray(chunkCount);
            string chunkContents = "[";
            foreach (int item in chunkData)
            {
                chunkContents += item + ", ";
            }
            chunkContents += "]";

           // Debug.Log("chunkData at " + chunkCount + chunkContents);

            for(int blockCountThisChunk = 0; blockCountThisChunk < chunkData.Count; blockCountThisChunk++)
            {
                float offsetX = (blockCountThisChunk % CHUNKHEIGHT) * BLOCKSIZE;
                float offsetY = Mathf.Floor(blockCountThisChunk / CHUNKWIDTH) * BLOCKSIZE;
                Vector2 offset = new Vector2(offsetX, offsetY);
                InstanceBlockAtCoords(offset);
                Debug.Log(offset);
            }

            /*
            for(int blockCountThisChunk = 0; blockCountThisChunk < 144; blockCountThisChunk++)
            {
                // offset because of chunk
                float chunkOffsetX = (chunkCount % 3) * CHUNKWIDTH * BLOCKSIZE;
                float chunkOffsetY = (chunkCount / 3) * CHUNKHEIGHT * BLOCKSIZE;

                if (chunkData[blockCountThisChunk] != 0)
                {
                    //offset within the chunk
                    float offsetX = (blockCountThisChunk % CHUNKWIDTH) * BLOCKSIZE;
                    float offsetY = (blockCountThisChunk / CHUNKHEIGHT) * BLOCKSIZE;
                    Vector2 offset = new Vector2(offsetX + chunkOffsetX, offsetY + chunkOffsetY);
                    Debug.Log(offset);
                    InstanceBlockAtCoords(offset);
                }
            }*/
        }
    }

    public List<int> GetChunkArray(int _chunkPos)
    {
        List<int> result = new List<int>();
        // out of bounds for some reason
        if (_chunkPos > 8)
        {
            for (int i = 0; i < 144; i++)
            {
                result.Add(0);
            }
            
        }
        else
        {
            for (int i = 0; i < 144; i++)
            {
                result.Add(1);
            }
        }

        return result;
    }

    public void InstanceBlockAtCoords(Vector2 _targetPos)
    {
        int randBlockIndex = (int) (Random.Range(0, BlockPrefabs.Count));
        GameObject newBlock = Instantiate(BlockPrefabs[randBlockIndex], new Vector3(Mathf.Floor(_targetPos.x / 100), -(Mathf.Floor(_targetPos.y / 100)), 0), Quaternion.identity);
        BlocksInstanced.Add(newBlock);
    }

    public void DeleteLevel()
    {
        // delete each block that currently exists
        foreach(GameObject g in BlocksInstanced)
        {
            Destroy(g);
        }
        // override BlocksInstanced with a blank list
        BlocksInstanced = new List<GameObject>();
    }
}
