using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;

[System.Serializable]
public class StressEventManager : MonoBehaviour {

    [System.Serializable]
    public struct SEventSetting
    {
        public KeyCode eventStartTrigger;
        public KeyCode eventEndTrigger;
        public StressEvent stressEvent;
    }

    static public StressEventManager instance;

    public SEventSetting[] eventSettings;
    private StressEventContainer m_currentEventContainer;

    private Queue<StressEvent_InContainer> m_eventWaitingList = new Queue<StressEvent_InContainer>();

    //------------------------------------------------------
    //  Initialization
    //------------------------------------------------------
    void Start()
    {
        // Singleton
        Assert.IsNull(instance, "We shouldn't have 2 event manager in the scene !");
        instance = this;
    }

    //------------------------------------------------------
    //  Input trigger management
    //------------------------------------------------------
    void Update()
    {
        foreach (SEventSetting setting in eventSettings)
        {
            if (Input.GetKeyDown(setting.eventStartTrigger))
            {
                if (setting.stressEvent.IsInactive)
                {
                    setting.stressEvent.StartEvent();
                }
            };

            if (Input.GetKeyDown(setting.eventEndTrigger))
            {
                if (!setting.stressEvent.IsInactive)
                {
                    setting.stressEvent.EndEvent();
                }
            };
        }
    }

    //------------------------------------------------------
    //  Set current event container
    //------------------------------------------------------
    public void SetCurrentContainer(StressEventContainer p_Newcontainer)
    {
        if (p_Newcontainer != null)
        {
            // if there is an event waiting for player in the current event container
            // and they are not on the same street
            if (m_currentEventContainer != null && !m_currentEventContainer.IsEmplty
                && p_Newcontainer.transform.parent != m_currentEventContainer.transform.parent)
            {
                // deplace the event
                StressEvent_InContainer stressEvent = m_currentEventContainer.currentEvent;
                if (stressEvent.IsWaitingForPlayer)
                {
                    m_currentEventContainer.FreeContainer();
                    p_Newcontainer.SetEventToContainer(stressEvent);
                }
            }
            // if there are events in the waiting list
            else if (m_eventWaitingList.Count > 0)
            {
                // Set event to this container
                p_Newcontainer.SetEventToContainer(m_eventWaitingList.Dequeue());
            }
        }
        else
        {
            // if the current container is to be set null
            // if there is an event waiting for player in the current event container
            if (m_currentEventContainer != null && !m_currentEventContainer.IsEmplty)
            {
                // deplace the event to event waiting list
                StressEvent_InContainer stressEvent = m_currentEventContainer.currentEvent;
                if (stressEvent.IsWaitingForPlayer)
                {
                    m_currentEventContainer.FreeContainer();
                    Assert.IsFalse(m_eventWaitingList.Contains(stressEvent));
                    m_eventWaitingList.Enqueue(stressEvent);
                }
            }
        }
        m_currentEventContainer = p_Newcontainer;
    }

    //------------------------------------------------------
    //  Set event to current container
    //------------------------------------------------------
    public void SetEventToCurrentContainer(StressEvent_InContainer p_newEvent)
    {
        if (m_currentEventContainer != null)
        {
            m_currentEventContainer.SetEventToContainer(p_newEvent);
        }
        else
        {
            if (!m_eventWaitingList.Contains(p_newEvent))
            {
                m_eventWaitingList.Enqueue(p_newEvent);
            }
        }
    }
}