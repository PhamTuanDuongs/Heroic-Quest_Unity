using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerate : MonoBehaviour
{
    [SerializeField] private GameObject[] enemiesToSpawn;
    [SerializeField] private Vector2 spawnArea;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private PlayerLevel playerLevel;

    private void Start()
    {
        playerLevel = FindFirstObjectByType<PlayerLevel>();
        StartCoroutine(SpawnRandomObject());
    }

    IEnumerator SpawnRandomObject()
    {
        GameObject objectToSpawn = enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)];
        float randomDistance = Random.Range(0f, spawnArea.magnitude);
        float randomAngle = Random.Range(0f, 360f);
        Vector2 spawnPosition = (Vector2)transform.position + new Vector2(Mathf.Cos(randomAngle) * randomDistance, Mathf.Sin(randomAngle) * randomDistance);

        GameObject enemy = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        enemy.GetComponent<EnemyHealth>().OnEnemyDead += playerLevel.OnGetExp;

        yield return new WaitForSeconds(spawnInterval);
        StartCoroutine(SpawnRandomObject());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, spawnArea.magnitude);
    }
}