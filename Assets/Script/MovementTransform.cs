using UnityEngine;

public class MovementTransform : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0.0f;
    [SerializeField]
    private Vector3 MoveDirection = Vector3.zero;
    private void Update()
    {
        transform.position += MoveDirection * moveSpeed * Time.deltaTime;
    }
    public void MoveTo(Vector3 direction)
    {
        MoveDirection = direction;
    }
}
