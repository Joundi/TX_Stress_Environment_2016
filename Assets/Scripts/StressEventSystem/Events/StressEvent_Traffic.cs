using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class StressEvent_Traffic : StressEvent_Immediate
{
    public int maxCarNb = 150;
    public int normalCarNb = 10;

    private TSTrafficSpawner m_trafficController;
    //------------------------------------------------------
    //  Initialization
    //------------------------------------------------------
    override protected void Start()
    {
        base.Start();

        // get traffic controller
        m_trafficController = GetComponent<TSTrafficSpawner>();
        Assert.IsNotNull(m_trafficController, 
            "Please attache this event script to the object to which a TSTrafficSpawner script has been added!");

        // Set car nb
        m_trafficController.amount = normalCarNb;
        m_trafficController.totalAmountOfCars = normalCarNb;
    }

    //------------------------------------------------------
    //  Stress Event Start
    //------------------------------------------------------
    override public void StartEvent()
    {
        base.StartEvent();

        // Set car nb
        m_trafficController.amount = maxCarNb;
        m_trafficController.totalAmountOfCars = maxCarNb;
    }

    //------------------------------------------------------
    //  Stress Event End
    //------------------------------------------------------
    override public void EndEvent()
    {
        base.EndEvent();

        // Set car nb
        m_trafficController.amount = normalCarNb;
        m_trafficController.totalAmountOfCars = normalCarNb;
    }
}
