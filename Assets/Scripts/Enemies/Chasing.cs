using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chasing : MonoBehaviour, IEnemy
{
    EnemyPathFinding enemyPathFinding;
    private PlayerController playerController;
    private void Awake()
    {
        enemyPathFinding = GetComponent<EnemyPathFinding>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    public void Attack()
    {
        Vector2 targetDirection = playerController.transform.position - transform.position;
        enemyPathFinding.MoveTo(targetDirection);
    }
}
