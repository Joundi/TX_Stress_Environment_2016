using UnityEngine;
using System.Collections;

[System.Serializable]
public struct SEventTimeControl
{
    public string eventName;
    public float startTime;
    public float duration;
}

[System.Serializable]
public struct SEventKeyControl
{
    public string eventName;
    public KeyCode startKey;
    public KeyCode endKey;
}

public class StressEventController : MonoBehaviour
{
    //------------------------------------------------------
    // In this script, we can call events set in the StressEventManager
    //------------------------------------------------------

    public SEventTimeControl[] eventTimeControl;    // Set event start time and duration
    public SEventKeyControl[] eventKeyControl;      // Set event start key and end key

    void Start()
    { 
        // apply event times set in Inpector
        foreach (SEventTimeControl control in eventTimeControl)
        {
            StartCoroutine(StartEventAfterNSeconds(control.eventName, control.startTime));
            StartCoroutine(EndEventAfterNSeconds(control.eventName, control.startTime + control.duration));

        }
    }

    //------------------------------------------------------
    // 
    //------------------------------------------------------
    void Update()
    {
        // check event controlled by keys
        foreach (SEventKeyControl control in eventKeyControl)
        {
            if (Input.GetKeyDown(control.startKey))
            {
                StressEventManager.instance.StartEvent(control.eventName);
            }

            if (Input.GetKeyDown(control.endKey))
            {
                StressEventManager.instance.EndEvent(control.eventName);
            }
        }
    }

    //------------------------------------------------------
    // These functions allow for starting/ending an event at the end of any second
    /// For example, if we want to start the aquaplane event at the end of 5 seconds and end it at the end of 25 seconds
    /// StartCoroutine(StartEventAfterNSeconds("Aquaplane", 5f));
    /// StartCoroutine(EndEventAfterNSeconds("Aquaplane", 25f));
    //------------------------------------------------------
    IEnumerator StartEventAfterNSeconds(string stressEvent, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        StressEventManager.instance.StartEvent(stressEvent);
    }

    IEnumerator EndEventAfterNSeconds(string stressEvent, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        StressEventManager.instance.EndEvent(stressEvent);
    }
}