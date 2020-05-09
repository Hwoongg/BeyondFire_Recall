using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// 상호작용 시스템의 기본 추상클래스입니다.
//
public abstract class InteractionSystem : MonoBehaviour
{

    // abstract = 순수가상, 포함한다면 클래스에도 지정해야한다
    // virtual = 가상함수. 부모에 정의
    // override = 재정의
    // 의문점 : override를 override 할 수 있는가? = 가능.
    // 순수가상 private는 불가능한가? = 불가능함, C++에서도 그랬을 듯. 추후 실험
    public abstract void doAction();

    // 상, 하단 버튼 기능 분리용 함수 추가.
    public abstract void upAction();
    public abstract void downAction();

}
