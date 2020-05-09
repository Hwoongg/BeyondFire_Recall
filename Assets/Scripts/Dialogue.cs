using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// 대화 객체입니다.
//

[System.Serializable]
public class Dialogue
{
    public string name;
    public Sprite sprNPCImage;

    [TextArea(3, 10)]
    public string[] sentences;
}
