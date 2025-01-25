using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSystem : MonoBehaviour
{
    private static TerrainSystem instance;
    public static TerrainSystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TerrainSystem>();
                if (instance == null)
                {
                    GameObject go = new GameObject("TerrainSystem");
                    instance = go.AddComponent<TerrainSystem>();
                }
            }
            return instance;
        }
    }

    public GameObject[] terrainBlocks;
    public GameObject LavaBlock;

    void Start()
    {
        // TODO: Initialize TerrainSystem
        DEBUG_randomSetLava9();
    }

    void DEBUG_randomSetLava9()
    {
        foreach(GameObject block in terrainBlocks)
        {
            if(Random.Range(0, 10) == 0)
            {
                StartCoroutine(block.GetComponent<Block>().FloorIsLava());
            }
        }
    }
    
}