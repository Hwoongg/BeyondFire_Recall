using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu_SlowMove : MonoBehaviour {

    public bool LeftOn = true;
    
	void Update () {
        if (gameObject.transform.position.x <= 3.3f && LeftOn)
        {
            gameObject.transform.Translate(Vector3.right * Time.deltaTime * 0.15f);
        }
        else if (gameObject.transform.position.x >= 3.3f && LeftOn)
        {
            LeftOn = false;
        }
        else if (!LeftOn)
        {
            gameObject.transform.Translate(Vector3.left * Time.deltaTime * 0.15f);
            if (!LeftOn && gameObject.transform.position.x <= 1f)
            {
                LeftOn = true;
            }
        }
    }
}
