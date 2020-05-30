using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneManager : MonoBehaviour
{
    Transform upperTransform;

    // Start is called before the first frame update
    void Start()
    {
        upperTransform = transform.Find("SubCG");
    }

    public void DisableSubCG()
    {
        for(int i=0; i<upperTransform.childCount; i++)
        {
            upperTransform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
