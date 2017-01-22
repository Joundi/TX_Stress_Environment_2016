using UnityEngine;
using System.Collections;

public class StressEventController : MonoBehaviour
{
    //------------------------------------------------------
    // In this script, we can call events setted in the StressEventManager
    //------------------------------------------------------
    void Start()
    {
        // For example, if we want to start the aquaplane event at the end of 5 seconds and end it at the end of 25 seconds
        //StartCoroutine(StartEventAfterNSeconds("Aquaplane", 5f));
        //StartCoroutine(EndEventAfterNSeconds("Aquaplane", 25f));
    }

    //------------------------------------------------------
    // In this script, we can call events setted in the StressEventManager
    // These functions allow for starting/ending an event at the end of any second
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