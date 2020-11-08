using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestList : MonoBehaviour
{
    [SerializeField] Mission[] missions;
    Note playerNote;

    void Start()
    {
        playerNote = FindObjectOfType<Note>();
    }
    
    public Mission GetMission(string _name)
    {
        for(int i=0; i<missions.Length; i++)
        {
            if(_name == missions[i].name)
            {
                return missions[i];
            }
        }

        return null;
    }
}
