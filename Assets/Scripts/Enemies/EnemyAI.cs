using System;
using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float roamChangeDirFloat = 2f;
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private MonoBehaviour enemyType;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private bool stopMovingWhileAttacking = false;

    private bool canAttack = true;
    private PlayerController playerController;
    private enum State
    {
        Roaming,
        Attacking
    }

    private Vector2 roamPosition;
    private float timeRoaming = 0f;

    private State state;
    private EnemyPathFinding pathFinding;
    private void Awake()
    {
        pathFinding = GetComponent<EnemyPathFinding>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        state = State.Roaming;
    }

    private void Start()
    {
        roamPosition = GetRoamPosition();
    }


    private void Update()
    {
        MovementStateControl();
    }

    private void MovementStateControl()
    {
        switch (state)
        {
            case State.Roaming:
                Roaming();
                break;
            case State.Attacking:
                Attacking();
                break;
        }
    }

    private void Roaming()
    {
        timeRoaming += Time.deltaTime;
        pathFinding.MoveTo(roamPosition);
        if (Vector2.Distance(transform.position, playerController.transform.position) < attackRange)
        {
            state = State.Attacking;
        }

        if (timeRoaming > roamChangeDirFloat)
        {
            roamPosition = GetRoamPosition();
        }
    }

    private void Attacking()
    {
        if (Vector2.Distance(transform.position, playerController.transform.position) > attackRange)
        {
            state = State.Roaming;
        }

        if (attackRange != 0 && canAttack)
        {
            canAttack = false;
            (enemyType as IEnemy)?.Attack();

            if (stopMovingWhileAttacking)
            {
                pathFinding.StopMoving();
            }
            else if (enemyType is not Chasing)
            {
                pathFinding.MoveTo(roamPosition);
            }

            StartCoroutine(AttackCoolDownRoutine());
        }
    }



    private Vector2 GetRoamPosition()
    {
        timeRoaming = 0f;
        return new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }

    private IEnumerator AttackCoolDownRoutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
