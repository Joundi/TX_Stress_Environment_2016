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
    public StressEventContainer initialContainer;

    public StressEventContainer currentEventContainer
    {
        get { return m_currentEventContainer;  }
    }

    private StressEventContainer m_currentEventContainer;
    private Dictionary<KeyCode, StressEvent> m_Ongoingevents;

    //------------------------------------------------------
    //  Initialization
    //------------------------------------------------------
    void Start()
    {
        // Singleton
        Assert.IsNull(instance, "We shouldn't have 2 event manager in the scene !");
        instance = this;

        Assert.IsNotNull(initialContainer);
        SetCurrentContainer(initialContainer);
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
        // if there is an event waiting for player in the current event container
        // and they are not on the same street
        if(m_currentEventContainer != null && !m_currentEventContainer.IsEmplty
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
        m_currentEventContainer = p_Newcontainer;
    }
}