using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
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
    [SerializeField] private LayerMask boundary;

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
        //if (currentWave >= wavesToSpawn.Length)
        //{
        //    StopAllCoroutines();
        //    yield return null;
        //}

        //spawn enemy

        int numberEnemies = initCount + (currentWave * incrementalNum);
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



            GameObject enemy = Instantiate(objectToSpawn, GetRandomPosition(), Quaternion.identity);
            enemy.GetComponent<EnemyHealth>().OnEnemyDead += playerLevel.OnGetExp;

            yield return new WaitForSeconds(.5f);
        }

    }

    private Vector2 GetRandomPosition()
    {

        Vector2 spawnPosition = transform.position;

        float randomDistance = Random.Range(0f, spawnArea.magnitude);
        float randomAngle = Random.Range(0f, 360f);
        spawnPosition = (Vector2)transform.position + new Vector2(Mathf.Cos(randomAngle) * randomDistance, Mathf.Sin(randomAngle) * randomDistance);
        if (Physics2D.OverlapCircle(spawnPosition, 0.5f, boundary))
            spawnPosition = transform.position;

        return spawnPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, spawnArea.magnitude);
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}
