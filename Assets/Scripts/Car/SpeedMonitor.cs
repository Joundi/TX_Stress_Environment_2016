using UnityEngine;
using System.Collections;


public class SpeedMonitor : MonoBehaviour
{
    public void SetCurrentSpeed(float p_fSpeed, string p_unit)
    {
        GetComponentInChildren<UnityEngine.UI.Text>().text = p_fSpeed.ToString("F2") + " " + p_unit;
    }
}
