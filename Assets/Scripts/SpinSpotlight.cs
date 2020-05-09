using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// 시험용 스포트라이트 회전 기능 컴포넌트.
//

public class SpinSpotlight : MonoBehaviour {

    public Light spotlight;
    public float spinspeed;
    private float spinspeed2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        spotlight.transform.Rotate(spinspeed, 0, 0);
	}
}
