using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] private int health = 3;
    private int currentHealth;
    private KnockBack knockBack;
    private Flash flash;

    private void Awake()
    {
        knockBack = GetComponent<KnockBack>();
        flash = GetComponent<Flash>();
    }
    void Start()
    {
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
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
            Destroy(gameObject);
        }
    }
}
