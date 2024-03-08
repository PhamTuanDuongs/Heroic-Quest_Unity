using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 3;
    [SerializeField] private float knockBackThrustAmount = 10f;
    [SerializeField] private float damageRecoveryTime = 1f;

    private bool canTakeDamage = true;
    private int currentHealth;
    private KnockBack knockBack;
    private Flash flash;
    private Rigidbody2D rb;
    private void Awake()
    {
        knockBack = GetComponent<KnockBack>();
        flash = GetComponent<Flash>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    private void OnCollisionStay2D(Collision2D collision)
    {
        EnemyAI enemy = collision.gameObject.GetComponent<EnemyAI>();
        if (enemy)
        {
            TakeDamage(1, enemy.transform);
            //knockBack.GettingKnocked(collision.gameObject.transform, knockBackThrustAmount);
            //StartCoroutine(flash.FlashRoutine());
        }
    }

    public void TakeDamage(int damage, Transform source)
    {
        if (!canTakeDamage) { return; }

        knockBack.GettingKnocked(source, knockBackThrustAmount);
        StartCoroutine(flash.FlashRoutine());
        canTakeDamage = false;
        currentHealth -= damage;
        StartCoroutine(RecoveryAfterAttack());
    }

    private IEnumerator RecoveryAfterAttack()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }

    public void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
