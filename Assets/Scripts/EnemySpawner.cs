using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemyList;
    [SerializeField] int[] enemyCounts;
    [SerializeField] float spawnTimer;

    float sinceLastSpawn;
    int emptyFields = 0;
    // Start is called before the first frame update
    void Start()
    {
        sinceLastSpawn = spawnTimer;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnFirstStep();
    }
    void SpawnFirstStep()
    {
        if (emptyFields < enemyList.Length)
        {
            if (sinceLastSpawn <= 0)
            {
                int who = Random.Range(0, enemyList.Length);
                SpawnSecondStep(who);
                sinceLastSpawn = spawnTimer;
            }
            sinceLastSpawn -= Time.deltaTime;
        }
    }



    void SpawnSecondStep(int who)
    {
        if (enemyCounts[who] > 0)
        {
            Instantiate(enemyList[who], new Vector3((int)Random.Range(-2, 3), 0.8f, 7), enemyList[who].transform.rotation);
            enemyCounts[who]--;
            emptyFields = 0;
            return;
        }
        if (emptyFields != enemyCounts.Length)
        {
            who++;
            emptyFields++;
            SpawnSecondStep(who % enemyCounts.Length);
        }

    }
}
