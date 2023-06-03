using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float fireRate = 0.5f;

    private void Start()
    {
        InvokeRepeating("ShootBullet", fireRate, fireRate);
    }

    private void ShootBullet()
    {
        GameObject closestEnemy = FindClosestEnemyWithTag("Enemy");

        if (closestEnemy != null)
        {
            Vector3 direction = closestEnemy.transform.position - transform.position;
            direction.Normalize();

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.LookRotation(direction));

            Destroy(bullet.GetComponent<Rigidbody>());
            Destroy(bullet.GetComponent<Collider>());

            BulletMovement bulletMovement = bullet.AddComponent<BulletMovement>();
            bulletMovement.Initialize(direction, bulletSpeed);
        }
    }

    private GameObject FindClosestEnemyWithTag(string tag)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestEnemy = enemy;
                closestDistance = distance;
            }
        }

        return closestEnemy;
    }
}
