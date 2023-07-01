using UnityEngine;
using System;
using Random = System.Random;
using JetBrains.Annotations;
using System.Threading;

public class PlayerController : MonoBehaviour
{
    private MovementCharacterController movement;
    private Status status;
    void Awake()
    {
        status = GetComponent<Status>();
        movement = GetComponent<MovementCharacterController>();
    }
    private void Update()
    {
        UpdateMove();
    }
    private void UpdateMove()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput);
        transform.position += moveDirection.normalized * status.NormalSpeed * Time.deltaTime;
    }
    public void TakeDamage(int damage)
    {
        Debug.Log("playercontroller's");
        Debug.Log(string.Format("Take {0} damage", damage));
        bool isDie = status.DecreaseHP(damage);
        if (isDie)
        {
            Debug.Log("GameOver");
        }
    }
}
