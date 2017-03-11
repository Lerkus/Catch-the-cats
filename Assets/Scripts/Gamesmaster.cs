using UnityEngine;
using System.Collections;


public class Gamesmaster : MonoBehaviour
{

    private GameObject[] spawnPoints;
    private Coroutine waveTimer;
    public float timeBetweenSpawnes = 5;
    public bool shouldSpawn = true;

    public GameObject catPrefab;
    public GameObject heavyPrefab;

    public float catSpawnChance = 0.25f;

    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("spawn");
        print(spawnPoints.Length);
        waveTimer = StartCoroutine(waveWaiter());
    }

    public void spawn()
    {
        int spawnCycles = (int)Random.Range(1, 4);
        for (int i = 0; i < spawnCycles; i++)
        {

            if (shouldSpawn)
            {
                Vector3 spawnPoint = spawnPoints[(int)Random.Range(0, spawnPoints.Length)].transform.position;

                if (Random.Range(0f, 1f) < catSpawnChance)
                {
                    Instantiate(catPrefab, spawnPoint, new Quaternion());
                }
                else
                {
                    Instantiate(heavyPrefab, spawnPoint, new Quaternion());
                }
            }
        }
    }

    public IEnumerator waveWaiter()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenSpawnes);
            spawn();
        }
    }
}
