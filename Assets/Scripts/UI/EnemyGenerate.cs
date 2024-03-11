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
    private int currentWave = 0;

    private void Start()
    {
        playerLevel = FindFirstObjectByType<PlayerLevel>();
        StartCoroutine(SpawnWave());
    }
    IEnumerator SpawnWave()
    {
        Debug.Log("c: " + currentWave);
        if(currentWave >= wavesToSpawn.Length)
        {
            Debug.Log("??????");
            StopAllCoroutines();
            yield return null;
        }

        for(int i = 0; i < wavesToSpawn[currentWave].enemiesToSpawn.Length; i++)
        {
            StartCoroutine(SpawnRandomObject(wavesToSpawn[currentWave].enemiesToSpawn[i], wavesToSpawn[currentWave].totalEnemiesToSpawn[i]));
        }
        currentWave++;
        yield return new WaitForSeconds(spawnInterval);
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnRandomObject(GameObject objectToSpawn, int total)
    {
        int count = 0;
        while(count < total)
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
