using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class EnemyHPEvent : UnityEngine.Events.UnityEvent<int, int> { }
public class EnemyStatus : MonoBehaviour
{
    [HideInInspector]
    public HPEvent onHPEvent = new HPEvent();
    [Header("Walk,Run Speed")]
    [SerializeField]
    private float normalSpeed;
    [SerializeField]
    private float fastSpeed;

    [Header("HP")]
    [SerializeField]
    private int maxHP = 100;
    private int currentHP;
    public float NormalSpeed => normalSpeed;
    public float FastSpeed => fastSpeed;
    public int CurrentHP => currentHP;
    public int MaxHP => maxHP;
    private void Awake()
    {
        currentHP = maxHP;
    }

    public bool DecreaseHP(int damage)
    {
        Debug.Log(string.Format("Take {0} damage", damage));
        int previousHP = currentHP;
        currentHP = currentHP - damage > 0 ? currentHP - damage : 0;
        onHPEvent.Invoke(previousHP, currentHP);
        if (currentHP == 0)
        {
            return true;
        }
        return false;
    }
    public void IncreaseHP(int hp)
    {
        int previousHP = currentHP;
        currentHP = currentHP + hp > MaxHP ? MaxHP : currentHP + hp;
        onHPEvent.Invoke(previousHP, currentHP);
    }

}
