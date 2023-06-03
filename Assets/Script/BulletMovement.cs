using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private Vector3 direction;
    private float speed;
    private EnemyFSM enemyFSM;
    public int damage = 100;

    public void Initialize(Vector3 dir, float spd)
    {
        direction = dir;
        speed = spd;
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Enemy"))
    //    {
    //        Debug.Log("Enemy hit");
    //        other.GetComponent<EnemyFSM>().TakeDamage(damage);
    //        Destroy(gameObject);
    //    }
    //}
}
