using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState { None = -1, Idle = 0, Wander, Pursuit, Attack, }
public class EnemyFSM : MonoBehaviour
{
    [Header("Pursuit")]
    [SerializeField]
    private float targetRecognitionRange = 8;
    [SerializeField]
    private float pursuitLimitRange = 10;
    [Header("Attack")]
    //[SerializeField]
    //private GameObject projectilePrefab;
    //[SerializeField]
    //private Transform projectileSpawnPoint;
    [SerializeField]
    private GameObject EXPItem;
    //[SerializeField]
    //private float attackRange = 0;
    //[SerializeField]
    //private float attackRate = 1;
    [SerializeField]
    private int attackPower = 10;
    private EnemyState enemyState = EnemyState.None;
    private float lastAttackTime = 0;
    private EnemyStatus status;
    private PlayerController playerController;
    private NavMeshAgent navMeshAgent;
    private Transform target;
    private EnemyMemoryPool enemyMemoryPool;
    public void Setup(Transform target, EnemyMemoryPool enemyMemoryPool)
    {
        playerController = target.GetComponent<PlayerController>();
        status = GetComponent<EnemyStatus>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        this.target = target;
        this.enemyMemoryPool = enemyMemoryPool;
        navMeshAgent.updateRotation = false;
    }
    private void OnEnable()
    {
        ChangeState(EnemyState.Idle);
    }
    private void OnDisable()
    {
        StopCoroutine(enemyState.ToString());
        enemyState = EnemyState.None;
    }
    private void ChangeState(EnemyState newState)
    {
        if (enemyState == newState) return;
        StopCoroutine(enemyState.ToString());
        enemyState = newState;
        StartCoroutine(enemyState.ToString());
    }
    private IEnumerator Idle()
    {
        StartCoroutine("AutoChangeFromIdleToWander");
        while (true)
        {
            CalculateDistanceToTargetAndSelectState();
            yield return null;
        }
    }
    private IEnumerator AutoChangeFromIdleToWander()
    {
        int changeTime = Random.Range(1, 5);
        yield return new WaitForSeconds(changeTime);
        ChangeState(EnemyState.Wander);
    }
    private IEnumerator Wander()
    {
        float currentTime = 0;
        float maxTime = 10;
        navMeshAgent.speed = status.NormalSpeed;
        navMeshAgent.SetDestination(CalculateWanderPosition());
        Vector3 to = new Vector3(navMeshAgent.destination.x, 0, navMeshAgent.destination.z);
        Vector3 from = new Vector3(transform.position.x, 0, transform.position.z);
        transform.rotation = Quaternion.LookRotation(to - from);
        while (true)
        {
            currentTime += Time.deltaTime;
            to = new Vector3(navMeshAgent.destination.x, 0, navMeshAgent.destination.z);
            from = new Vector3(transform.position.x, 0, transform.position.z);
            if ((to - from).sqrMagnitude < 0.01f || currentTime >= maxTime)
            {
                ChangeState(EnemyState.Idle);
            }
            CalculateDistanceToTargetAndSelectState();
            yield return null;
        }
    }
    private IEnumerator Pursuit()
    {
        while (true)
        {
            navMeshAgent.speed = status.FastSpeed;
            navMeshAgent.SetDestination(target.position);
            LookRotationToTarget();
            CalculateDistanceToTargetAndSelectState();
            yield return null;
        }
    }

    //private IEnumerator Attack()
    //{
    //    navMeshAgent.ResetPath();
    //    while (true)
    //    {
    //        LookRotationToTarget();
    //        CalculateDistanceToTargetAndSelectState();
    //        if (Time.time - lastAttackTime > attackRate)
    //        {
    //            lastAttackTime = Time.time;
    //            GameObject clone = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
    //            clone.GetComponent<EnemyProjectitle>().Setup(target.position);
    //        }
    //        yield return null;
    //    }
    //}
    private void LookRotationToTarget()
    {
        Vector3 to = new Vector3(target.position.x, 0, target.position.z);
        Vector3 from = new Vector3(transform.position.x, 0, transform.position.z);
        //바로돌기
        //transform.rotation = Quaternion.LookRotation(to - from);
        Quaternion rotation = Quaternion.LookRotation(to - from);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.01f);
    }
    private void CalculateDistanceToTargetAndSelectState()
    {
        if (target == null) return;
        float distance = Vector3.Distance(target.position, transform.position);
        //if (distance <= attackRange)
        //{
        //    ChangeState(EnemyState.Attack);
        //}
        if (distance <= targetRecognitionRange)
        {
            ChangeState(EnemyState.Pursuit);
        }
        //else if (distance >= pursuitLimitRange)
        //{
        //    ChangeState(EnemyState.Wander);
        //}
    }
    private Vector3 CalculateWanderPosition()
    {
        float wanderRadius = 10;
        int wanderJitter = 0;
        int wanderJitterMin = 0;
        int wanderJitterMax = 360;
        Vector3 rangePosition = Vector3.zero;
        Vector3 rangeScale = Vector3.one * 100.0f;
        wanderJitter = Random.Range(wanderJitterMin, wanderJitterMax);
        Vector3 targetPosition = transform.position + SetAngle(wanderRadius, wanderJitter);
        targetPosition.x = Mathf.Clamp(targetPosition.x, rangePosition.x - rangeScale.x * 0.5f, rangePosition.x + rangePosition.x + rangeScale.x * 0.5f);
        targetPosition.y = 0.0f;
        targetPosition.z = Mathf.Clamp(targetPosition.z, rangePosition.z - rangeScale.z * 0.5f, rangePosition.z + rangePosition.z + rangeScale.z * 0.5f);
        return targetPosition;
    }
    private Vector3 SetAngle(float radius, int angle)
    {
        Vector3 position = Vector3.zero;
        position.x = Mathf.Cos(angle) * radius;
        position.z = Mathf.Sin(angle) * radius;
        return position;
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.black;
    //    Gizmos.DrawRay(transform.position, navMeshAgent.destination - transform.position);
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, targetRecognitionRange);
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireSphere(transform.position, pursuitLimitRange);
    //    Gizmos.color = new Color(0.39f, 0.04f, 0.04f);
    //    Gizmos.DrawWireSphere(transform.position, attackRange);
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponentInParent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(attackPower);
            }
        }
        if (other.CompareTag("Bullet"))
        {
            Debug.Log("Enemy hit");
            EnemyFSM enemy = other.GetComponentInParent<EnemyFSM>();
            if (enemy != null)
            {
                BulletMovement bullet = other.GetComponent<BulletMovement>();
                if (bullet != null)
                {
                    enemy.TakeDamage(bullet.damage);
                }
            }
            Destroy(other.gameObject);
        }
    }


    public void TakeDamage(int damage)
    {
        Debug.Log("Damage Taken: " + damage);
        bool isDie = status.DecreaseHP(damage);
        if (isDie == true)
        {
            enemyMemoryPool.DeactivataEnemy(gameObject);
            GameObject expItem = Instantiate(EXPItem, transform.position, Quaternion.identity);
        }
    }
}
