using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ExpBarControl : MonoBehaviour
{
    public enum InfoType {Exp, Level, Kill, Time, Health}
    public InfoType type;
    Text myText;
    Slider mySlider;
    private void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }
    private void LateUpdate()
    {
    //    switch (type)
    //    {
    //        case InfoType.Exp:
    //            float curExp=GameManager.Instance.Exp;
    //            float maxExp = GameManager.instance.nextExp[GameManager.instance.level];
    //            mySlider.value = curExp/ maxExp;
    //            break;
    //        case InfoType.Level:

    //            break; 
    //        case InfoType.Kill:

    //            break;
    //        case InfoType.Time:

    //            break;
    //        case InfoType.Health:

    //            break;
    //    }
    }
}
//BulletMovement ��ũ��Ʈ�� ����� BasicBullet �������� Enemy�±׸� ���� Enemy������� �浹������ Enemy