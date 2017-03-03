using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class StressEvent_InContainer : StressEvent
{
    public StressEventContainer parentContainer
    {
        get { return m_parentContainer; }
        set { m_parentContainer = value; }
    }

    private StressEventContainer m_parentContainer;

    //------------------------------------------------------
    //  Constructor
    //------------------------------------------------------
    public StressEvent_InContainer() : base(EStressEventType.e_InContainer) { }

    //------------------------------------------------------
    //  Initialization
    //------------------------------------------------------s
    override protected void Start()
    {
        base.Start();

        // Check the existence of event trigger
        m_parentContainer = null;
        if (GetComponent<Collider>() == null)
            Debug.LogError("There is no event trigger set to " + name + ", the event in container won't be activated");
    }

    //------------------------------------------------------
    // Start event when player enters the trigger collider
    // A container event must have a collider as the event trigger
    //------------------------------------------------------
    private void OnTriggerEnter(Collider p_player)
    {
        if (p_player.gameObject.tag == "Player" && m_eventState == EStressEventState.e_waitingForPlayer)
        {
            StartContainerEvent();
        }
    }

    //------------------------------------------------------
    //  Attribute the container nearest to player to the event
    //------------------------------------------------------
    sealed override public void StartEvent()
    {
        // call base 
        base.StartEvent();

        // Set event state
        m_eventState = EStressEventState.e_waitingForPlayer;

        //Find the nearest conetainer and put the event in it
        StressEventManager.instance.SetEventToCurrentContainer(this);
    }

    //------------------------------------------------------
    //  End event and free container
    //------------------------------------------------------
    sealed override public void EndEvent()
    {
        // call base 
        base.EndEvent();

        // end event
        EndContainerEvent();

        //Free container
        Assert.IsNotNull(m_parentContainer);
        m_parentContainer.FreeContainer();
    }

    //------------------------------------------------------
    //  To be implemented in child classes
    //------------------------------------------------------
    virtual public void StartContainerEvent()
    {
        m_eventState = EStressEventState.e_ongoing;
    }

    //------------------------------------------------------
    //  Reset event values, prepare it for the next call
    //------------------------------------------------------
    virtual public void EndContainerEvent()
    {

    }
}