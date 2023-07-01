using UnityEngine;

public class EXPItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Status status = other.GetComponent<Status>();
            if (status != null)
            {
                status.IncreaseEXP(100);
            }

            gameObject.SetActive(false);
        }
    }
}
