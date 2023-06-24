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
//BulletMovement 스크립트가 적용된 BasicBullet 프리펩이 Enemy태그를 가진 Enemy프리펩과 충돌했을때 Enemy