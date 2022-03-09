using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject chiken;
    public float spawnTime = 5f;
    public float spawnDelay = 0f;
    public float nbSpawn = 10;
    public float time;
    public GameObject gameRule;

    private int nbCurrentSpawn = 0;

    private void Start()
    {         
        time = gameRule.GetComponent<GameRule>().startingTime;
        //Debug.Log(spawnDelay);
        nbSpawn = time / spawnDelay;
        InvokeRepeating("SpawnChiken", spawnTime, spawnDelay);
    }

    void SpawnChiken()
    {
        //Vector3 randomSpawnPosition = transform.localPosition + new Vector3(Random.Range(-2.5f, 2.6f), -2.5f, Random.Range(-2.5f, 2.6f));
        Vector3 randomSpawnPosition = transform.localPosition + new Vector3(0, -2.5f, 0);
        Instantiate(chiken, randomSpawnPosition, Quaternion.Euler(0, 90, 0));
        nbCurrentSpawn++;
        if (nbCurrentSpawn >= nbSpawn)
        {
            //Debug.Log("fini");
            CancelInvoke("SpawnChiken");
        }
    }


}