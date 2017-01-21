using UnityEngine;
using System.Collections;


public class SpeedMonitor : MonoBehaviour
{
    public RectTransform pointer;
    public void SetCurrentSpeed(float p_fSpeed, float p_fMaxSpeed)
    {
        pointer.localRotation = Quaternion.Euler(0f, 0f, -257f * p_fSpeed / p_fMaxSpeed);
    }
}