using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyWave
{
    public GameObject[] enemiesToSpawn;
    public int[] totalEnemiesToSpawn;
}
public class EnemyGenerate : MonoBehaviour
{
    [SerializeField] private EnemyWave[] wavesToSpawn;
    [SerializeField] private Vector2 spawnArea;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private PlayerLevel playerLevel;
    [SerializeField] private WaveUI waveUI;
    [SerializeField] private GameObject[] enemyTypes;
    [SerializeField] private int initCount = 2;
    [SerializeField] private int incrementalNum = 1;

    private int currentWave = 0;
    private bool canSpawn = true;

    private void Start()
    {
        playerLevel = FindFirstObjectByType<PlayerLevel>();
        StartCoroutine(SpawnWave());
    }

    private void Update()
    {
        GameObject[] enemiesInWave = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemiesInWave.Length <= 0 && canSpawn)
        {
            StartCoroutine(waveUI.WaveComplete());
            StartCoroutine(SpawnWave());
        }
    }
    IEnumerator SpawnWave()
    {
        canSpawn = false;
        if (currentWave != 0)
        {
            yield return new WaitForSeconds(1f);
        }
        Debug.Log("c: " + currentWave);
        //if (currentWave >= wavesToSpawn.Length)
        //{
        //    StopAllCoroutines();
        //    yield return null;
        //}

        //spawn enemy

        int numberEnemies = initCount + (currentWave * incrementalNum);
        Debug.Log("total Enemies: " + (currentWave * incrementalNum));
        for (int i = 0; i < enemyTypes.Length; i++)
        {
            StartCoroutine(SpawnRandomObject(enemyTypes[i], numberEnemies));

        }

        //for (int i = 0; i < wavesToSpawn[currentWave].enemiesToSpawn.Length; i++)
        //{
        //    StartCoroutine(SpawnRandomObject(wavesToSpawn[currentWave].enemiesToSpawn[i], wavesToSpawn[currentWave].totalEnemiesToSpawn[i]));
        //}
        currentWave++;
        canSpawn = true;
        //yield return new WaitForSeconds(spawnInterval);
        //StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnRandomObject(GameObject objectToSpawn, int total)
    {
        int count = 0;
        while (count < total)
        {

            count++;
            float randomDistance = Random.Range(0f, spawnArea.magnitude);
            float randomAngle = Random.Range(0f, 360f);
            Vector2 spawnPosition = (Vector2)transform.position + new Vector2(Mathf.Cos(randomAngle) * randomDistance, Mathf.Sin(randomAngle) * randomDistance);

            GameObject enemy = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
            enemy.GetComponent<EnemyHealth>().OnEnemyDead += playerLevel.OnGetExp;

            yield return new WaitForSeconds(.5f);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, spawnArea.magnitude);
    }
}
