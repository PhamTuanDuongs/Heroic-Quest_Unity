using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chasing : MonoBehaviour, IEnemy
{
    EnemyPathFinding enemyPathFinding;
    private void Awake()
    {
        enemyPathFinding = GetComponent<EnemyPathFinding>();
    }
    public void Attack()
    {
        Vector2 targetDirection = PlayerController.Instance.transform.position - transform.position;
        enemyPathFinding.MoveTo(targetDirection);
    }
}
