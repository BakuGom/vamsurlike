using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectControl : MonoBehaviour
{
    private GameManager gameManager;
    public void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }
    public void ShowSkillOption()
    {
        if (Time.timeScale == 1) 
        {
            Time.timeScale = 0f;
            gameManager.SkillOption.SetActive(true);
        }
    }
    public void HideSkillOption()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1f;
            gameManager.SkillOption.SetActive(false);
        }
    }
}
