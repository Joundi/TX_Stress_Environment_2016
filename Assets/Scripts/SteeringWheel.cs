using UnityEngine;
using System.Collections;

public class SteeringWheel : MonoBehaviour {

    public float rotationDegree = 180f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Rotate(float steering)
    {
        transform.localEulerAngles = new Vector3(0, 0, -steering * rotationDegree/2f);
    }
}
