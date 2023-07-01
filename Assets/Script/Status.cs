using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

[System.Serializable]
public class HPEvent : UnityEngine.Events.UnityEvent<int, int> { }
public class Status : MonoBehaviour
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
    private int maxHP = 300;
    private int currentHP;
    [Header("Level")]
    [SerializeField]
    private int maxLevel = 200;
    private int currentLevel = 1;
    [Header("EXP")]
    private int currentEXP;
    private int maxEXP;
    [SerializeField]
    private TextMeshProUGUI hpText;
    [SerializeField]
    private TextMeshProUGUI levelText;
    public float NormalSpeed => normalSpeed;
    public float FastSpeed => fastSpeed;
    public int CurrentHP => currentHP;
    public int MaxHP => maxHP;
    public int MaxLevel => maxLevel;
    public int CurrentEXP => currentEXP;
    public int MaxEXP => maxEXP;
    private void Awake()
    {
        currentEXP = 0;
        maxEXP = 100;
        currentHP = maxHP;
        maxEXP = CalculateMaxEXP(currentLevel);
        UpdateHPText();
        UpdateLevelText();
    }
    private int CalculateMaxEXP(int level)
    {
        return level *200 ;
    }
    private void UpdateHPText()
    {
        hpText.text = "" + currentHP.ToString(); // Customize the text format as needed
    }
    private void UpdateLevelText()
    {
        levelText.text=""+currentLevel.ToString();
    }
    public void IncreaseEXP(int exp)
    {
        int previousEXP = currentEXP;
        currentEXP += exp;

        if (currentEXP >= maxEXP)
        {
            LevelUp();
        }
    }
    private void LevelUp()
    {
        Debug.Log("·¹º§¾÷");
        currentLevel++;
        maxEXP = CalculateMaxEXP(currentLevel);
        currentEXP = 0;
        UpdateLevelText();
    }
    public bool DecreaseHP(int damage)
    {
        int previousHP = currentHP;
        currentHP = currentHP - damage > 0 ? currentHP - damage : 0;
        onHPEvent.Invoke(previousHP, currentHP);
        if (currentHP == 0)
        {
            return true;
        }
        UpdateHPText();
        return false;
    }
    public void IncreaseHP(int hp)
    {
        int previousHP = currentHP;
        currentHP = currentHP + hp > MaxHP ? MaxHP : currentHP + hp;
        onHPEvent.Invoke(previousHP, currentHP);
        UpdateHPText();
    }
    
}
