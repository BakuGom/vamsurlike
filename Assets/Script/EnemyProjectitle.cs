using System.Collections;
using UnityEngine;

public class EnemyProjectitle : MonoBehaviour
{
    private MovementTransform movement;
    private float projectileDistance = 30;
    private int damage = 5;
    public void Setup(Vector3 position)
    {
        movement = GetComponent<MovementTransform>();
        StartCoroutine("OnMove", position);
    }
    private IEnumerator OnMove(Vector3 targetPosrition)
    {
        Vector3 start = transform.position;
        movement.MoveTo((targetPosrition - transform.position).normalized);
        while (true)
        {
            if (Vector3.Distance(transform.position, start) >= projectileDistance)
            {
                Destroy(gameObject);
                yield break;
            }
            yield return null;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("player Hit");
            other.GetComponent<PlayerController>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
