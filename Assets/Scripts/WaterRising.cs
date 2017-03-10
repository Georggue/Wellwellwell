using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRising : MonoBehaviour {

    public float WaterRisingFactor;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + WaterRisingFactor, transform.localScale.z);
    }
}
