using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour, IEnemy
{

    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private float bulletMoveSpeed;
    [SerializeField] private int burstCount;
    [SerializeField] private float timeBetweenBurst;
    [SerializeField] private float burstSpeed;
    [SerializeField] private float resetTime = 1f;

    private bool isShooting = false;
    private PlayerController playerController;

    private void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    public void Attack()
    {
        //Vector2 targetDirection = PlayerController.Instance.transform.position - transform.position;
        //GameObject newBullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
        //newBullet.transform.right = targetDirection;
        if (!isShooting)
        {
            StartCoroutine(ShootRoutine());
        }
    }

    private IEnumerator ShootRoutine()
    {
        isShooting = true;
        for (int i = 0; i < burstCount; i++)
        {
            Vector2 targetDirection = playerController.transform.position - transform.position;
            GameObject newBullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
            newBullet.transform.right = targetDirection;
            if (newBullet.TryGetComponent(out Projectiles projectiles))
            {
                projectiles.UpdateMoveSpeed(bulletMoveSpeed);
            }
            yield return new WaitForSeconds(timeBetweenBurst);
        }
        yield return new WaitForSeconds(resetTime);
        isShooting = false;
    }

}
