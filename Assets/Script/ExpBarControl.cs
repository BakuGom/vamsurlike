using UnityEngine;
using UnityEngine.UI;

public class ExpBarControl : MonoBehaviour
{
    Text myText;
    Slider mySlider;
    private Status status;

    private void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
        status = FindObjectOfType<Status>();
    }

    private void Update()
    {
        int currentExp = status.CurrentEXP;
        int maxExp = status.MaxEXP;
        mySlider.value = (float)currentExp / maxExp;
    }
}
