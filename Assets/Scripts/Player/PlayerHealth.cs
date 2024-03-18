using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 3;
    [SerializeField] private float knockBackThrustAmount = 10f;
    [SerializeField] private float damageRecoveryTime = 1f;
    [SerializeField] private AudioSource takeDamageSound;

    private bool canTakeDamage = true;
    private int currentHealth;
    private KnockBack knockBack;
    private Flash flash;
    private Rigidbody2D rb;

    //Display hp
    private HealthUI healthUI;

    private void Awake()
    {
        knockBack = GetComponent<KnockBack>();
        flash = GetComponent<Flash>();
        rb = GetComponent<Rigidbody2D>();
        healthUI = GetComponent<HealthUI>();
    }
    void Start()
    {
        currentHealth = maxHealth;
        healthUI.UpdateHealth(currentHealth, maxHealth);
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
        takeDamageSound.Play();
        if (!canTakeDamage) { return; }
        currentHealth -= damage;
        healthUI.UpdateHealth(currentHealth, maxHealth);
        knockBack.GettingKnocked(source, knockBackThrustAmount);
        StartCoroutine(flash.FlashRoutine());
        canTakeDamage = false;
        StartCoroutine(RecoveryAfterAttack());
        DetectDeath();
    }

    public void Recovery(int value)
    {
        currentHealth += value;
        healthUI.UpdateHealth(currentHealth, maxHealth);
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
            SceneManager.LoadScene(1);

        }
    }
}
