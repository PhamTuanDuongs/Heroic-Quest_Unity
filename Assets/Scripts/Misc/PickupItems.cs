using HeroicQuest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItems : MonoBehaviour
{
    [SerializeField] private float pickUpDistance = 5f;
    [SerializeField] private float accelartionRate = .2f;
    [SerializeField] private float moveSpeed = 3f;
    GameplayUI gameplayUI;
    private Vector3 moveDir;
    private Rigidbody2D rb;
    private PlayerController playerController;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gameplayUI = GameObject.Find("GameplayUI").GetComponent<GameplayUI>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        Vector3 playerPos = playerController.transform.position;

        if (Vector3.Distance(transform.position, playerPos) < pickUpDistance)
        {
            moveDir = (playerPos - transform.position).normalized;
            moveSpeed += accelartionRate;
        }
        else
        {
            moveDir = Vector3.zero;
            moveSpeed = 0;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDir * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            gameplayUI.UpdateMoney();
            Destroy(gameObject);
        }
    }
}
