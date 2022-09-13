using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemyList;
    [SerializeField] int[] enemyCounts;
    [SerializeField] float spawnTimer;
    [SerializeField] TextMeshProUGUI winText;

    float sinceLastSpawn;
    int emptyFields = 0;
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        sinceLastSpawn = spawnTimer;
        foreach(int i in enemyCounts)
        {
            count += i;
        }
        winText.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //ABSTACTION
        SpawnFirstStep();
        TriggerWin();
    }
    //ABSTRACTION
    void TriggerWin()
    {

    }
    //ABSTACTION
    void SpawnFirstStep()
    {
        if (emptyFields < enemyList.Length)
        {
            if (sinceLastSpawn <= 0)
            {
                int who = Random.Range(0, enemyList.Length);
                //ABSTACTION
                SpawnSecondStep(who);
                sinceLastSpawn = spawnTimer;
            }
            sinceLastSpawn -= Time.deltaTime;
        }
    }


    //ABSTACTION
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
    public void CountDown()
    {
            count--;
        if (count == 0){
            winText.alpha = 1;
        }
    }
}
