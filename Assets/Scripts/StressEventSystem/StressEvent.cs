using UnityEngine;
using System.Collections;

public enum EStressEventType
{
    e_Immediate,
    e_InContainer
}

public enum EStressEventState
{
    e_inactive,
    e_waitingForPlayer,  // this is an event state only exists for StressEvent_InContainer
    e_ongoing
}

public class StressEvent : MonoBehaviour
{
    public bool IsOngoing
    {
        get { return m_eventState == EStressEventState.e_ongoing; }
    }

    public bool IsWaitingForPlayer
    {
        get { return m_eventState == EStressEventState.e_waitingForPlayer; }
    }

    public bool IsInactive
    {
        get { return (m_eventState == EStressEventState.e_inactive); }
    }

    [HideInInspector]
    public EStressEventType m_eventType;
    [HideInInspector]
    public string m_eventName;
    protected EStressEventState m_eventState;

    //------------------------------------------------------
    //  Constructor
    //------------------------------------------------------
    public StressEvent(EStressEventType p_eventType)
    {
        m_eventType = p_eventType;
    }

    //------------------------------------------------------
    //  Initialization
    //------------------------------------------------------
    virtual protected void Start ()
    {
        // event state
        m_eventState = EStressEventState.e_inactive;
    }

    //------------------------------------------------------
    //  To be implemented in child classes
    //------------------------------------------------------
    virtual public void StartEvent()
    {
        Debug.Log("Event " + m_eventName + " starts");
        m_eventState = EStressEventState.e_ongoing;
    }
    virtual public void EndEvent()
    {
        Debug.Log("Event " + m_eventName + " ends");
        m_eventState = EStressEventState.e_inactive;
    }
}

abstract public class StressEvent_Immediate : StressEvent
{
    public StressEvent_Immediate() : base(EStressEventType.e_Immediate) {}
}