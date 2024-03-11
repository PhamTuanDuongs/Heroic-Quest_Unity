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
    private HealthUI healthUI;

    private void Awake()
    {
        knockBack = GetComponent<KnockBack>();
        flash = GetComponent<Flash>();
        healthUI = GetComponent<HealthUI>();
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
        knockBack.GettingKnocked(PlayerController.Instance.transform, 15f);
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
            Destroy(gameObject);
        }
    }
}
