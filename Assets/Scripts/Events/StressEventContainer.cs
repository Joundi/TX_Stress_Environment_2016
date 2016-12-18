using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class StressEventContainer : MonoBehaviour {

    public bool IsEmplty
    {
        get { return m_currentStressEvent == null; }
    }

    public StressEvent_InContainer currentEvent
    {
        get { return m_currentStressEvent; }
    }

    private StressEvent_InContainer m_currentStressEvent;
    private Transform m_originalParent;

    //------------------------------------------------------
    //  Initialization
    //------------------------------------------------------
    void Start ()
    {
        m_currentStressEvent = null;
        m_originalParent = null;
    }

    //------------------------------------------------------
    //  Set event into the container
    //------------------------------------------------------
    public void SetEventToContainer(StressEvent_InContainer p_event)
    {
        // No event in the current container
        if (!IsEmplty)
        {
            Debug.LogWarning("An event has already been assigned to the nearest container, please wait");
            return;
        }

        // Set parent of event
        m_originalParent = p_event.transform.parent;
        p_event.transform.SetParent(transform, false);
        p_event.transform.localPosition = Vector3.zero;

        // Assign current event
        m_currentStressEvent = p_event;
        m_currentStressEvent.parentContainer = this;
        Assert.IsNotNull(m_currentStressEvent);
    }

    //------------------------------------------------------
    //  Free the event in the container
    //------------------------------------------------------
    public void FreeContainer()
    {
        Assert.IsNotNull(m_originalParent);

        // Set its parent  to its original parent
        m_currentStressEvent.transform.SetParent(m_originalParent, false);
        m_currentStressEvent.transform.localPosition = Vector3.zero;

        // Set current event to null
        m_currentStressEvent.parentContainer = null;
        m_currentStressEvent = null;
    }
}