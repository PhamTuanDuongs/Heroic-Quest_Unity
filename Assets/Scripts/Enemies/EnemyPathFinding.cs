using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyPathFinding : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2f;
    private Rigidbody2D rb;
    private Vector2 moveDir;
    private KnockBack knockBack;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        knockBack = GetComponent<KnockBack>();

    }

    private void FixedUpdate()
    {
        if (knockBack.gettingKnockBack) return;
        rb.MovePosition(rb.position + (moveDir * moveSpeed * Time.deltaTime));
    }
    public void MoveTo(Vector2 roamPosition)
    {

        moveDir = roamPosition;
    }

    internal void StopMoving()
    {
        moveDir = new Vector2(0, 0);
    }
}
