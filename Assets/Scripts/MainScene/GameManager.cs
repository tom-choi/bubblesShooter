using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class EnemyList
    {
        public GameObject enemyPrefab;  // 怪物預製體
        public int score;               // 怪物分值
    }

    public List<EnemyList> enemies = new List<EnemyList>();  // 可生成的怪物列表
    public float spawnInterval = 0.5f;  // 生成間隔
    public float mapSize = 15f;         // 地圖大小
    public int waveScore = 18;          // 每波怪物總分值

    private void Start()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        int remainingScore = waveScore;

        while (remainingScore > 0)
        {
            // 篩選出分值不超過剩餘分值的怪物
            List<EnemyList> availableEnemies = enemies.FindAll(e => e.score <= remainingScore);
            
            if (availableEnemies.Count == 0)
                break;

            // 隨機選擇一個怪物
            EnemyList selectedEnemy = availableEnemies[Random.Range(0, availableEnemies.Count)];
            
            // 在外圍隨機位置生成怪物
            Vector3 spawnPosition = GetRandomSpawnPosition();
            Instantiate(selectedEnemy.enemyPrefab, spawnPosition, Quaternion.identity);
            
            remainingScore -= selectedEnemy.score;
            
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // 获取随机生成位置
    private Vector3 GetRandomSpawnPosition()
    {
        // 获取地图大小的一半
        float halfSize = mapSize / 2f;
        Vector3 spawnPos;

        // 隨機選擇生成在哪一邊
        int side = Random.Range(0, 4);

        int hight = 3;
        
        switch (side)
        {
            case 0: // 上邊
                spawnPos = new Vector3(
                    Random.Range(-halfSize, halfSize),
                    hight,
                    halfSize
                );
                break;
            case 1: // 右邊
                spawnPos = new Vector3(
                    halfSize,
                    hight,
                    Random.Range(-halfSize, halfSize)
                );
                break;
            case 2: // 下邊
                spawnPos = new Vector3(
                    Random.Range(-halfSize, halfSize),
                    hight,
                    -halfSize
                );
                break;
            default: // 左邊
                spawnPos = new Vector3(
                    -halfSize,
                    hight,
                    Random.Range(-halfSize, halfSize)
                );
                break;
        }

        return spawnPos;
    }

    // 開始新一波生成
    public void StartNewWave()
    {
        StartCoroutine(SpawnWave());
    }
}