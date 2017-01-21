using UnityEngine;
using System.Collections;

public class SteeringWheel : MonoBehaviour {

    [HideInInspector]
    public float wheelRange = 360f;

    public void Rotate(float steering)
    {
        transform.localEulerAngles = new Vector3(0, 0, -steering * wheelRange / 2f);
    }
}