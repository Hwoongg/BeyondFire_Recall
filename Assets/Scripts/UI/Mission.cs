using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Mission 
{
    public string name;
    public string description;
    public bool isCountable;
    [HideInInspector] public int nowCount;
    public int maxCount;

}
