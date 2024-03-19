using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public EventHandler<int> OnEnemyDead;
    [SerializeField] private int expDrop;

    [SerializeField] private int health = 3;
    [SerializeField] private int currentHealth;

    private KnockBack knockBack;
    private Flash flash;
    private PickupSpawner pickupSpawner;
    private PlayerController playerController;

    //Display hp
    private HealthUI healthUI;

    private void Awake()
    {
        knockBack = GetComponent<KnockBack>();
        flash = GetComponent<Flash>();
        healthUI = GetComponent<HealthUI>();
        pickupSpawner = GetComponent<PickupSpawner>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    void Start()
    {
        currentHealth = health;
        healthUI.UpdateHealth(currentHealth, health);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthUI.UpdateHealth(currentHealth, health);
        knockBack.GettingKnocked(playerController.transform, 15f);
        StartCoroutine(flash.FlashRoutine());
        StartCoroutine(CheckDetectRoutine());

    }

    private IEnumerator CheckDetectRoutine()
    {
        yield return new WaitForSeconds(flash.FlashDuration);
        DetectDeath();
    }

    public void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            OnEnemyDead.Invoke(this, expDrop);
            pickupSpawner.DropItems();
            Destroy(gameObject);
        }
    }
}
