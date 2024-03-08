using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material whiteFlashMaterial;
    [SerializeField] private float flashDuration = .2f;

    private Material materialDefault;
    private SpriteRenderer sr;
    private EnemyHealth enemyHealth;

    public float FlashDuration { get => flashDuration; }

    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        sr = GetComponent<SpriteRenderer>();
        materialDefault = sr.material;
    }

    public IEnumerator FlashRoutine()
    {
        sr.material = whiteFlashMaterial;
        yield return new WaitForSeconds(FlashDuration);
        sr.material = materialDefault;

    }
}
