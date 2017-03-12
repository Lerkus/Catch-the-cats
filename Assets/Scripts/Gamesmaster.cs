using UnityEngine;
using System.Collections;


public class Gamesmaster : MonoBehaviour
{

    private GameObject[] spawnPoints;
    private Coroutine waveTimer;
    public float timeBetweenSpawnes = 5;
    public bool shouldSpawn = true;

    public AudioClip mySound;
    public AudioClip openSound;
    public AudioClip[] cats;
    public AudioSource soundSource;

    private float lowPitchRange = .90F;
    private float highPitchRange = 1.5F;



    public GameObject catPrefab;
    public GameObject heavyPrefab;

    public float catSpawnChance = 0.25f;
    

    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("spawn");
        print(spawnPoints.Length);
        waveTimer = StartCoroutine(waveWaiter());
        soundSource = GetComponent<AudioSource>();
        soundSource.volume = 0.7f;
        
    }

    public void spawn()
    {
        int spawnCycles = (int)Random.Range(1, 4);
        int randomNumber;
        GameObject spawnWindow;
        Vector3 spawnPoint;
        
        soundSource.PlayOneShot(openSound, 1);
        for (int i = 0; i < spawnCycles; i++)
        {
            if (shouldSpawn)
            {
                spawnWindow = spawnPoints[(int)Random.Range(0, spawnPoints.Length)];
                spawnPoint = spawnWindow.transform.position;
                spawnWindow.GetComponent<Windows>().open();
                randomNumber = (int)Random.Range(0,3);

                soundSource.pitch = (float)1;
                soundSource.pitch = Random.Range(lowPitchRange, highPitchRange);
                mySound = cats[randomNumber];


                if (Random.Range(0f, 1f) < catSpawnChance)
                {
                    Instantiate(catPrefab, spawnPoint, new Quaternion());
                    soundSource.PlayOneShot(mySound, 1);
                }
                else
                {
                    Instantiate(heavyPrefab, spawnPoint, new Quaternion());
                }
                spawnWindow.transform.GetChild(0).gameObject.SetActive(true);
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
